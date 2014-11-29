using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Ex : 1 ere Trimestre
    /// </summary>
    public class PeriodeScolaire
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid PERIODE_SCOLAIRE_ID { get; set; }

        /// <summary>
        /// Ex: 1 ere Trimestre 2013/2014
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// ID de l'Annee Scolaire
        /// </summary>
        public Guid ANNEE_SCOLAIRE_ID { get; set; }

        /// <summary>
        /// Date de Debut du Periode
        /// </summary>
        public DateTime? START_DATE { get; set; }



    }
}
