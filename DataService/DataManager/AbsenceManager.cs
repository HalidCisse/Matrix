using System;
using System.Data.Entity;
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
            if (!myTicket.IsPresent || myTicket.RetardTime != new TimeSpan(0))
                return AbsenceTicketExist(myTicket) ? UpdateAbsenceTicket(myTicket) : AddAbsenceTicket(myTicket);
            DeleteAbsenceTicket(myTicket.AbsenceTicketGuid);
            return true;
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
            using (var db = new Ef())
            {
                if (db.AbsenceTicket.Find(absenceTicket.AbsenceTicketGuid) != null) return true;
                
                return db.AbsenceTicket.Any(t => t.CoursGuid == absenceTicket.CoursGuid && 
                                                 t.PersonGuid == absenceTicket.PersonGuid && 
                                                 t.CoursDate == absenceTicket.CoursDate);
            }
        }

        /// <summary>
        /// Verifie Si un Etudiant ou Staff est Present a un cours donnee
        /// </summary>       
        /// <param name="personGuid"></param>
        /// <param name="coursGuid"></param>
        /// <param name="coursDate"></param>
        /// <returns></returns>
        public static bool EstPresent(Guid personGuid, Guid coursGuid, DateTime coursDate)
        {           
            using (var db = new Ef())
            {
                var ticket = db.AbsenceTicket.FirstOrDefault(t => t.CoursGuid == coursGuid &&
                                                                          t.PersonGuid == personGuid &&
                                                                          t.CoursDate == coursDate);
                return ticket != null && ticket.IsPresent;
            }
        }


    }
}
