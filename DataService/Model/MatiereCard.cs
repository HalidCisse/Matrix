using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService.Model
{
    public class MatiereCard 
    {
        public MatiereCard (string FiliereID, int FiliereLevel,  Matiere Mat )
        {
            MATIERE_ID = Mat.MATIERE_ID;
            NAME = Mat.NAME;
            HEURES_PAR_SEMAINE = GetHEURE_PAR_SEMAINE (FiliereID, FiliereLevel);
            INSTRUCTEURS_COUNT = GetINSTRUCTEURS_COUNT();
        }


        public string MATIERE_ID { get; set; }
         
        public string NAME { get; set; }

        public string HEURES_PAR_SEMAINE { get; set; }
        
        public int INSTRUCTEURS_COUNT { get; set; }


        #region Helpers
        public string GetHEURE_PAR_SEMAINE ( string FiliereID, int FiliereLevel )
        {
            using(var Db = new EF ())
            {
                return Db.FILIERE_MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
            }
        }

        public int GetINSTRUCTEURS_COUNT ()
        {
            using(var Db = new EF ())
            {
                return Db.MATIERES_INSTRUCTEURS.Count (M => M.MATIERE_ID == MATIERE_ID);
            }
        }

        #endregion
    }
}
