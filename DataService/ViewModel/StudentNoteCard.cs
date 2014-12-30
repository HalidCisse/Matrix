using System;
using System.Linq;
using DataService.Context;
using DataService.Entities.Pedagogy;

namespace DataService.ViewModel
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
            var myStudentNote = GetStudentNote(coursGuid, studentGuid) ?? new StudentNote
            {
                StudentNoteGuid = Guid.NewGuid(),
                CoursGuid       = coursGuid,
                StudentGuid     = studentGuid,                    
            };

            StudentNoteGuid = myStudentNote.StudentNoteGuid;            
            CoursGuid       = myStudentNote.CoursGuid;
            StudentGuid     = myStudentNote.StudentGuid;
            IsPresentColor  = EstPresent(studentGuid, CoursGuid) ? "#A9A9CB" : "Red";
            Note            = myStudentNote.Note;
            Appreciation    = myStudentNote.Appreciation;
            
            using (var db = new Ef())
            {
                var st        = db.Student.Find(myStudentNote.StudentGuid);
                PhotoIdentity = st?.PhotoIdentity;
                FullName      = st?.FullName;
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


        private static StudentNote GetStudentNote(Guid coursGuid, Guid studentGuid)
        {
            using (var db = new Ef())
            {
                return db.StudentNote.FirstOrDefault(t => t.CoursGuid.Equals(coursGuid) && t.StudentGuid.Equals(studentGuid));
            }
        }

        private static bool EstPresent(Guid personGuid, Guid controlGuid)
        {
            var coursDate = new DateTime();  

            using (var xb = new Ef())
            {
                var startDate = xb.Cours.Find(controlGuid)?.StartDate;
                if (startDate != null)
                    coursDate = ((DateTime) startDate).Date;
            }

            using (var db = new Ef())
            {
                var ticket = db.AbsenceTicket.FirstOrDefault(t => t.CoursGuid == controlGuid    &&
                                                                  t.PersonGuid == personGuid  &&
                                                                  t.CoursDate == coursDate    );

                return ticket == null || ticket.IsPresent;
            }
        }


    }
}
