using System;
using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    /// <summary>
    /// Model d'une matiere
    /// </summary>
    public class MatiereCard 
    {
        /// <summary>
        /// Model d'une matiere
        /// </summary>
        /// <param name="Mat"></param>
        public MatiereCard ( Matiere Mat )
        {
            MATIERE_ID = Mat.MATIERE_ID;
            NAME = Mat.NAME;
            COEFF = Mat.COEFFICIENT;


            using (var Db = new EF())
            {
                var S_ID = Db.COURS.First (C => C.MATIERE_ID == Mat.MATIERE_ID).STAFF_ID;

                INSTRUCTEUR_NAME = Db.STAFF?.Find(S_ID).FULL_NAME;

                INSTRUCTEUR_PHOTO = Db.STAFF?.Find(S_ID).PHOTO_IDENTITY;

                HEURES_PAR_SEMAINE = "";

                var T = new DateTime();

                foreach (var MC in Db.COURS.Where(C => C.MATIERE_ID == Mat.MATIERE_ID))
                {
                    if (MC.RECURRENCE_DAYS.Contains("1"))
                    {                       
                        T.Add((MC.END_TIME - MC.START_TIME).Value);

                    }
                }
                



            }  


        }

        /// <summary>
        /// 
        /// </summary>
        public Guid MATIERE_ID { get; }
         
        /// <summary>
        /// Nomination
        /// </summary>
        public string NAME { get; }

        /// <summary>
        /// Coefficient
        /// </summary>
        public int COEFF { get; }

        /// <summary>
        /// Heures par semaine
        /// </summary>
        public string HEURES_PAR_SEMAINE { get; }

        /// <summary>
        /// Nom de l'instructeur
        /// </summary>
        public string INSTRUCTEUR_NAME { get; }
        
        /// <summary>
        /// Photo de l'instructeur
        /// </summary>
        public byte[] INSTRUCTEUR_PHOTO { get; set; }


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
