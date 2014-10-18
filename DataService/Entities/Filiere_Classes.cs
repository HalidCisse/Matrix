using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere_Classes
    {

        [Key]
        public string FILIERE_CLASSES_ID { get; set; }

        public string FILIERE_ID { get; set; }

        public string CLASSE_ID { get; set; }

    }
}
