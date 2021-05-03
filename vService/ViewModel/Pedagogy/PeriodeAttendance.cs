using System;
using System.Collections.Generic;
using System.Linq;
using CLib;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// Le Rapport d'Assudite Par Periode
    /// </summary>
    public class PeriodeAttendance
    {

        /// <summary>
        /// Le Rapport d'Assudite Par Periode
        /// </summary>
        public PeriodeAttendance(Guid personGuid, SchoolPeriod schoolPeriod)
        {            
            using (var db = new SchoolContext())
            {
                var personTickets = db.AbsenceTickets.Where(tk =>
                                                                tk.PersonGuid == personGuid &&
                                                                (
                                                                    tk.CoursDate >= schoolPeriod.StartDate &&
                                                                    tk.CoursDate <= schoolPeriod.EndDate
                                                                )
                                                          ).OrderBy(tk => tk.CoursDate).ToList();

                foreach (var personTicket in personTickets) TicketsCardList.Add(new AssiduiteCard(personTicket));

                PeriodeName = schoolPeriod.Name.ToUpper();

                PeriodeTime = "  (" + schoolPeriod.StartDate.GetValueOrDefault().ToShortDateString() + " -> " +
                              schoolPeriod.EndDate.GetValueOrDefault().ToShortDateString() + ")";

                PersonFullName = db.Students.Find(personGuid)?.Person.FullName ?? db.Staffs.Find(personGuid)?.Person.FullName;

                foreach (var ticket in personTickets)
                {
                    if (!ticket.IsPresent) TotalAbsences = TotalAbsences + 1;
                    else {                    
                        TotalRetards = TotalRetards + 1;
                        TotalRatardTime = TotalRatardTime.Add(ticket.RetardTime);
                    }                                       
                }

                TrimestreTotal = TotalAbsences > 0 ? "Absences " + TotalAbsences + " fois,  " : "Aucune Absence,  ";

                TrimestreTotal = TotalRetards > 0
                    ? TrimestreTotal + "Retards " + TotalRetards + " fois en "
                      + TotalRatardTime.ProperTimeSpan() + " mins"
                    : TrimestreTotal + "Aucun Retard";
            }
        }



        /// <summary>
        /// PeriodeName
        /// </summary>
        public string PeriodeName { get; }


        /// <summary>
        /// PeriodeTime
        /// </summary>
        public string PeriodeTime { get; }


        /// <summary>
        /// Le nom de la Personne
        /// </summary>
        public string PersonFullName { get; }


        /// <summary>
        /// Nombre total d'Absences
        /// </summary>
        public int TotalAbsences { get; }


        /// <summary>
        /// Nombre Total de Retards
        /// </summary>
        public int TotalRetards { get;}


        /// <summary>
        /// La somme de tous les temps de retards
        /// </summary>
        public TimeSpan TotalRatardTime { get; } = new TimeSpan(0);


        /// <summary>
        /// TrimestreTotal
        /// </summary>
        public string TrimestreTotal { get; }


        /// <summary>
        /// La list des tickets d'absences
        /// </summary>
        public HashSet<AssiduiteCard> TicketsCardList { get; } = new HashSet<AssiduiteCard>();

    }
}
