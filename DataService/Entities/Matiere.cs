using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Matiere
    {

        [Key]
        public string MATIERE_ID { get; set; }

        public string NAME { get; set; }

    }
}
