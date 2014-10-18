using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class MatiereControl
    {

        [Key]
        public string MATIERECONTROL_ID { get; set; }

        public string COURS_ID { get; set; }

        public DateTime? START_TIME { get; set; }

        public int DURATION { get; set; }


    }
}
