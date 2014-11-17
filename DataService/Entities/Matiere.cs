using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Matiere
    {

        [Key]
        public Guid MATIERE_ID { get; set; }

        public string NAME { get; set; }

        public string SIGLE { get; set; }
         
        public Guid CLASSE_ID { get; set; }
         
        //public Guid FILIERE_ID { get; set; }

        //public int FILIERE_LEVEL { get; set; }

        //public string HEURE_PAR_SEMAINE { get; set; }

        public int COEFFICIENT { get; set; }
         
        public string COULEUR { get; set; }

        public string DESCRIPTION { get; set; }

        
         
    }
}
