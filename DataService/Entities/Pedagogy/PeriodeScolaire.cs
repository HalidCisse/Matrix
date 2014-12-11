using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
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
        public Guid PeriodeScolaireId { get; set; }

        /// <summary>
        /// Ex: 1 ere Trimestre 2013/2014
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID de l'Annee Scolaire
        /// </summary>
        public Guid AnneeScolaireId { get; set; }

        /// <summary>
        /// Date de Debut du Periode
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Date de Debut du Periode
        /// </summary>
        public DateTime? EndDate { get; set; }



    }
}
