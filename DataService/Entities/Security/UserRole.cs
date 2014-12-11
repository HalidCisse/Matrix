using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities.Security
{
    /// <summary>
    /// Le Role De L'Utilisateur
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid UserRoleIdGuid { get; set; }
        
        /// <summary>
        /// L'Utilisateur
        /// </summary>
        public Guid UserProfileId { get; set; }

        /// <summary>
        /// Espace de L'Utilisateur
        /// </summary>
        public string UserSpace { get; set; }



    }
}
