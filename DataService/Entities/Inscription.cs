using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Inscription
    /// </summary>
    public class Inscription
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid INSCRIPTION_ID { get; set; }

        /// <summary>
        /// ID de l'Etudiant
        /// </summary>
        public string STUDENT_ID { get; set; }

        /// <summary>
        /// ID de la Classe
        /// </summary>
        public Guid CLASSE_ID { get; set; }

        /// <summary>
        /// L'Annee Scolaire de l'Inscription
        /// </summary>
        public Guid ANNEE_SCOLAIRE_ID { get; set; }

    }
}
