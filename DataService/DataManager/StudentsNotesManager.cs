using System;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Notes
    /// </summary>
    public class StudentsNotesManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public bool AddOrUpdateStudentNote(StudentNote myNote)
        {
            return NoteExist(myNote) ? UpdateStudentNote(myNote) : AddStudentNote(myNote);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        private static bool AddStudentNote(StudentNote myNote)
        {
            var controlDate = GetCoursById(myNote.CoursGuid)?.StartDate;
            if (controlDate == null || !EstPresent(myNote.StudentGuid, myNote.CoursGuid, (DateTime)controlDate)) myNote.Note = 0;

            using (var db = new Ef())
            {
                db.StudentNote.Add(myNote);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        private static bool UpdateStudentNote(StudentNote myNote)
        {
            var controlDate = GetCoursById(myNote.CoursGuid)?.StartDate;
            if (controlDate == null || !EstPresent(myNote.StudentGuid, myNote.CoursGuid, (DateTime)controlDate)) myNote.Note = 0;
                             
            using (var db = new Ef())
            {
                db.StudentNote.Attach(myNote);
                db.Entry(myNote).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myNoteGuid"></param>
        /// <returns></returns>
        private bool DeleteControlNote(Guid myNoteGuid)
        {
            using (var db = new Ef())
            {
                db.StudentNote.Remove(db.StudentNote.Find(myNoteGuid));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myNote"></param>        
        /// <returns></returns>
        private static bool NoteExist(StudentNote myNote)
        {
            using (var db = new Ef())
            {
                if (db.Cours.Find(myNote.StudentNoteGuid) != null) return true;

                return db.StudentNote.Any(t => t.CoursGuid == myNote.CoursGuid &&
                                                 t.StudentGuid == myNote.StudentGuid );
            }
        }



        #region Helpers
 
        private static bool EstPresent(Guid personGuid, Guid coursGuid, DateTime coursDate)
        {
            using (var db = new Ef())
            {
                var ticket = db.AbsenceTicket.FirstOrDefault(t => t.CoursGuid == coursGuid &&
                                                                          t.PersonGuid == personGuid &&
                                                                          t.CoursDate == coursDate);
                return ticket != null && ticket.IsPresent;
            }
        }

        private static Cours GetCoursById(Guid coursId)
        {
            using (var db = new Ef())
            {
                return db.Cours.Find(coursId);
            }
        }


        #endregion

    }
}
