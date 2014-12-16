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
    public class UserRoles
    {
        /// <summary>
        /// ID L'Utilisateur
        /// </summary>
        [Key]
        public Guid UserProfileId { get; set; }
            
        /// <summary>
        /// Es ce que L'Utilisateur Peut Ajouter Un Etudiant ?
        /// </summary>
        public bool CanAddStudent { get; set; }

        /// <summary>
        /// Es ce que L'Utilisateur Peut Suprimer Un Etudiant ?
        /// </summary>
        public bool CanDeleteStudent { get; set; }



    }
}
