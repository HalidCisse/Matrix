using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Parametres
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// ID de L'Utilisateur
        /// </summary> 
        [Key]
        public Guid UserProfileId { get; set; }
        
        

        
    }
}
