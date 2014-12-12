using DataService.Context;
using DataService.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataService.Entities.Pedagogy;

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
        /// <param name="myFiliere"></param>
        /// <returns></returns>
        public bool AddFiliere(Filiere myFiliere)
        {
            using (var db = new Ef())
            {
                db.Filiere.Add(myFiliere);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFiliere"></param>
        /// <returns></returns>
        public bool UpdateFiliere(Filiere myFiliere)
        {
            using (var db = new Ef())
            {
                db.Filiere.Attach(myFiliere);
                db.Entry(myFiliere).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public bool DeleteFiliere(Guid filiereId)
        {
            using (var db = new Ef())
            {
                db.Filiere.Remove(db.Filiere.Find(filiereId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public Filiere GetFiliereById(Guid filiereId)
        {
            using (var db = new Ef())
            {
                var myFiliere = db.Filiere.Find(filiereId);
                return myFiliere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereName"></param>
        /// <returns></returns>
        public Filiere GetFiliereByName(string filiereName)
        {
            using (var db = new Ef())
            {
                var myFiliere = db.Filiere.First(s => s.Name == filiereName);

                return myFiliere;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Filiere> GetAllFilieres()
        {
            using (var db = new Ef())
            {
                return db.Filiere.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAllFilieresNames()
        {
            var names = new List<string>();
            using (var db = new Ef())
            {
                names.AddRange(db.Filiere.Select(s => s.Name));
                return names;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public int GetFiliereClassCount(Guid filiereId)
        {
            using (var db = new Ef())
            {
                return db.Classe.Count(c => c.FiliereId == filiereId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public string GetFiliereName(Guid filiereId)
        {
            using (var db = new Ef())
            {
                var myFiliereName = db.Filiere.Find(filiereId).Name;
                return myFiliereName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public bool FiliereExist(Guid filiereId)
        {
            using (var db = new Ef())
            {
                return db.Filiere.Find(filiereId) != null;
            }
        }

    }
}
