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
        public Guid UserProfileId { get; set; }

        /// <summary>
        /// Les Droits D'Access
        /// </summary>
        public Guid UserRoleGuid { get; set; }

        /// <summary>
        /// Admin, Staff, Student
        /// </summary>
        public string UserType { get; set; }

    }
}
