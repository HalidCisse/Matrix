using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere
    {

        [Key]
        public string FILIERE_ID { get; set; }

        public string NAME { get; set; }

        public int NIVEAU { get; set; }
    }
}
