using DataService.Context;
using DataService.Entities;

namespace DataService.Model
{
    public class ClassCard
    {
        public ClassCard ( Classe Clas )
        {
            CLASS_ID = Clas.CLASSE_ID;
            NAME = Clas.NAME;
            LEVEL = Clas.LEVEL;

            GetSTUDENTS_COUNT();
            GetINSTRUCTEURS_COUNT ();
            GetHEURE_PAR_SEMAINE ();
            GetMATIERES_COUNT();
        }

       
        public string CLASS_ID { get; set; }
         
        public string NAME { get; set; }

        public int LEVEL { get; set; }

        public int STUDENTS_COUNT { get; set; }

        public int INSTRUCTEURS_COUNT { get; set; }

        public string HEURES_PAR_SEMAINE { get; set; }

        public int MATIERES_COUNT { get; set; }


        #region Helpers

        private void GetSTUDENTS_COUNT ()
        {
            STUDENTS_COUNT = 10;
        }        
        private void GetINSTRUCTEURS_COUNT ()
        {
            using(var Db = new EF ())
            {
                INSTRUCTEURS_COUNT = 5; //Db.MATIERES_INSTRUCTEURS.Count (M => M.MATIERE_ID == MATIERE_ID);
            }
        }
        private void GetHEURE_PAR_SEMAINE ()
        {
            using(var Db = new EF ())
            {
                HEURES_PAR_SEMAINE = "2 Heures"; //Db.FILIERE_MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
            }
        }
        private void GetMATIERES_COUNT ()
        {
            MATIERES_COUNT = 10;
        }

        #endregion

    }
}
