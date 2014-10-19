using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere
    {

        [Key]
        public string FILIERE_ID { get; set; } // --> NAME + NIVEAU

        public string NAME { get; set; } //Genie Informatique

        public string NIVEAU { get; set; } //Licence -> Licence Professionelle -> Technicien -> Technicien Specialise -> Master -> Ingenieur -> Ingenieur Etat -> Doctorat

        public string NIVEAU_ENTREE { get; set; }

        public int N_ANNEE { get; set; }  // 5 ans
    }
}
