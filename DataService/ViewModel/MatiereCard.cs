using System;
using System.Collections.Generic;
using System.Globalization;
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
            //COLOR = Mat.COULEUR;

            using (var Db = new EF())
            {
                var S_ID = Db.COURS.FirstOrDefault (C => C.MATIERE_ID == Mat.MATIERE_ID)?.STAFF_ID;

                INSTRUCTEUR_NAME = Db.STAFF?.Find(S_ID)?.FULL_NAME; //--------

                INSTRUCTEUR_PHOTO = Db.STAFF?.Find(S_ID)?.PHOTO_IDENTITY; //------

                var WeekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                var T = new TimeSpan(0,0,0);
                var scheduleDate = DateTime.Today.Date;

                foreach (var MC in Db.COURS.Where(C => C.MATIERE_ID == Mat.MATIERE_ID && C.START_DATE <= scheduleDate && C.END_DATE >= scheduleDate))
                {                             
                    var CoursDuree = MC.END_TIME.GetValueOrDefault().TimeOfDay - MC.START_TIME.GetValueOrDefault().TimeOfDay;

                    foreach (var D in WeekDays)
                    {
                        var DayNum = (int)D;

                        if (MC.RECURRENCE_DAYS.Contains(DayNum.ToString()))
                        {
                            T = T.Add(CoursDuree);
                        }
                    }
                    HEURES_PAR_SEMAINE = T.TotalHours.ToString(CultureInfo.InvariantCulture); //---
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
        /// Nomination
        /// </summary>
        //public string COLOR { get; }

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


       
    }
}
