using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Annee Scolaire ex : 2013-2014
    /// </summary>
    public class AnneeScolaire
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid ANNEE_SCOLAIRE_ID { get; set; }

        /// <summary>
        /// Ex: Spring 2013/2014
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Debut de l'Annee Scolaire
        /// </summary>
        public DateTime? DATE_DEBUT { get; set; }

        /// <summary>
        /// Fin de l'Annee Scolaire
        /// </summary>
        public DateTime? DATE_FIN { get; set; }
        
        /// <summary>
        /// Date Ou L'Inscription Est Ouvert
        /// </summary>
        public DateTime? DATE_DEBUT_INSCRIPTION { get; set; }

        /// <summary>
        /// Date de la Fermeture de l'Inscription
        /// </summary>
        public DateTime? DATE_FIN_INSCRIPTION { get; set; }
         
        /// <summary>
        /// Description
        /// </summary>
        public string DESCRIPTION { get; set; }

    }

}
