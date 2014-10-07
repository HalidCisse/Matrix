using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Entities
{
    public class Student 
    {

        [Key]        
        public string STUDENT_ID { get; set; }

       
        public string TITLE { get; set; }

        [Required (ErrorMessage="Student First Name is Required")]
        public string FIRSTNAME { get; set; }

        [Required (ErrorMessage="Student Last Name is Required")]
        public string LASTNAME { get; set; }
        public byte[] PHOTO_IDENTITY { get; set; }        
        public string NATIONALITY { get; set; }
        public string IDENTITY_NUMBER { get; set; }


        //[Column ("BIRTH_DATE", TypeName="DateTime2")]
        public DateTime? BIRTH_DATE { get; set; }
        public string BIRTH_PLACE { get; set; }

        public string PHONE_NUMBER { get; set; }
        //[EmailAddress] 
        public string EMAIL_ADRESS { get; set; }
        public string HOME_ADRESS { get; set; }


        public DateTime? REGISTRATION_DATE { get; set; }
        public string STATUT { get; set; }


        #region Trans
        public string FULL_NAME
        {
            get
            {
                return FIRSTNAME + " " + LASTNAME;
            }
        }

        public string COURSE 
        {
            get
            {
                return "Software Developper ";
            }
        }

        public string LEVEL
        {
            get
            {
                return "1 ere Annnee";
            }
        }


        #endregion

    }
}
