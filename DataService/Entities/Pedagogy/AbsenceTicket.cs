using System;
using System.ComponentModel.DataAnnotations;


namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// Ticket d'Absence ou de Retard
    /// </summary>
    public class AbsenceTicket
    {
        /// <summary>
        /// Presence a un cours
        /// </summary>
        [Key]
        public Guid AbsenceTicketGuid { get; set; }

        /// <summary>
        /// ID de la person => StudentGuid ou StaffGuid
        /// </summary>
        public Guid PersonGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CoursGuid { get; set; }

        /// <summary>
        /// La Date de La journee de Presence
        /// </summary>
        public  DateTime? CoursDate { get; set; }

        /// <summary>
        /// Bool Present ?
        /// </summary>
        public bool IsPresent  { get; set; } = true;

        /// <summary>
        /// Retard en Minute
        /// </summary>
        public TimeSpan RetardTime { get; set; } = new TimeSpan(0);
    }
}
