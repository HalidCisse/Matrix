﻿using System;
using Common.Pedagogy.Entity;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// Un Model de classe
    /// </summary>
    public class ClassCard
    {
        /// <summary>
        /// Un Model de classe
        /// </summary>
        /// <param name="clas">La classe</param>
        public ClassCard ( Classe clas )
        {
            ClassGuid = clas.ClasseGuid;
            Name = clas.Name;
            Level = (int) clas.ClassGrade;
                //clas.ClassAnnee == 1 ? "1 ere Annee" : clas.ClassAnnee + " eme Annee";

            //GetLEVEL (Clas.LEVEL);            
            //GetSTUDENTS_COUNT();
            // GetINSTRUCTEURS_COUNT ();
            // GetHEURE_PAR_SEMAINE ();
            //GetMATIERES_COUNT();
        }
       
        /// <summary>
        /// ID
        /// </summary>
        public Guid ClassGuid { get; }
         
        /// <summary>
        /// Nomination
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Level
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Nombre d'etudiants
        /// </summary>
        public int StudentsCount { get; } = 10;

        /// <summary>
        /// Nombre d'instructeurs
        /// </summary>
        public int InstructeursCount { get; } = 10;

        /// <summary>
        /// Nombre d'heure par semaine
        /// </summary>
        public string HeuresParSemaine { get; } = "10 Heures";

        /// <summary>
        /// Nombre de matieres
        /// </summary>
        public int MatieresCount { get; } = 10;


        
              
        //private void GetINSTRUCTEURS_COUNT ()
        //{
        //    using(var db = new SchoolContext ())
        //    {
        //        var x = db.Cours.Where(c => c.ClasseId == ClassId);

        //    }
        //}
        //private void GetHEURE_PAR_SEMAINE ()
        //{
        //    using(var db = new SchoolContext ())
        //    {
        //        // From Cours
        //       // HEURES_PAR_SEMAINE = "10 Heures"; //Db.FILIERE_MATIERE.Find (FiliereID + MATIERE_ID + FiliereLevel).HEURE_PAR_SEMAINE;
        //    }
        //}
        //private void GetMATIERES_COUNT ()
        //{
        //    //From Cours
        //    //MATIERES_COUNT = 10;
        //}
        //private void GetLevel (int level)
        //{
            
        //}

        

    }
}
