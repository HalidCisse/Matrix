using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Matiere_Instructeurs
    {

        [Key]
        public Guid MATIERE_INSTRUCTEURS_ID { get; set; } // MATIERE_ID + STAFF_ID

        public Guid MATIERE_ID { get; set; }

        public String STAFF_ID { get; set; }

    }
}
