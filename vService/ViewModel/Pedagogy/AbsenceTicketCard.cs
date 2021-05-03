using System;
using System.Linq;
using Common.Pedagogy.Entity;
using Common.Shared.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// Model Presence 
    /// </summary>
    public class AbsenceTicketCard
    {
        /// <summary>
        ///  Model Presence 
        /// </summary>
        public AbsenceTicketCard(Guid personGuid, Guid coursGuid, DateTime coursDate )
        {
            coursDate = coursDate.Date;

            var currentTicket = GetTicket(personGuid, coursGuid, coursDate) ?? new AbsenceTicket
            {
                AbsenceTicketGuid = Guid.NewGuid(),
                PersonGuid        = personGuid,
                CoursGuid         = coursGuid,
                CoursDate         = coursDate         
            };

            if (coursDate > DateTime.Today)
            {
                currentTicket.IsPresent = false;
                currentTicket.RetardTime = new TimeSpan(0);
            }

            AbsenceTicketGuid = currentTicket.AbsenceTicketGuid;
            PersonGuid        = currentTicket.PersonGuid;            
            IsPresent         = currentTicket.IsPresent;
            RetardTime        = (int) currentTicket.RetardTime.TotalMinutes;

            using (var db = new SchoolContext())
            {                
                PhotoIdentity = db.Set<Person>().Find(PersonGuid)?.PhotoIdentity;
                FullName      =db.Set<Person>().Find(PersonGuid)?.FullName;
            }
        }

        /// <summary>
        /// Presence a un cours
        /// </summary>       
        public Guid AbsenceTicketGuid { get; }

        /// <summary>
        /// ID de la person => StudentGuid ou StaffGuid
        /// </summary>
        public Guid PersonGuid { get; }

        /// <summary>
        /// Photo de la Personne
        /// </summary>
        public byte[] PhotoIdentity { get; }

        /// <summary>
        /// Photo de la Personne
        /// </summary>
        public string FullName { get; }
       
        /// <summary>
        /// Est Present ?
        /// </summary>
        public bool IsPresent { get; set; }

        /// <summary>
        /// Temps de Retard 
        /// </summary>       
        public int RetardTime { get; set; }


       
        private static AbsenceTicket GetTicket (Guid personGuid, Guid coursGuid, DateTime coursDate)
        {
            using (var db = new SchoolContext())
                return
                    db.AbsenceTickets.FirstOrDefault(
                        t =>
                            t.PersonGuid.Equals(personGuid) && t.CoursGuid.Equals(coursGuid) &&
                            t.CoursDate.Value.Equals(coursDate));
        }



    }
}
