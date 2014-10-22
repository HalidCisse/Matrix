using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataService.Context;

namespace DataService.Entities
{
    public class Matiere
    {

        [Key]
        public string MATIERE_ID { get; set; }

        public string NAME { get; set; }
       
        public string GetHEURE_PAR_SEMAINE(string FiliereID, int FiliereLevel)
        {           
            using(var Db = new EF ())
            {
                return Db.FILIERE_MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
            }            
        }
    }
}
