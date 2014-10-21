using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Matiere_Instructeurs
    {

        [Key]
        public string MATIERE_INSTRUCTEURS_ID { get; set; } // MATIERE_ID + STAFF_ID

        public string MATIERE_ID { get; set; }

        public string STAFF_ID { get; set; }

    }
}
