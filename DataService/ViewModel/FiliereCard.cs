using System.Linq;
using DataService.Context;

namespace DataService.ViewModel
{
    public class FiliereCard
    {
        public FiliereCard (string FiliereID)
        {
            FILIERE_ID = FiliereID;         
        }
        public string FILIERE_ID { get; set; } 

        public string NAME { get; set; }

        public string NIVEAU { get; set; }

        public string NIVEAU_ENTREE { get; set; }

        public int N_ANNEE { get; set; }

        public int STAFFS_COUNT
        {
            get
            {
                using(var Db = new EF ())
                {
                    var CT = 0;

                    // ReSharper disable once LoopCanBeConvertedToQuery
                    foreach (var MID in Db.FILIERE_MATIERE.Where (S => S.FILIERE_ID == FILIERE_ID).Select(M => M.MATIERE_ID))
                    {
                        CT = CT + Db.MATIERES_INSTRUCTEURS.Count(M => M.MATIERE_ID == MID);
                    }
                    return CT;
                }
            }
        }

        public int STUDENTS_COUNT
        {
            get
            {                
                return 10;                
            }
        }

        public int CLASSES_COUNT
        {            
            get
            {
                
                return 12;
                
            }              
        }

        public int MATIERES_COUNT
        {
            get
            {
                using(var Db = new EF ())
                {
                    return Db.FILIERE_MATIERE.Count (S => S.FILIERE_ID == FILIERE_ID);
                }
            }           
        }


    }
}
