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
        public Guid COURS_ID { get; set; }

        /// <summary>
        /// ID de la classe
        /// </summary>
        public Guid CLASSE_ID { get; set; }

        /// <summary>
        /// ID du staff qui va dispenser le cours
        /// </summary>
        public string STAFF_ID { get; set; }      

        /// <summary>
        /// ID du matiere qui sera enseigner
        /// </summary>
        public Guid MATIERE_ID { get; set; }

        /// <summary>
        /// La salle ou le cours sera dispenser
        /// </summary>
        public string SALLE { get; set; }
           
        /// <summary>
        /// La recurrence du cours
        /// </summary>
        public string RECURRENCE_DAYS { get; set; }

        /// <summary>
        /// Heure de debut du cours
        /// </summary>
        public DateTime? START_TIME { get; set; }
         
        /// <summary>
        /// Heure de fin du cours
        /// </summary>
        public DateTime? END_TIME { get; set; }

        /// <summary>
        /// Debut de la periode du cours
        /// </summary>
        public DateTime? START_DATE { get; set; }

        /// <summary>
        /// Fin de la periode du cours
        /// </summary>
        public DateTime? END_DATE { get; set; }

        /// <summary>
        /// Control, Cours, Examen, Test, Revision, Travaux Pratiques, Travaux Dirigés
        /// </summary>
        public string TYPE { get; set; }

        //Todo: PERIODE SCOLAIRE Integration To Cours
        /// <summary>
        /// La periode du cours
        /// </summary>
        public Guid PERIODE_SCOLAIRE_ID { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string DESCRIPTION { get; set; }
    }
}
