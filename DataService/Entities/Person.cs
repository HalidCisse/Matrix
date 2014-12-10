using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    
    public class Person
    {
        [Key] 
        public string PersonId { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] PhotoIdentity { get; set; }

        public string Nationality { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public string PhoneNumber { get; set; }       
        public string EmailAdress { get; set; }
        public string HomeAdress { get; set; }

        public DateTime? RegistrationDate { get; set; }

        #region Trans

        public string FullName
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }

        #endregion


    }
}
