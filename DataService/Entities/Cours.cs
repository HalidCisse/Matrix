using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Cours
    {

        [Key]
        public string COURS_ID { get; set; }

        public string MATIERE_ID { get; set; }

        public int SALLE { get; set; }

        public string CLASSE_ID { get; set; }

        public int START_TIME { get; set; }

        public int DURATION { get; set; }

    }
}
