using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Ex : 1 ere Trimestre
    /// </summary>
    public class SchoolPeriod {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid SchoolPeriodGuid { get; set; }

        /// <summary>
        /// Ex: 1 ere Trimestre 2013/2014
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID de l'Annee Scolaire
        /// </summary>
        public Guid SchoolYearGuid { get; set; }

        /// <summary>
        /// Date de Debut du Periode
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Date de Debut du Periode
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("SchoolYearGuid")]
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
