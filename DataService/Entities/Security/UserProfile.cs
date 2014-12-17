using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// ID du Profil
        /// </summary>
        [Key]
        public Guid UserProfileGuid { get; set; }

        /// <summary>
        /// Admin, Staff, Student
        /// </summary>
        public UserSpace UserSpace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Statut { get; set; }

    }
}
