using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities
{
    /// <summary>
    /// Parametres
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// ID
        /// </summary> 
        [Key]
        public Guid SettingId { get; set; }

        /// <summary>
        /// L'Utilisateur
        /// </summary>
        public Guid UserProfileId { get; set; }

        /// <summary>
        /// Le Nom du Parametre
        /// </summary>
        public int SettingNum { get; set; }

        /// <summary>
        /// La Valeur du Parametre
        /// </summary>
        public string SettingValue { get; set; }

        
    }
}
