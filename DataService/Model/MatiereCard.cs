using System;
using DataService.Entities;

namespace DataService.Model
{
    public class MatiereCard 
    {
        public MatiereCard ( Matiere Mat )
        {
            MATIERE_ID = Mat.MATIERE_ID;
            NAME = Mat.NAME;
            //HEURES_PAR_SEMAINE = Mat.HEURE_PAR_SEMAINE; //GetHEURE_PAR_SEMAINE (FiliereID, FiliereLevel);
            //INSTRUCTEURS_COUNT = GetINSTRUCTEURS_COUNT();
        }


        public Guid MATIERE_ID { get; set; }
         
        public string NAME { get; set; }

        public string HEURES_PAR_SEMAINE { get; set; }

        public string INSTRUCTEUR_NAME { get; set; }
        
        //public int INSTRUCTEURS_COUNT { get; set; }


        #region Helpers

        //public string GetINSTRUCTEUR_NAME ( )
        //{
        //    using(var Db = new EF ())
        //    {
        //        var S_ID = Db.COURS.First (C => C.MATIERE_ID == MATIERE_ID).STAFF_ID;
        //        return Db.STAFF != null ? Db.STAFF.Find(S_ID).FULL_NAME : null;
        //    }            
        //}


        //public string GetHEURE_PAR_SEMAINE ( Guid FiliereID, int FiliereLevel )
        //{
        //    using(var Db = new EF ())
        //    {
        //        return Db.MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
        //    }
        //}

        //public int GetINSTRUCTEURS_COUNT ()
        //{
        //    using(var Db = new EF ())
        //    {
        //        return Db.MATIERES_INSTRUCTEURS.Count (M => M.MATIERE_ID == MATIERE_ID);
        //    }
        //}

        #endregion
    }
}
