using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using Common.Shared.Entity;
using DataService.Context;
using DataService.ViewModel;
using DataService.ViewModel.Economat;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Manager des Classes
    /// </summary>
    public class ClassesManager
    {

        #region CRUD


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myClasse"></param>
        /// <returns></returns>
        public bool AddClasse(Classe myClasse)
        {           
            using (var db = new SchoolContext())
            {
                if(!db.Filieres.Any(f => f.FiliereGuid==myClasse.FiliereGuid))
                    throw new InvalidOperationException("FILIERE_REFERENCE_NOT_FOUND");
                if(!db.SchoolYears.Any(a => a.DateDebut<=DateTime.Today&&a.DateFin>=DateTime.Today&&a.Session.Equals(myClasse.Session, StringComparison.CurrentCultureIgnoreCase)))
                    throw new InvalidOperationException("SESSION_NOT_ACTIVE");

                if (string.IsNullOrEmpty(myClasse.Name)) throw new InvalidOperationException("CLASSE_NAME_CAN_NOT_BE_EMPTY");
                if (string.IsNullOrEmpty(myClasse.Sigle)) myClasse.Sigle = myClasse.Name;
                if (string.IsNullOrEmpty(myClasse.Description)) myClasse.Description = myClasse.Name;
                if (myClasse.ClassGrade == 0) myClasse.ClassGrade = ClassGrades.Grade1;
                if (myClasse.ClasseGuid == Guid.Empty) myClasse.ClasseGuid = Guid.NewGuid();

                myClasse.DateAdded = DateTime.Now;
                myClasse.LastEditDate = DateTime.Now;

                db.Classes.Add(myClasse);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Modifier les Informations d'une Classe
        /// </summary>
        /// <param name="myClasse"></param>
        /// <returns></returns>
        public bool UpdateClasse(Classe myClasse)
        {
            using (var db = new SchoolContext())
            {
                if (String.IsNullOrEmpty(myClasse.Name)) throw new InvalidOperationException("CLASSE_NAME_CAN_NOT_BE_EMPTY");
                if (String.IsNullOrEmpty(myClasse.Sigle)) myClasse.Sigle = myClasse.Name;
                if (String.IsNullOrEmpty(myClasse.Description)) myClasse.Description = myClasse.Name;
                if (myClasse.ClassGrade == 0) myClasse.ClassGrade = ClassGrades.Grade1;

                myClasse.LastEditDate = DateTime.Now;

                db.Classes.Attach(myClasse);
                db.Entry(myClasse).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public bool DeleteClasse(Guid classeId)
        {
            using (var db = new SchoolContext())
            {
                //todo softdelete
                db.Classes.Remove(db.Classes.Find(classeId));
                return db.SaveChanges() > 0;
            }
        }


        #endregion



        #region Helpers


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public Classe GetClasseById(Guid classeId)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Find(classeId);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeName"></param>
        /// <returns></returns>
        public static Classe GetClasseByName(string classeName)
        {
            using (var db = new SchoolContext())
            {
                var myClasse = db.Classes.SingleOrDefault(s => s.Name == classeName);

                return myClasse;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Classe> GetAllClasse()
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public string GetClasseName(Guid classeId)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Find(classeId).Name;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public bool ClasseExist(string classeId)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Find(classeId) != null;
            }
        }


        /// <summary>
        /// Renvoi les matieres Enregistrees pour cette classe
        /// </summary>
        /// <param name="classeId"></param>
        /// <returns></returns>
        public List<Subject> GetClassMatieres(Guid classeId)
        {
            using (var db = new SchoolContext())
                //return db.Matiere.Where(m => m.ClasseGuid == classeId).ToList();   db.Matiere.Find(c.MatiereGuid)
                return
                    db.Studies.Where(m => m.ClasseGuid == classeId && !m.IsDeleted).Select(c => c.Subject).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public IEnumerable<Staff> GetClassStaffs(Guid classId)
        {
            var staffs = new List<Staff>();

            using (var db = new SchoolContext())
            {
                foreach (var st in db.Studies.Where(c => c.ClasseGuid == classId))
                    staffs.Add(db.Staffs.Find(st.Proff.StaffGuid));
                return staffs;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classGuid"></param>
        /// <param name="anneeScolaireGuid"></param>
        /// <returns></returns>
        public IEnumerable<ClasseStudentCard> GetClassStudents(Guid classGuid, Guid anneeScolaireGuid)
        {            
            using (var db = new SchoolContext())
            {
                //var students = new HashSet<ClasseStudentCard>();

                //foreach (var st in db.Enrollements.Where(c => c.ClasseGuid.Equals(classGuid) && c.SchoolYearGuid.Equals(anneeScolaireGuid) ))
                //{
                //    var nn = db.Students.Find(st.StudentGuid);
                //    if (nn != null)
                //        students.Add (new ClasseStudentCard(nn));
                //}

                //return students;

                return db.Enrollements.Where(c => c.ClasseGuid==classGuid&&c.SchoolYearGuid==anneeScolaireGuid)
                    .Select(e => e.Student)
                    .Include(s => s.Person)
                    .ToList()
                    .Select(s => new ClasseStudentCard(s));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="classGuid"></param>
        /// <returns></returns>
        public IEnumerable<StudentCard> GetClassStudents(Guid classGuid){
            using (var db = new SchoolContext()) {               
                return db.Enrollements.Where(c => c.ClasseGuid==classGuid&&c.SchoolYear.DateFin>=DateTime.Today)
                    .Select(e=> e.Student)
                    .Include(s=> s.Person)
                    .ToList()
                    .Select(s=> new StudentCard(s));
            }
        }


        /// <summary>
        /// Verifie si une classe de meme Nom Exist dans cette filiere
        /// </summary>
        /// <param name="filiereGuid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ClassNameExist(Guid filiereGuid, string name)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Any(c => c.FiliereGuid == filiereGuid && c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
        }


        /// <summary>
        /// Renvoi de nombre de fois le sigle exist
        /// </summary>
        /// <param name="sigle"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int ClassSigleCount(string sigle)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Count(c => c.Sigle.Equals(sigle, StringComparison.CurrentCultureIgnoreCase));
            }
        }


        /// <summary>
        /// Renvoi Toutes les Sigles de tous les classes
        /// </summary>
        /// <returns></returns>
        public List<string> ClassesSigles()
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Select(c => c.Sigle).Distinct().ToList();
            }
        }


        /// <summary>
        /// Renvoi de nombre de fois le nom exist pour cette filiere
        /// </summary>
        /// <param name="filiereGuid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int ClassNameCount(Guid filiereGuid, string name)
        {
            using (var db = new SchoolContext())
                return
                    db.Classes.Count(
                        c =>
                            c.FiliereGuid == filiereGuid &&
                            c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }


        /// <summary>
        /// Filiere et leurs Classes
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetClassesThree () {
            using (var db = new SchoolContext())
                return db.Filieres.ToList().Select(f => new FiliereClasses(f)).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <returns></returns>
        public IEnumerable GetStaffCurrentClasses (Guid staffGuid) {
            using (var db = new SchoolContext())
            {
                var spanPeriod = DateTime.Today.AddMonths(-3);
                return db.Studies.Where(s=> s.ProffGuid == staffGuid && s.EndDate >spanPeriod)
                                 .Select(s=> s.Classe)
                                 .Distinct()
                                 .Include(c=> c.Filiere)
                                 .ToList()
                                 .Select(s=> new DataCard(s));
            }
        }


        #endregion



        #region Internal Static


        internal static List<Guid> StaticGetClassStudentsGuids(Guid classGuid, Guid anneeScolaireGuid)
        {
            using (var db = new SchoolContext())
                return new List<Guid>(db.Enrollements.Where(
                    i => i.ClasseGuid.Equals(classGuid) && i.SchoolYearGuid.Equals(anneeScolaireGuid))
                    .Select(i => i.StudentGuid));
        }


        internal static Classe StaticGetClasseById(Guid classeGuid)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Find(classeGuid);
            }
        }


        internal static bool StaticGenerateClasses(Filiere filiere, string session)
        {
            var upper = (int)filiere.Scolarite;

            for (var i = 1; i <= upper; i++)
            {
                var newClasse = new Classe { ClasseGuid = Guid.NewGuid(), FiliereGuid = filiere.FiliereGuid, ClassGrade = (ClassGrades)i, Session = session };

                if (i == 1) newClasse.Name = "1 ère Année";
                else newClasse.Name = i + " ème Année";
                newClasse.Description = newClasse.Name;

                using (var db = new SchoolContext())
                {
                    db.Classes.Add(newClasse);
                    db.SaveChanges();
                }
            }
            return true;
        }


        internal static bool StaticClasseValid(Guid classeGuid)
        {
            using (var db = new SchoolContext())
            {
                var classe = db.Classes.Find(classeGuid);

                return PedagogyManager.StaticSessionValid(classe.Session);
            }
        }


        internal static bool StaticCanAddClasse()
        {
            using (var db = new SchoolContext())
            {
                return db.SchoolYears.Any();
            }
        }





        #endregion

        
    }
}
