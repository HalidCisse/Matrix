using DataService.Context;
using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Manager des Classes
    /// </summary>
    public class ClassesManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyClasse"></param>
        /// <returns></returns>
        public bool AddClasse(Classe MyClasse)
        {
            using (var Db = new EF())
            {
                Db.CLASSE.Add(MyClasse);
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyClasse"></param>
        /// <returns></returns>
        public bool UpdateClasse(Classe MyClasse)
        {
            using (var Db = new EF())
            {
                Db.CLASSE.Attach(MyClasse);
                Db.Entry(MyClasse).State = EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClasseID"></param>
        /// <returns></returns>
        public bool DeleteClasse(Guid ClasseID)
        {
            using (var Db = new EF())
            {
                Db.CLASSE.Remove(Db.CLASSE.Find(ClasseID));
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClasseID"></param>
        /// <returns></returns>
        public Classe GetClasseByID(Guid ClasseID)
        {
            using (var Db = new EF())
            {
                return Db.CLASSE.Find(ClasseID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClasseName"></param>
        /// <returns></returns>
        public static Classe GetClasseByName(string ClasseName)
        {
            using (var Db = new EF())
            {
                var MyClasse = Db.CLASSE.SingleOrDefault(S => S.NAME == ClasseName);

                return MyClasse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Classe> GetAllClasse()
        {
            using (var Db = new EF())
            {
                return Db.CLASSE.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClasseID"></param>
        /// <returns></returns>
        public string GetClasseName(Guid ClasseID)
        {
            using (var Db = new EF())
            {
                return Db.CLASSE.Find(ClasseID).NAME;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClasseID"></param>
        /// <returns></returns>
        public bool ClasseExist(string ClasseID)
        {
            using (var Db = new EF())
            {
                return Db.CLASSE.Find(ClasseID) != null;
            }
        }

        /// <summary>
        /// Renvoi les matieres Enregistrees pour cette classe
        /// </summary>
        /// <param name="ClasseID"></param>
        /// <returns></returns>
        public List<Matiere> GetClassMatieres(Guid ClasseID)
        {
            using (var Db = new EF())
            {
                return Db.MATIERE.Where(M => M.CLASSE_ID == ClasseID).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public List<Staff> GetClassStaffs(Guid ClassID)
        {
            var Staffs = new List<Staff>();

            using (var Db = new EF())
            {
                foreach (var ST in Db.COURS.Where(C => C.CLASSE_ID == ClassID))
                {
                    Staffs.Add(Db.STAFF.Find(ST.STAFF_ID));
                }
                return Staffs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public List<Student> GetClassStudents(Guid ClassID)
        {
            var Students = new List<Student>();

            using (var Db = new EF())
            {
                foreach (var ST in Db.INSCRIPTION.Where(C => C.CLASSE_ID == ClassID))
                {
                    Students.Add(Db.STUDENT.Find(ST.STUDENT_ID));
                }
                return Students;
            }
        }

    }
}
