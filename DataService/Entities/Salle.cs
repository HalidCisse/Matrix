using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Salle
    {

        [Key]
        public Guid SALLE_ID { get; set; }

        public string NAME { get; set; }

        public string ADRESSE { get; set; }


    }
}
