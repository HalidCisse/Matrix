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
        public Guid InscriptionGuid { get; set; }

        /// <summary>
        /// ID de l'Etudiant
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// ID de la Classe
        /// </summary>
        public Guid ClasseGuid { get; set; }

        /// <summary>
        /// L'Annee Scolaire de l'Inscription
        /// </summary>
        public Guid AnneeScolaireGuid { get; set; }

    }
}
