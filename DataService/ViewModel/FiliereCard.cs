using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    public class FiliereCard
    {
        public FiliereCard (Filiere FL)
        {
            FILIERE_ID = FL.FILIERE_ID;
            NAME = FL.NAME.ToUpper();
            NIVEAU = FL.NIVEAU;
            NIVEAU_ENTREE = FL.NIVEAU_ENTREE;
            N_ANNEE = FL.N_ANNEE;
            GetCLASSES_COUNT();
        }

        public string FILIERE_ID { get; set; } 

        public string NAME { get; set; }

        public string NIVEAU { get; set; }

        public string NIVEAU_ENTREE { get; set; }

        public int N_ANNEE { get; set; }

        public int STAFFS_COUNT { get; set; }

        public int STUDENTS_COUNT { get; set; }

        public int CLASSES_COUNT { get; set; }
       
        public int MATIERES_COUNT { get; set; }



        #region HELPERS

        private void GetCLASSES_COUNT()
        {
            using (var Db = new EF())
            {
                CLASSES_COUNT = Db.CLASSE.Count(C => C.FILIERE_ID == FILIERE_ID);        
            }
        }

        #endregion

    }
}
