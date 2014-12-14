using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Parametres
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// ID de L'Utilisateur
        /// </summary> 
        [Key]
        public Guid UserProfileId { get; set; }
        
        /// <summary>
        /// Le Guid de l'annee Scolaire Actuelle
        /// </summary>
        public Guid CurrentAnneeScolaireGuid { get; set; }

        /// <summary>
        /// Le Guid de de la Periode l'annee Scolaire Actuelle
        /// </summary>
        public Guid CurrentPeriodeScolaireGuid { get; set; }

        
    }
}
