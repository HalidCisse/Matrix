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
        /// <param name="myCours"></param>
        /// <returns></returns>
        public bool AddCours(Cours myCours)
        {
            myCours.CoursId = Guid.NewGuid();
            using (var db = new Ef())
            {
                db.Cours.Add(myCours);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCours"></param>
        /// <returns></returns>
        public bool UpdateCours(Cours myCours)
        {
            using (var db = new Ef())
            {
                db.Cours.Attach(myCours);
                db.Entry(myCours).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        public bool DeleteCours(Guid coursId)
        {
            using (var db = new Ef())
            {
                db.Cours.Remove(db.Cours.Find(coursId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        public Cours GetCoursById(Guid coursId)
        {
            using (var db = new Ef())
            {
                var myCours = db.Cours.Find(coursId);
                return myCours;
            }
        }

    }
}
