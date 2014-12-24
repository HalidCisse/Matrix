using System;
using System.ComponentModel.DataAnnotations;


namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// Presence a un cours
    /// </summary>
    public class CoursPresence
    {

        /// <summary>
        /// Presence a un cours
        /// </summary>
        [Key]
        public Guid CoursPresenceGuid { get; set; }

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
        public bool Present  { get; set; }

        /// <summary>
        /// Retard en Minute
        /// </summary>
        public TimeSpan Retard { get; set; }




    }
}
