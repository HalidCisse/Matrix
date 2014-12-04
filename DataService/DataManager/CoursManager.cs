using DataService.Context;
using DataService.Entities;
using System;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion Des Cours
    /// </summary>
    public class CoursManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyCours"></param>
        /// <returns></returns>
        public bool AddCours(Cours MyCours)
        {
            MyCours.COURS_ID = Guid.NewGuid();
            using (var Db = new EF())
            {
                Db.COURS.Add(MyCours);
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MyCours"></param>
        /// <returns></returns>
        public bool UpdateCours(Cours MyCours)
        {
            using (var Db = new EF())
            {
                Db.COURS.Attach(MyCours);
                Db.Entry(MyCours).State = System.Data.Entity.EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CoursID"></param>
        /// <returns></returns>
        public bool DeleteCours(Guid CoursID)
        {
            using (var Db = new EF())
            {
                Db.COURS.Remove(Db.COURS.Find(CoursID));
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CoursID"></param>
        /// <returns></returns>
        public Cours GetCoursByID(Guid CoursID)
        {
            using (var Db = new EF())
            {
                var MyCours = Db.COURS.Find(CoursID);
                return MyCours;
            }
        }

    }
}
