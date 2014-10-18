using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere_Matieres
    {
        [Key]
        public string FILIERE_MATIERE_ID { get; set; }

        public string MATIERE_ID { get; set; }

        public string FILIERE_ID { get; set; }

    }
}
