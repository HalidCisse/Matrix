using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Represente un cours
    /// </summary>
    public class Cours
    {

        /// <summary>
        /// ID du Cours
        /// </summary>
        [Key]
        public Guid CoursId { get; set; }

        /// <summary>
        /// ID de la classe
        /// </summary>
        public Guid ClasseId { get; set; }

        /// <summary>
        /// ID du staff qui va dispenser le cours
        /// </summary>
        public string StaffId { get; set; }      

        /// <summary>
        /// ID du matiere qui sera enseigner
        /// </summary>
        public Guid MatiereId { get; set; }

        /// <summary>
        /// La salle ou le cours sera dispenser
        /// </summary>
        public string Salle { get; set; }
           
        /// <summary>
        /// La recurrence du cours
        /// </summary>
        public string RecurrenceDays { get; set; }

        /// <summary>
        /// Heure de debut du cours
        /// </summary>
        public DateTime? StartTime { get; set; }
         
        /// <summary>
        /// Heure de fin du cours
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Debut de la periode du cours
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Fin de la periode du cours
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Control, Cours, Examen, Test, Revision, Travaux Pratiques, Travaux Dirigés
        /// </summary>
        public string Type { get; set; }

        //Todo: PERIODE SCOLAIRE Integration To Cours
        /// <summary>
        /// La periode du cours
        /// </summary>
        public Guid PeriodeScolaireId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
