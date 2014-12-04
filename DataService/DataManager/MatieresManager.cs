using DataService.Context;
using DataService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Matiere
    /// </summary>
    public class MatieresManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyMatiere"></param>
        /// <returns></returns>
        public bool AddMatiere(Matiere MyMatiere)
        {
            using (var Db = new EF())
            {
                Db.MATIERE.Add(MyMatiere);
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyMatiere"></param>
        /// <returns></returns>
        public bool UpdateMatiere(Matiere MyMatiere)
        {
            using (var Db = new EF())
            {
                Db.MATIERE.Attach(MyMatiere);
                Db.Entry(MyMatiere).State = System.Data.Entity.EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatiereID"></param>
        /// <returns></returns>
        public bool DeleteMatiere(Guid MatiereID)
        {
            using (var Db = new EF())
            {
                Db.MATIERE.Remove(Db.MATIERE.Find(MatiereID));
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatiereID"></param>
        /// <returns></returns>
        public Matiere GetMatiereByID(Guid MatiereID)
        {
            using (var Db = new EF())
            {
                var MyMatiere = Db.MATIERE.Find(MatiereID);
                return MyMatiere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatiereName"></param>
        /// <returns></returns>
        public static Matiere GetMatiereByName(string MatiereName)
        {
            using (var Db = new EF())
            {
                var MyMatiere = Db.MATIERE.SingleOrDefault(S => S.NAME == MatiereName);

                return MyMatiere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Matiere> GetAllMatieres()
        {
            using (var Db = new EF())
            {
                return Db.MATIERE.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerable GetAllMatieresNames()
        {
            using (var Db = new EF())
            {
                return Db.MATIERE.Select(M => M.NAME).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatiereID"></param>
        /// <returns></returns>
        public string GetMatiereName(Guid MatiereID)
        {
            using (var Db = new EF())
            {
                var MyMatiereName = Db.MATIERE.Find(MatiereID).NAME;
                return MyMatiereName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatiereName"></param>
        /// <returns></returns>
        public string GetMatiereIDFromName(string MatiereName)
        {
            using (var Db = new EF())
            {
                var MAT = Db.MATIERE.FirstOrDefault(M => M.NAME == MatiereName);
                return MAT == null ? null : MAT.MATIERE_ID.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MatiereID"></param>
        /// <returns></returns>
        public bool MatiereExist(Guid MatiereID)
        {
            using (var Db = new EF())
            {
                return Db.MATIERE.Find(MatiereID) != null;
            }
        }

    }
}
