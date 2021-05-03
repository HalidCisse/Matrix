using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using Common.Security.Enums;
using DataService.Context;
using DataService.Helpers;
using DataService.ViewModel.Economat;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Notes
    /// </summary>
    public sealed class GradesManager
    {

        #region CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGrade"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Correcteur)]
        public bool AddOrUpdateStudentNote(StudentGrade myGrade) => NoteExist(myGrade) ? UpdateStudentNote(myGrade) : AddStudentNote(myGrade);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGrade"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Correcteur)]
        internal static bool AddStudentNote(StudentGrade myGrade)
        {           
            using (var db = new SchoolContext())
            {
                if(myGrade.StudentGradeGuid==Guid.Empty) myGrade.StudentGradeGuid = Guid.NewGuid();
                var matiereGuid = db.Studies.Find(myGrade.CoursGuid).SubjectGuid;

                myGrade.Coefficient      = db.Subjects.Find(matiereGuid).Coefficient;
                myGrade.DateTaken        = db.Studies.Find(myGrade.CoursGuid).StartDate;
                myGrade.Barem            = 20;

                myGrade.DateAdded        = DateTime.UtcNow;
                myGrade.AddUserGuid      = Guid.Empty;
                myGrade.LastEditDate     = DateTime.UtcNow;
                myGrade.LastEditUserGuid = Guid.Empty;

                db.Set<StudentGrade>().Add(myGrade);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGrade"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Correcteur)]
        internal static bool UpdateStudentNote(StudentGrade myGrade)
        {                   
            using (var db = new SchoolContext())
            {
                var oldGrade = db.StudentGrades.Find(myGrade.StudentGradeGuid);

                if (oldGrade == null) throw new InvalidOperationException("CAN_NOT_FIND_REFERENCE_GRADE");

                oldGrade.Mark             =myGrade.Mark;
                oldGrade.Appreciation     =myGrade.Appreciation;

                oldGrade.LastEditDate     =DateTime.UtcNow;
                oldGrade.LastEditUserGuid =Guid.Empty;

                db.Set<StudentGrade>().Attach(oldGrade);
                db.Entry(oldGrade).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGrade"></param>        
        /// <returns></returns>
        private static bool NoteExist(StudentGrade myGrade)
        {
            using (var db = new SchoolContext())
                return db.Studies.Find(myGrade.StudentGradeGuid) != null ||
                       db.StudentGrades.Any(t => t.CoursGuid == myGrade.CoursGuid &&
                                               t.StudentGuid == myGrade.StudentGuid);
        }


        #endregion



        #region Helpers



        /// <summary>
        /// Changer de Correcteur
        /// </summary>
        /// <returns></returns>
        public bool ChangeGrader (Guid examGuid, Guid newGraderGuid) {
            using (var db = new SchoolContext()) {

                var theExam = db.Studies.Find(examGuid);

                if(theExam==null)
                    throw new InvalidOperationException("CAN_NOT_FIND_REFERENCED_EXAM");
                if(db.Staffs.Find(newGraderGuid)==null)
                    throw new InvalidOperationException("CAN_NOT_FIND_REFERENCED_STAFF");

                theExam.GraderGuid=newGraderGuid;

                db.Studies.Attach(theExam);
                db.Entry(theExam).State=EntityState.Modified;
                return db.SaveChanges()>0;
            }
        }

        /// <summary>
        /// Les Cours dont le Proff est designer Correcteur
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetStaffScoringWork (Guid staffGuid, DateTime? startDate = null, DateTime? endDate = null) {
            using (var db = new SchoolContext()) {
                // ReSharper disable once InvertIf
                if(startDate==null||endDate==null) {
                    startDate=DateTime.Today.AddMonths(-1);
                    endDate=DateTime.Today;
                }
                return db.Studies.Where(c => c.GraderGuid==staffGuid&&!c.IsDeleted&&
                                ((
                                    c.StartDate<=startDate&&
                                    c.EndDate>=startDate
                                )
                                ||
                                (
                                    c.StartDate>=startDate&&
                                    c.StartDate<=endDate
                                ))).OrderByDescending(c => c.StartDate)
                                                .ThenBy(c => c.StartTime)
                                                .Include(c => c.Classe)
                                                .Include(c => c.Subject)
                                                .ToList()
                                                .Where(s=> CoursHelper.IsGraded(s.Type))
                                                .Select(s => new DataCard(s));
            }
        }

        /// <summary>
        /// List des Notes des Etudiants
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<GradeCard> GetGrades (Guid studentGuid, DateTime? startDate, DateTime? endDate) => StaticGetGrades(studentGuid, startDate, endDate).Select(g => new GradeCard(g)).ToList();

        /// <summary>
        /// Moyenne Generale d'un etudiant pendant ce temps
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public double GetAverage (Guid studentGuid, DateTime? fromDate, DateTime? toDate) {
            return StaticGetAverage(studentGuid, fromDate, toDate);
        }

        /// <summary>
        /// Les Notes Pour le controle
        /// </summary>
        /// <param name="currentCoursGuid"></param>
        /// <returns></returns>
        public IEnumerable GetGradeCards (Guid currentCoursGuid) {
            var noteList = new ConcurrentBag<StudentNoteCard>();

            //var stdsGuids = ClassesManager.StaticGetClassStudentsGuids(CoursManager.StaticGetCoursClasseGuid(currentCoursGuid), CoursManager.StaticGetCoursAnneeScolaireGuid(currentCoursGuid));

            using (var db = new SchoolContext()) {
                var coursDate = db.Studies.Find(currentCoursGuid).StartDate;

                var stdsGuids = db.Studies.Find(currentCoursGuid).
                    Classe.Inscriptions.Where(
                        i =>
                            !i.IsDeleted&&i.EnrollementStatus!=EnrollementStatus.Canceled&&
                            i.SchoolYear.DateDebut<=coursDate&&i.SchoolYear.DateFin>=coursDate)
                        .Select(i => i.Student.StudentGuid);

                Parallel.ForEach(stdsGuids, std => {
                    noteList.Add(new StudentNoteCard(currentCoursGuid, std));
                });

                return noteList.OrderBy(s => s.FullName);
            }
        }

        #endregion



        #region Protected Internal Static


        internal static double StaticGetAverage (Guid studentGuid, DateTime? startDate, DateTime? endDate) {            
            return StaticGetGrades(studentGuid, startDate, endDate).Any() ?
                        StaticGetGrades(studentGuid, startDate, endDate).Sum(g => (g.Mark*g.Coefficient))/
                        StaticGetGrades(studentGuid, startDate, endDate).Sum(g => g.Coefficient) : 0;
        }

        internal static List<StudentGrade> StaticGetGrades(Guid studentGuid, DateTime startDate, DateTime endDate)
        {            
            using (var db = new SchoolContext())
            {
                var studentClasses = EnrollementManager.GetStudentInscriptions(studentGuid, startDate, endDate).Select(c=> c.ClasseGuid);

                var controls = new List<Study>();
                foreach (var classe in studentClasses)
                    controls.AddRange(
                        db.Studies.Where(c => !c.IsDeleted && c.ClasseGuid==classe &&(c.Type==CoursTypes.Control||c.Type==CoursTypes.Examen||c.Type==CoursTypes.Test||c.Type==CoursTypes.Devoir||c.Type==CoursTypes.Composition)&& (
                            c.StartDate>=startDate&&
                            c.StartDate<=endDate
                        )));

                var grades = new List<StudentGrade>();
                foreach (var examGuid in controls.OrderBy(c => c.StartDate).Select(c=> c.StudyGuid).ToList())
                {
                    var corrected = db.StudentGrades.FirstOrDefault(g => g.CoursGuid == examGuid);
                    if (corrected != null)                      
                       grades.Add(corrected);
                }
                return grades;
            }
        }

        internal static List<StudentGrade> StaticGetGrades (Guid studentGuid, DateTime? startDate, DateTime? endDate) {
            //todo possible incoherence as if not never noted it shall be null !!

            using (var db = new SchoolContext()) {
                if(startDate==null||endDate==null)
                    return db.StudentGrades.Where(c => c.StudentGuid==studentGuid&&!c.IsDeleted)
                        .OrderBy(c => c.Study.StartDate).ToList();

                return db.StudentGrades.Where(c => c.StudentGuid==studentGuid&&!c.IsDeleted&&
                        (
                            c.DateTaken>=startDate&&
                            c.DateTaken<=endDate
                        )).OrderBy(c => c.Study.StartDate).ToList();
            }
        }

        #endregion


    }
}
