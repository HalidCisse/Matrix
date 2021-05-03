using System;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class AssiduiteCard
    {
        /// <summary>
        /// Card de Rapport d'Absence
        /// </summary>        
        /// <param name="absenceTicket"></param>
        public AssiduiteCard(AbsenceTicket absenceTicket)
        {
            using (var db = new SchoolContext())
            {
                var theCours      = db.Studies.Find(absenceTicket.CoursGuid);
                AbsenceTicketGuid = absenceTicket.AbsenceTicketGuid;
                DateTimeString    = absenceTicket.CoursDate.GetValueOrDefault().ToShortDateString();
                if (theCours?.StartTime != null) CoursStartTime    = theCours.StartTime;
                if (theCours?.EndTime != null) CoursEndTime      = theCours.EndTime;
                if (theCours != null) MatiereName       = db.Subjects.Find(theCours.SubjectGuid).Name;
                PresenceColor     = "#B22400";

                if (!absenceTicket.IsPresent) Observation = "Absence Au Cours";
                else { Observation = "Retard de " + absenceTicket.RetardTime.TotalMinutes + " mins";
                    PresenceColor = "#707376";
                }
            }
        }

        /// <summary>
        /// AbsenceTicketGuid
        /// </summary>
        public Guid AbsenceTicketGuid { get; }

        /// <summary>
        /// DateTimeString
        /// </summary>
        public string DateTimeString { get; }

        /// <summary>
        /// CoursStartTime
        /// </summary>
        public TimeSpan CoursStartTime { get;}

        /// <summary>
        /// CoursEndTime
        /// </summary>
        public TimeSpan CoursEndTime { get;}

        /// <summary>
        /// MatiereName
        /// </summary>
        public string MatiereName { get; }

        /// <summary>
        /// PresenceColor
        /// </summary>
        public string PresenceColor { get; }

        /// <summary>
        /// Observation
        /// </summary>
        public string Observation { get; }

    }

}
