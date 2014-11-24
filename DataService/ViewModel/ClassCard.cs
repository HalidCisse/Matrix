using System;
using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    /// <summary>
    /// Un Model de classe
    /// </summary>
    public class ClassCard
    {
        /// <summary>
        /// Un Model de classe
        /// </summary>
        /// <param name="Clas">La classe</param>
        public ClassCard ( Classe Clas )
        {
            CLASS_ID = Clas.CLASSE_ID;
            NAME = Clas.NAME;
            LEVEL = Clas.LEVEL == 1 ? "1 ere Annee" : Clas.LEVEL + " eme Annee";

            //GetLEVEL (Clas.LEVEL);            
            //GetSTUDENTS_COUNT();
           // GetINSTRUCTEURS_COUNT ();
           // GetHEURE_PAR_SEMAINE ();
            //GetMATIERES_COUNT();
        }
       
        /// <summary>
        /// ID
        /// </summary>
        public Guid CLASS_ID { get; }
         
        /// <summary>
        /// Nomination
        /// </summary>
        public string NAME { get; }

        /// <summary>
        /// Level
        /// </summary>
        public string LEVEL { get; }

        /// <summary>
        /// Nombre d'etudiants
        /// </summary>
        public int STUDENTS_COUNT { get; } = 10;

        /// <summary>
        /// Nombre d'instructeurs
        /// </summary>
        public int INSTRUCTEURS_COUNT { get; } = 10;

        /// <summary>
        /// Nombre d'heure par semaine
        /// </summary>
        public string HEURES_PAR_SEMAINE { get; } = "10 Heures";

        /// <summary>
        /// Nombre de matieres
        /// </summary>
        public int MATIERES_COUNT { get; } = 10;


        
              
        private void GetINSTRUCTEURS_COUNT ()
        {
            using(var Db = new EF ())
            {
                var x = Db.COURS.Where(C => C.CLASSE_ID == CLASS_ID);

            }
        }
        private void GetHEURE_PAR_SEMAINE ()
        {
            using(var Db = new EF ())
            {
                // From Cours
               // HEURES_PAR_SEMAINE = "10 Heures"; //Db.FILIERE_MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
            }
        }
        private void GetMATIERES_COUNT ()
        {
            //From Cours
            //MATIERES_COUNT = 10;
        }
        private void GetLEVEL (int level)
        {
            
        }

        

    }
}
