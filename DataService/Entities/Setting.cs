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
        public Guid SETTING_ID { get; set; }

        /// <summary>
        /// L'Utilisateur
        /// </summary>
        public Guid USER_ID { get; set; }

        /// <summary>
        /// Le Nom du Parametre
        /// </summary>
        public string SETTING_NAME { get; set; }

        /// <summary>
        /// La Valeur du Parametre
        /// </summary>
        public string SETTING_VALUE { get; set; }

        
    }
}
