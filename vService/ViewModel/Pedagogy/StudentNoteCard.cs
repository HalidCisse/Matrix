using System;
using System.Linq;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// Model de Note
    /// </summary>
    public class StudentNoteCard
    {

        /// <summary>
        /// 
        /// </summary>
        public StudentNoteCard(Guid coursGuid, Guid studentGuid)
        {
            var myStudentNote = GetStudentNote(coursGuid, studentGuid) ?? new StudentGrade
            {
                StudentGradeGuid = Guid.NewGuid(),
                CoursGuid       = coursGuid,
                StudentGuid     = studentGuid                   
            };

            StudentNoteGuid = myStudentNote.StudentGradeGuid;            
            CoursGuid       = myStudentNote.CoursGuid;
            StudentGuid     = myStudentNote.StudentGuid;
            IsPresentColor  = EstPresent(studentGuid, CoursGuid) ? "#A9A9CB" : "Red";
            Note            = myStudentNote.Mark;
            Appreciation    = myStudentNote.Appreciation;
            
            using (var db = new SchoolContext())
            {
                var st        = db.Students.Find(myStudentNote.StudentGuid);
                PhotoIdentity = st?.Person.PhotoIdentity;
                FullName      = st?.Person.FullName;
            }
        }


        /// <summary>
        /// ID
        /// </summary>       
        public Guid StudentNoteGuid { get; }

        /// <summary>
        /// Student ID
        /// </summary>       
        public Guid StudentGuid { get; }

        /// <summary>
        /// Cours ID
        /// </summary>       
        public Guid CoursGuid { get; }
        
        /// <summary>
        /// Photo de l'etudiant
        /// </summary>
        public byte[] PhotoIdentity { get; }

        /// <summary>
        /// Nom Complet
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Si l'Etudiant est present au cours
        /// </summary>
        public string IsPresentColor { get; }

        /// <summary>
        /// La Note
        /// </summary>
        public double Note { get; set; }

        /// <summary>
        /// Appreciation
        /// </summary>
        public string Appreciation { get; set; }


        private static StudentGrade GetStudentNote(Guid coursGuid, Guid studentGuid)
        {
            using (var db = new SchoolContext())
                return
                    db.StudentGrades.FirstOrDefault(
                        t => t.CoursGuid.Equals(coursGuid) && t.StudentGuid.Equals(studentGuid));
        }

        private static bool EstPresent(Guid personGuid, Guid controlGuid)
        {
            var coursDate = new DateTime();  

            using (var xb = new SchoolContext())
            {
                var startDate = xb.Studies.Find(controlGuid)?.StartDate;
                if (startDate != null)
                    coursDate = ((DateTime) startDate).Date;
            }

            using (var db = new SchoolContext())
            {
                var ticket = db.AbsenceTickets.FirstOrDefault(t => t.CoursGuid == controlGuid    &&
                                                                  t.PersonGuid == personGuid  &&
                                                                  t.CoursDate == coursDate    );

                return ticket == null || ticket.IsPresent;
            }
        }


    }
}
