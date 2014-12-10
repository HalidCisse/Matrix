using DataService.Context;
using DataService.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion d'Etudiants
    /// </summary>
    public class StudentsManager
    {
       /// <summary>
       /// 
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
        /// 
        /// </summary>
        /// <param name="myStudent"></param>
        /// <returns></returns>
        public bool UpdateStudent(Student myStudent)
        {
            using (var db = new Ef())
            {
                db.Student.Attach(myStudent);
                db.Entry(myStudent).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteStudent(string studentId)
        {
            using (var db = new Ef())
            {
                db.Student.Remove(db.Student.Find(studentId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTudentId"></param>
        /// <returns></returns>
        public Student GetStudentById(string sTudentId)
        {
            using (var db = new Ef())
            {
                var myStudent = db.Student.Find(sTudentId);
                return myStudent;
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
                var myStudent = db.Student.SingleOrDefault(s => s.Firstname + s.Lastname == firstAndLastName);

                return myStudent;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Student> GetAllStudents()
        {
            using (var db = new Ef())
            {

                IList<Student> daraNatadjis = db.Student.ToList();

                return (List<Student>)daraNatadjis;
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
                var myStudentName = db.Student.Find(studentId).Firstname + " " + db.Student.Find(studentId).Lastname;
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

                return db.Student.Find(studentId) != null;

            }
        }

    }
}
