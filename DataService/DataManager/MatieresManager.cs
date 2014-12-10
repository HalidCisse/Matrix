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
        /// <param name="myMatiere"></param>
        /// <returns></returns>
        public bool AddMatiere(Matiere myMatiere)
        {
            using (var db = new Ef())
            {
                db.Matiere.Add(myMatiere);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMatiere"></param>
        /// <returns></returns>
        public bool UpdateMatiere(Matiere myMatiere)
        {
            using (var db = new Ef())
            {
                db.Matiere.Attach(myMatiere);
                db.Entry(myMatiere).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereId"></param>
        /// <returns></returns>
        public bool DeleteMatiere(Guid matiereId)
        {
            using (var db = new Ef())
            {
                db.Matiere.Remove(db.Matiere.Find(matiereId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereId"></param>
        /// <returns></returns>
        public Matiere GetMatiereById(Guid matiereId)
        {
            using (var db = new Ef())
            {
                var myMatiere = db.Matiere.Find(matiereId);
                return myMatiere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereName"></param>
        /// <returns></returns>
        public static Matiere GetMatiereByName(string matiereName)
        {
            using (var db = new Ef())
            {
                var myMatiere = db.Matiere.SingleOrDefault(s => s.Name == matiereName);

                return myMatiere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Matiere> GetAllMatieres()
        {
            using (var db = new Ef())
            {
                return db.Matiere.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerable GetAllMatieresNames()
        {
            using (var db = new Ef())
            {
                return db.Matiere.Select(m => m.Name).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereId"></param>
        /// <returns></returns>
        public string GetMatiereName(Guid matiereId)
        {
            using (var db = new Ef())
            {
                var myMatiereName = db.Matiere.Find(matiereId).Name;
                return myMatiereName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereName"></param>
        /// <returns></returns>
        public string GetMatiereIdFromName(string matiereName)
        {
            using (var db = new Ef())
            {
                var mat = db.Matiere.FirstOrDefault(m => m.Name == matiereName);
                return mat == null ? null : mat.MatiereId.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereId"></param>
        /// <returns></returns>
        public bool MatiereExist(Guid matiereId)
        {
            using (var db = new Ef())
            {
                return db.Matiere.Find(matiereId) != null;
            }
        }

    }
}
