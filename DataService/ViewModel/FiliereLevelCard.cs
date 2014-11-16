using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Context;
using DataService.Model;

namespace DataService.ViewModel
{
    public class FiliereLevelCard
    {

        public FiliereLevelCard ( Guid FiliereID, int FiliereYear )
        {
            FILIERE_ID = FiliereID;
            FormatYear(FiliereYear);      
            MATIERES_LIST = new List<MatiereCard> ();
            GetMATIERES_LIST (FiliereYear);           
        }

        public Guid FILIERE_ID { get; set; }

        public string FILIERE_YEAR { get; set; }

        public List<MatiereCard> MATIERES_LIST { get; set; }


        #region HELPERS

        private void GetMATIERES_LIST (int FiliereYear )
        {
            using(var Db = new EF ())
            {
                //var MatieresIDs = Db.FILIERE_MATIERE.Where (F => F.FILIERE_ID == FILIERE_ID && F.FILIERE_LEVEL == FiliereYear).Select (F => F.MATIERE_ID).ToList ();
               
                //foreach(var M in MatieresIDs.Select (M => Db.MATIERE.Find (M)))
                //{
                //    MATIERES_LIST.Add (new MatiereCard (FILIERE_ID, FiliereYear, M));
                //}   

                foreach(var M in Db.MATIERE.Where (M => M.FILIERE_ID == FILIERE_ID && M.FILIERE_LEVEL == FiliereYear))
                {
                    MATIERES_LIST.Add (new MatiereCard (M));
                } 
            }
        }

        private void FormatYear(int FiliereYear)
        {
            FILIERE_YEAR = FiliereYear == 1 ? "1 ere Annee" : FiliereYear + " eme Annee";
        }

        #endregion

    }
}
