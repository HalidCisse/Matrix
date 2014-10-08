using System;

namespace DataService.Entities
{
    public abstract class Human
    {
        public string TITLE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public byte[] PHOTO_IDENTITY { get; set; }

        public string NATIONALITY { get; set; }
        public string IDENTITY_NUMBER { get; set; }
        public DateTime? BIRTH_DATE { get; set; }
        public string BIRTH_PLACE { get; set; }

        public string PHONE_NUMBER { get; set; }
        public string EMAIL_ADRESS { get; set; }
        public string HOME_ADRESS { get; set; }

        public DateTime? REGISTRATION_DATE { get; set; }

        public string FULL_NAME
        {
            get
            {
                return FIRSTNAME + " " + LASTNAME;
            }
        }

    }
}
