using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Matiere_Instructeurs
    {

        [Key]
        public string MATIERE_INSTRUCTEURS_ID { get; set; }

        public string MATIERE_ID { get; set; }

        public string STAFF_ID { get; set; }

    }
}
