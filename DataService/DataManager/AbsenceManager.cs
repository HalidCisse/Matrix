using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using DataService.Context;
using DataService.Entities.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class AbsenceManager
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTicket"></param>
        /// <returns></returns>
        public bool AddOrUpdateAbsenceTicket(AbsenceTicket myTicket)
        {
            return AbsenceTicketExist(myTicket) ? UpdateAbsenceTicket(myTicket) : AddAbsenceTicket(myTicket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTicket"></param>
        /// <returns></returns>
        private static bool AddAbsenceTicket(AbsenceTicket myTicket)
        {
            if (!myTicket.IsPresent) myTicket.RetardTime = new TimeSpan(0, 0, 0, 0);

            using (var db = new Ef())
            {
                db.AbsenceTicket.Add(myTicket);
                return db.SaveChanges() > 0;
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTicket"></param>
        /// <returns></returns>
        private static bool UpdateAbsenceTicket(AbsenceTicket myTicket)
        {
            if (!myTicket.IsPresent) myTicket.RetardTime = new TimeSpan(0, 0, 0, 0);

            using (var db = new Ef())
            {
                db.AbsenceTicket.Attach(myTicket);
                db.Entry(myTicket).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myTicketGuid"></param>
        /// <returns></returns>
        private bool DeleteAbsenceTicket(Guid myTicketGuid)
        {
            using (var db = new Ef())
            {
                db.AbsenceTicket.Remove(db.AbsenceTicket.Find(myTicketGuid));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="absenceTicket"></param>        
        /// <returns></returns>
        private static bool AbsenceTicketExist(AbsenceTicket absenceTicket)
        {
            //absenceTicket.CoursDate = DateTime.Parse(absenceTicket.CoursDate.GetValueOrDefault().Date.ToString(CultureInfo.InvariantCulture));
            
            using (var db = new Ef())
            {
                if (db.AbsenceTicket.Find(absenceTicket.AbsenceTicketGuid) != null) return true;
                
                return db.AbsenceTicket.Any(t => t.CoursGuid == absenceTicket.CoursGuid && 
                                                 t.PersonGuid == absenceTicket.PersonGuid && 
                                                 t.CoursDate == absenceTicket.CoursDate);
            }
        }



    }
}
