using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Definie Les Qualifications Pour pouvoir S inscrire a une Classe
    /// </summary>
    public class InscriptionRule
    {
        [Key]
        public Guid INSCRIPTION_RULE_ID { get; set; }

        public Guid CLASSE_ID { get; set; }

        public Guid QUALIFICATION_ID { get; set; }
        

    }
}
