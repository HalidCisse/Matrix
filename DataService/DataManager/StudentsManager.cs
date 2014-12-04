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
       /// <param name="MyStudent"></param>
       /// <returns></returns>
        public bool AddStudent(Student MyStudent)
        {
            using (var Db = new EF())
            {
                Db.STUDENT.Add(MyStudent);
                return Db.SaveChanges() > 0;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyStudent"></param>
        /// <returns></returns>
        public bool UpdateStudent(Student MyStudent)
        {
            using (var Db = new EF())
            {
                Db.STUDENT.Attach(MyStudent);
                Db.Entry(MyStudent).State = System.Data.Entity.EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public bool DeleteStudent(string StudentID)
        {
            using (var Db = new EF())
            {
                Db.STUDENT.Remove(Db.STUDENT.Find(StudentID));
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="STudentID"></param>
        /// <returns></returns>
        public Student GetStudentByID(string STudentID)
        {
            using (var Db = new EF())
            {
                var MyStudent = Db.STUDENT.Find(STudentID);
                return MyStudent;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FirstANDLastName"></param>
        /// <returns></returns>
        public static Student GetStudentByFullName(string FirstANDLastName)
        {
            using (var Db = new EF())
            {
                var MyStudent = Db.STUDENT.SingleOrDefault(S => S.FIRSTNAME + S.LASTNAME == FirstANDLastName);

                return MyStudent;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Student> GetAllStudents()
        {
            using (var Db = new EF())
            {

                IList<Student> DaraNatadjis = Db.STUDENT.ToList();

                return (List<Student>)DaraNatadjis;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public string GetStudentName(string StudentID)
        {
            using (var Db = new EF())
            {
                var MyStudentName = Db.STUDENT.Find(StudentID).FIRSTNAME + " " + Db.STUDENT.Find(StudentID).LASTNAME;
                return MyStudentName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public bool StudentExist(string StudentID)
        {
            using (var Db = new EF())
            {

                return Db.STUDENT.Find(StudentID) != null;

            }
        }

    }
}
