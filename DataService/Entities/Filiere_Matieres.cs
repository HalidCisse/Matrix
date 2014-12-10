using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class FiliereMatieresDep
    {
        [Key]
        public string FiliereMatiereId { get; set; } // FiliereID + FiliereLevel + MatiereID

        public string FiliereId { get; set; }    //ID de la filiere Consernee

        public int FiliereLevel { get; set; }  // 2 ere Annnee -> 2 eme Annnee

        public string MatiereId { get; set; }   //ID de la matiere Consernee

        public string HeureParSemaine { get; set; }  // 2 Heures, 2 Heures 30 min
    }
}
