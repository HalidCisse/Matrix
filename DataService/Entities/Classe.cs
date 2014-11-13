using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Classe
    {
        [Key]
        public Guid CLASSE_ID { get; set; }

        public string NAME { get; set; }

        public Guid FILIERE_ID { get; set; }

        public int LEVEL { get; set; }

        public string DESCRIPTION { get; set; }

        public Guid ANNEE_SCOLAIRE_ID { get; set; }

        
    }
}
