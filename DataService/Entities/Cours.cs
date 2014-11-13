using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Cours
    {

        [Key]
        public Guid COURS_ID { get; set; }

        public Guid CLASSE_ID { get; set; }

        public string STAFF_ID { get; set; }

        public Guid MATIERE_ID { get; set; }

        public Guid SALLE_ID { get; set; }
        
        public int DAY { get; set; }
         
        public DateTime START_TIME { get; set; }
         
        public DateTime END_TIME { get; set; }

        public DateTime START_DATE { get; set; }

        public DateTime END_DATE { get; set; }
         
        public string DESCRIPTION { get; set; }
    }
}
