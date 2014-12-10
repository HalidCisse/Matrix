using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Context;

namespace DataService.ViewModel
{

    /// <summary>
    /// Revoi Une le niveau d une filiere avec ses matieres deprecated
    /// </summary>
    public class FiliereLevelCard
    {

        public FiliereLevelCard ( Guid filiereId, int filiereYear )
        {
            FiliereId = filiereId;
            FormatYear(filiereYear);      
            //MATIERES_LIST = new List<MatiereCard> ();
            //GetMATIERES_LIST (FiliereYear);           
        }

        public Guid FiliereId { get; set; }

        public string FiliereYear { get; set; }

       // public List<MatiereCard> MATIERES_LIST { get; set; }


        #region HELPERS

        private void GetMATIERES_LIST (int filiereYear )
        {
            using(var db = new Ef ())
            {
                //var MatieresIDs = Db.FILIERE_MATIERE.Where (F => F.FILIERE_ID == FILIERE_ID && F.FILIERE_LEVEL == FiliereYear).Select (F => F.MATIERE_ID).ToList ();
               
                //foreach(var M in MatieresIDs.Select (M => Db.MATIERE.Find (M)))
                //{
                //    MATIERES_LIST.Add (new MatiereCard (FILIERE_ID, FiliereYear, M));
                //}   

                //foreach(var M in Db.MATIERE.Where (M => M.FILIERE_ID == FILIERE_ID && M.FILIERE_LEVEL == FiliereYear))
                //{
                //    MATIERES_LIST.Add (new MatiereCard (M));
                //} 
            }
        }

        private void FormatYear(int filiereYear)
        {
            this.FiliereYear = filiereYear == 1 ? "1 ere Annee" : filiereYear + " eme Annee";
        }

        #endregion

    }
}
