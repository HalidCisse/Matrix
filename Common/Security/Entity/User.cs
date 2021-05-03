

using System;
using System.Collections.Generic;
using Common.Security.Enums;

namespace Common.Security.Entity
{
    /// <summary>
    /// Represente un utilisateur
    /// </summary>
    public class User 
    {

        /// <summary>
        /// UserGuid
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// PhotoIdentity
        /// </summary>
        public byte[] PhotoIdentity { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// EmailAdress
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// PasswordQuestion
        /// </summary>
        public string PasswordQuestion { get; set; }

        /// <summary>
        /// PasswordAnswer
        /// </summary>
        public string PasswordAnswer { get; set; }

	    /// <summary>
	    /// IsApproved
	    /// </summary>
	    public bool IsApproved { get; set; } = false;

        /// <summary>
	    /// isLockedOut
	    /// </summary>
	    public bool IsLockedOut { get; set; } = false;

        /// <summary>
	    /// IsOnline
	    /// </summary>
	    public bool IsOnline { get; set; } = false;

        ///// <summary>
        ///// CreationDate
        ///// </summary>
        //public UserSpace UserSpace { get; set; }

        /// <summary>
        /// Les Espace utilisateurs
        /// </summary>
        public List<KeyValuePair<string, Enum>>  UserSpaces { get; set; }

    }
}
