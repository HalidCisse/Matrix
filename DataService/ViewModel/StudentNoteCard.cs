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
                Appreciation    = ""
            };

            StudentNoteGuid = myStudentNote.StudentNoteGuid;
            StudentGuid = myStudentNote.StudentGuid;
            CoursGuid = myStudentNote.CoursGuid;
            Note = myStudentNote.Note;
            Appreciation = myStudentNote.Appreciation;

            using (var db = new Ef())
            {
                var st = db.Student.Find(myStudentNote.StudentGuid);
                PhotoIdentity = st?.PhotoIdentity;
                FullName = st?.FullName;
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



    }
}
