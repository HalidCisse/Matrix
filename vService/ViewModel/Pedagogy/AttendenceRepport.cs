using System;
using System.Collections.Generic;
using System.Linq;
using CLib;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy {
    internal class AttendenceRepport {

        public AttendenceRepport(Guid personGuid, DateTime? fromDate, DateTime? toDate)
        {
            using (var db = new SchoolContext()) {
                var personTickets = db.AbsenceTickets.Where(tk =>
                                                                tk.PersonGuid==personGuid&&
                                                                (
                                                                    tk.CoursDate>=fromDate&&
                                                                    tk.CoursDate<=toDate
                                                                )
                                                          ).OrderBy(tk => tk.CoursDate).ToList();

                foreach(var personTicket in personTickets)
                    TicketsCardList.Add(new AssiduiteCard(personTicket));
               
                PersonFullName =db.Staffs.Find(personGuid)?.Person.FullName??db.Staffs.Find(personGuid)?.Person.FullName;
                PhotoIdentity = db.Staffs.Find(personGuid)?.Person.PhotoIdentity??db.Staffs.Find(personGuid)?.Person.PhotoIdentity;

                foreach(var ticket in personTickets) {
                    if(!ticket.IsPresent)
                        TotalAbsences=TotalAbsences+1;
                    else {
                        TotalRetards=TotalRetards+1;
                        TotalRatardTime=TotalRatardTime.Add(ticket.RetardTime);
                    }
                }

                TotalDescription=TotalAbsences>0 ? "Absences "+TotalAbsences+" fois,  " : "Aucune Absence,  ";

                TotalDescription=TotalRetards>0
                    ? TotalDescription+"Retards "+TotalRetards+" fois en "
                      +TotalRatardTime.ProperTimeSpan()+" mins"
                    : TotalDescription+"Aucun Retard";
            }
        }

       
        /// <summary>
        /// Le nom de la Personne
        /// </summary>
        public string PersonFullName { get; }


        /// <summary>
        /// 
        /// </summary>
        public byte[] PhotoIdentity { get; }


        /// <summary>
        /// Nombre total d'Absences
        /// </summary>
        public int TotalAbsences { get; }


        /// <summary>
        /// Nombre Total de Retards
        /// </summary>
        public int TotalRetards { get; }


        /// <summary>
        /// La somme de tous les temps de retards
        /// </summary>
        public TimeSpan TotalRatardTime { get; } = new TimeSpan(0);


        /// <summary>
        /// TrimestreTotal
        /// </summary>
        public string TotalDescription { get; }
        

        /// <summary>
        /// La list des tickets d'absences
        /// </summary>
        public HashSet<AssiduiteCard> TicketsCardList { get; } = new HashSet<AssiduiteCard>();

    }
}
