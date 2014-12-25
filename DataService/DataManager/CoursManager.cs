using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;
using DataService.Enum;

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
            myCours.CoursGuid = Guid.NewGuid();
            myCours.PeriodeScolaireGuid = GetCurrentPeriodeScolaireGuid;
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
                db.Entry(myCours).State = EntityState.Modified;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public HashSet<Cours> GetCoursBetween(Guid classeGuid, DateTime startDate, DateTime endDate)
        {
            using (var db = new Ef())
            {
                return new HashSet<Cours>(db.Cours.Where(c =>

                    c.ClasseGuid == classeGuid &&
                    (
                        (
                            c.StartDate <= startDate &&
                            c.EndDate >= endDate
                            )
                        |
                        (
                            c.EndDate >= startDate &&
                            c.EndDate <= endDate
                            )
                        |
                        (
                            c.StartDate >= startDate &&
                            c.StartDate <= endDate
                            )
                        )

                    ).OrderBy(c => c.StartTime));
            }
        }




        #region Helpers

        private static Guid GetCurrentPeriodeScolaireGuid
        {
            get 
            {
                using (var db = new Ef())
                {                   
                    var psList = db.PeriodeScolaire.Where(ps => ps.AnneeScolaireGuid.Equals(GetCurrentAnneeScolaireGuid));

                    foreach (var ps in psList) if (ps.StartDate <= DateTime.Today && DateTime.Today <= ps.EndDate) return ps.PeriodeScolaireGuid;
                   
                    return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;
                }
            }
        }

        private static Guid GetCurrentAnneeScolaireGuid
        {
            get
            {
                using (var db = new Ef())
                {
                    if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null)
                        db.MatrixSetting.Add(new MatrixSetting());

                    return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
                }
            }
        }


        #endregion


        }
}
