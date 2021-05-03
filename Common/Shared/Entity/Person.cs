using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Comm.Entity;
using Common.Pedagogy.Entity;
using Common.Shared.Enums;

namespace Common.Shared.Entity
{  
    /// <summary>
    /// Une Personne
    /// </summary>
    public class Person: Tracable
    {
        /// <summary>
        /// Guid de la personne Associer
        /// </summary>
        [Key]
        public Guid PersonGuid { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public PersonTitles Title { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// PhotoIdentity
        /// </summary>
        public byte[] PhotoIdentity { get; set; }

        /// <summary>
        /// HealthState
        /// </summary>
        public HealthStates HealthState { get; set; }

        /// <summary>
        /// Nationality
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// IdentityNumber
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// BirthDate
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// BirthPlace
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// EmailAdress
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// HomeAdress
        /// </summary>
        public string HomeAdress { get; set; }

        /// <summary>
        /// RegistrationDate
        /// </summary>
        public DateTime? RegistrationDate { get; set; } //todo deprecate

        /// <summary>
        /// FullName
        /// </summary>
        public string FullName => FirstName.Substring(0,1).ToUpper() + FirstName.Substring(1).ToLower() + " " + LastName.Substring(0, 1).ToUpper() + LastName.Substring(1).ToLower();




        /// <summary>
        /// les Tickets d'absence de la personne
        /// </summary>
        public virtual ICollection<AbsenceTicket> AbsenceTickets { get; set; } = new HashSet<AbsenceTicket>();
        /// <summary>
        /// Les Documents de la personne
        /// </summary>
        public virtual ICollection<Document> Documents { get; set; } = new HashSet<Document>();
        /// <summary>
        /// Les Conversation dont il a Participer
        /// </summary>
        public virtual ICollection<Chat> Chats { get; set; }= new HashSet<Chat>();
    }
}
