using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Classe
    {
        [Key]
        public Guid CLASSE_ID { get; set; }

        public string NAME { get; set; }

        public string FILIERE_ID { get; set; }

        public int LEVEL { get; set; }
        
    }
}
