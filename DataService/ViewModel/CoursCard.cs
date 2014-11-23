using System;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    /// <summary>
    /// Les Informations d'un cours
    /// </summary>
    public class CoursCard
    {
        /// <summary>
        /// Les Informations s'un cours
        /// </summary>
        /// <param name="CurrentCous">Le Cours</param>
        /// <param name="CoursDay">La Journee De Recurrence</param>
        public CoursCard ( Cours CurrentCous, DateTime CoursDay )
        {
            TYPE = CurrentCous.TYPE.ToUpper ();
            COURS_ID = CurrentCous.COURS_ID;           
            
            SALLE = CurrentCous.SALLE.ToUpper();

            COURS_DAY = CoursDay.DayOfWeek;

            START_TIME = CurrentCous.START_TIME.GetValueOrDefault();
            END_TIME = CurrentCous.END_TIME.GetValueOrDefault();

            FORE_COULEUR = "Black";

            HORRAIRE = START_TIME.TimeOfDay.ToString("hh\\:mm") + " - " + END_TIME.TimeOfDay.ToString("hh\\:mm");

            ResolveData (CoursDay, CurrentCous.MATIERE_ID, CurrentCous.STAFF_ID);
            
        }

        /// <summary>
        /// ID du cours
        /// </summary>
        public Guid COURS_ID { get; set; }

        /// <summary>
        /// Nom de la matiere
        /// </summary>
        public string MATIERE_NAME { get; set; }

        /// <summary>
        /// Le Type du Cours: Cours, Control, Test, TP, TD Etc..
        /// </summary>
        public string TYPE { get; set; } 

        /// <summary>
        /// Le nom de l'instructeur
        /// </summary>
        public string STAFF_FULL_NAME { get; set; }
        
        /// <summary>
        /// La Salle ou le cours Sera Dispenser
        /// </summary>
        public string SALLE { get; set; }
        
        /// <summary>
        /// La Journee ou le cours sera dispenser
        /// </summary>
        public DayOfWeek COURS_DAY { get; set; }

        /// <summary>
        /// L'Horraire Formater
        /// </summary>
        public string HORRAIRE { get; set; }

        /// <summary>
        /// La couleur du cours
        /// </summary>
        public string COULEUR { get; set; }

        /// <summary>
        /// Forecolor
        /// </summary>
        public string FORE_COULEUR { get; set; }

        /// <summary>
        /// L'heure ou le cours commencera 
        /// </summary>
        public DateTime START_TIME { get; set; }

        /// <summary>
        /// L'heure ou le cours terminera 
        /// </summary>
        public DateTime END_TIME { get; set; }

        private void ResolveData (DateTime CoursDay, Guid MatiereID, string StaffID )
        {           
            using(var Db = new EF ())
            {
                var M = Db.MATIERE.Find(MatiereID);

                MATIERE_NAME = M.NAME;
                COULEUR = M.COULEUR;

                STAFF_FULL_NAME = Db.STAFF.Find (StaffID).FULL_NAME;
            }

            if (CoursDay < DateTime.Today)
            {
                FORE_COULEUR = "Gray";
            }
            else if(CoursDay == DateTime.Today)
            {
                if (END_TIME.TimeOfDay < DateTime.Now.TimeOfDay)
                {                                                            
                    FORE_COULEUR = "Gray";                    
                }

                if (START_TIME.TimeOfDay <= DateTime.Now.TimeOfDay && END_TIME.TimeOfDay >= DateTime.Now.TimeOfDay)
                {
                    FORE_COULEUR = "Red";
                }
            }
                      
        }

    }
}
