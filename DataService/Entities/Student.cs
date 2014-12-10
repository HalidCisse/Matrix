using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Student 
    {

        [Key]        
        public string StudentId { get; set; }
     
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] PhotoIdentity { get; set; }   
     
        public string Nationality { get; set; }
        public string IdentityNumber { get; set; }


        //[Column ("BIRTH_DATE", TypeName="DateTime2")]
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public string HomeAdress { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public string Statut { get; set; }


        #region Trans
        public string FullName
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }

        public string Course 
        {
            get
            {
                return "Software Developper ";
            }
        }

        public string Level
        {
            get
            {
                return "1 ere Annnee";
            }
        }


        #endregion

    }
}
