using System;
using System.ComponentModel.DataAnnotations;
using DataService.Enum;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// Inscription
    /// </summary>
    public class Inscription
    {
        /// <summary>
        /// Inscription
        /// </summary>
        public Inscription()
        {
            InscriptionStatus = InscriptionStatus.Active;
            DateInscription = DateTime.Now;
            InscriptionGuid = Guid.NewGuid();
        }

        /// <summary>
        /// Guid
        /// </summary>
        [Key]
        public Guid InscriptionGuid { get; set; }

        /// <summary>
        /// Guid de l'Etudiant
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

        /// <summary>
        /// ID de l'Inscription
        /// </summary>
        public string InscriptionId { get; set; }

        /// <summary>
        /// Le status de l'Inscription
        /// </summary>
        public InscriptionStatus InscriptionStatus { get; set; }

        /// <summary>
        /// Date d'Inscription
        /// </summary>
        public DateTime? DateInscription { get; set; }

        /// <summary>
        /// Details de l'Inscription
        /// </summary>
        public string Description { get; set; }



    }
}
