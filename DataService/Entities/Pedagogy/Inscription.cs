using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
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
        public Guid InscriptionId { get; set; }

        /// <summary>
        /// ID de l'Etudiant
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// ID de la Classe
        /// </summary>
        public Guid ClasseId { get; set; }

        /// <summary>
        /// L'Annee Scolaire de l'Inscription
        /// </summary>
        public Guid AnneeScolaireId { get; set; }

    }
}
