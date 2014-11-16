using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Etablissement
    {
        [Key]
        public Guid ETABLISSEMENT_ID { get; set; }

        public string NAME { get; set; }

        public byte[] LOGO { get; set; } 

        public string COUNTRY { get; set; }

        public string ADRESSE { get; set; }

        public string PHONE { get; set; }

        public string FAX { get; set; }
         
        public string DESCRIPTION { get; set; }

    }
}
