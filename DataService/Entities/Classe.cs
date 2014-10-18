using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Classe
    {
        [Key]
        public string CLASSE_ID { get; set; }

        public string NAME { get; set; }

        public int LEVEL { get; set; }

        
    }
}
