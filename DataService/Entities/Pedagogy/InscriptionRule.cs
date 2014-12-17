using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// Definie Les Qualifications Pour pouvoir S inscrire a une Classe
    /// </summary>
    public class InscriptionRule
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid InscriptionRuleGuid { get; set; }

        /// <summary>
        /// ID de la Classe
        /// </summary>
        public Guid ClasseGuid { get; set; }

        /// <summary>
        /// ID de la Classe
        /// </summary>
        public Guid QualificationGuid { get; set; }
        

    }
}
