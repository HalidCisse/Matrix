using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion d'Etudiants
    /// </summary>
    public class StudentsManager
    {
       /// <summary>
       /// Ajouter Un Nouveau Etudiant
       /// </summary>
       /// <param name="myStudent"></param>
       /// <returns></returns>
        public bool AddStudent(Student myStudent)
        {
            using (var db = new Ef())
            {
                db.Student.Add(myStudent);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Modifier Un Etudiant
        /// </summary>
        /// <param name="myStudent"></param>
        /// <returns></returns>
        public bool UpdateStudent(Student myStudent)
        {
            using (var db = new Ef())
            {
                db.Student.Attach(myStudent);
                db.Entry(myStudent).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Supprimer Un Etudiant
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteStudent(string studentId)
        {
            using (var db = new Ef())
            {
                db.Student.Remove(db.Student.First(s => s.StudentId == studentId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Student GetStudentById(string studentId)
        {
            using (var db = new Ef())
            {
                return db.Student.First(s => s.StudentId == studentId);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        public Student GetStudentByGuid(Guid studentGuid)
        {
            using (var db = new Ef())
            {        
                return db.Student.Find(studentGuid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstAndLastName"></param>
        /// <returns></returns>
        public static Student GetStudentByFullName(string firstAndLastName)
        {
            using (var db = new Ef())
            {             
                return db.Student.SingleOrDefault(s => s.FirstName + s.LastName == firstAndLastName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HashSet<Student> GetAllStudents()
        {
            using (var db = new Ef())
            {                
                return new HashSet<Student>(db.Student.OrderBy(s => s.LastName)) ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string GetStudentName(string studentId)
        {
            using (var db = new Ef())
            {
                var myStudentName = db.Student.First(s => s.StudentId == studentId).FirstName;
                return myStudentName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool StudentExist(string studentId)
        {
            using (var db = new Ef())
            {
                return db.Student.Any(s => s.StudentId == studentId);
            }
        }

               
    }
}
