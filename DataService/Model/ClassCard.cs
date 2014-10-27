using System;
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
          
            GetLEVEL (Clas.LEVEL);            
            GetSTUDENTS_COUNT();
            GetINSTRUCTEURS_COUNT ();
            GetHEURE_PAR_SEMAINE ();
            GetMATIERES_COUNT();
        }

       
        public Guid CLASS_ID { get; set; }
         
        public string NAME { get; set; }

        public string LEVEL { get; set; }

        public int STUDENTS_COUNT { get; set; }

        public int INSTRUCTEURS_COUNT { get; set; }

        public string HEURES_PAR_SEMAINE { get; set; }

        public int MATIERES_COUNT { get; set; }


        #region Helpers

        private void GetSTUDENTS_COUNT ()
        {
            //From Cours => Class => Guys
            STUDENTS_COUNT = 10;
        }        
        private void GetINSTRUCTEURS_COUNT ()
        {
            using(var Db = new EF ())
            {
                //Class Cours proffs
                INSTRUCTEURS_COUNT = 10; //Db.MATIERES_INSTRUCTEURS.Count (M => M.MATIERE_ID == MATIERE_ID);
            }
        }
        private void GetHEURE_PAR_SEMAINE ()
        {
            using(var Db = new EF ())
            {
                // From Cours
                HEURES_PAR_SEMAINE = "10 Heures"; //Db.FILIERE_MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
            }
        }
        private void GetMATIERES_COUNT ()
        {
            //From Cours
            MATIERES_COUNT = 10;
        }

        private void GetLEVEL (int level)
        {
            LEVEL = level == 1 ? "1 ere Annee" : level + " eme Annee";
        }

        #endregion

    }
}
