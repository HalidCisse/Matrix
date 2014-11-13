using System;
using System.ComponentModel.DataAnnotations;


namespace DataService.Entities
{
    public class AnneeScolaire
    {
        [Key]
        public Guid ANNEE_SCOLAIRE_ID { get; set; }

        public string NAME { get; set; }

        /// <summary>
        /// Debut de l Annee Scolaire
        /// </summary>
        public DateTime DATE_DEBUT { get; set; }

        /// <summary>
        /// Fin de l Annee Scolaire
        /// </summary>
        public DateTime DATE_FIN { get; set; }
        
        public DateTime DATE_DEBUT_INSCRIPTION { get; set; }

        public DateTime DATE_FIN_INSCRIPTION { get; set; }
         
        public string DESCRIPTION { get; set; }

    }

}
