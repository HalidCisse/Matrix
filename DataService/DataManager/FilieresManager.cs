using DataService.Context;
using DataService.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Filieres
    /// </summary>
    public class FilieresManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyFiliere"></param>
        /// <returns></returns>
        public bool AddFiliere(Filiere MyFiliere)
        {
            MyFiliere.FILIERE_ID = Guid.NewGuid();
            using (var Db = new EF())
            {
                Db.FILIERE.Add(MyFiliere);
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyFiliere"></param>
        /// <returns></returns>
        public bool UpdateFiliere(Filiere MyFiliere)
        {
            using (var Db = new EF())
            {
                Db.FILIERE.Attach(MyFiliere);
                Db.Entry(MyFiliere).State = System.Data.Entity.EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiliereID"></param>
        /// <returns></returns>
        public bool DeleteFiliere(Guid FiliereID)
        {
            using (var Db = new EF())
            {
                Db.FILIERE.Remove(Db.FILIERE.Find(FiliereID));
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiliereID"></param>
        /// <returns></returns>
        public Filiere GetFiliereByID(Guid FiliereID)
        {
            using (var Db = new EF())
            {
                var MyFiliere = Db.FILIERE.Find(FiliereID);
                return MyFiliere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiliereName"></param>
        /// <returns></returns>
        public Filiere GetFiliereByName(string FiliereName)
        {
            using (var Db = new EF())
            {
                var MyFiliere = Db.FILIERE.First(S => S.NAME == FiliereName);

                return MyFiliere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Filiere> GetAllFilieres()
        {
            using (var Db = new EF())
            {
                return Db.FILIERE.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAllFilieresNames()
        {
            var Names = new List<string>();
            using (var Db = new EF())
            {
                Names.AddRange(Db.FILIERE.Select(S => S.NAME));
                return Names;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public int GetFiliereClassCount(Guid filiereId)
        {
            using (var Db = new EF())
            {
                return Db.CLASSE.Count(C => C.FILIERE_ID == filiereId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiliereID"></param>
        /// <returns></returns>
        public string GetFiliereName(Guid FiliereID)
        {
            using (var Db = new EF())
            {
                var MyFiliereName = Db.FILIERE.Find(FiliereID).NAME;
                return MyFiliereName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiliereID"></param>
        /// <returns></returns>
        public bool FiliereExist(Guid FiliereID)
        {
            using (var Db = new EF())
            {
                return Db.FILIERE.Find(FiliereID) != null;
            }
        }

    }
}
