using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere_Matieres
    {
        [Key]
        public string FILIERE_MATIERE_ID { get; set; } // FiliereID + FiliereLevel + MatiereID

        public string FILIERE_ID { get; set; }    //ID de la filiere Consernee

        public string FILIERE_LEVEL { get; set; }  // 2 ere Annnee -> 2 eme Annnee

        public string MATIERE_ID { get; set; }   //ID de la matiere Consernee
        
    }
}
