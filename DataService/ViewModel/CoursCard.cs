using System;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;

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
        /// <param name="currentCous">Le Cours</param>
        /// <param name="coursDay">La Journee De Recurrence</param>
        public CoursCard ( Cours currentCous, DateTime coursDay )
        {
            Type = currentCous.Type.ToUpper ();
            CoursId = currentCous.CoursId;           
            
            Salle = currentCous.Salle.ToUpper();

            CoursDay = coursDay.DayOfWeek;

            StartTime = currentCous.StartTime.GetValueOrDefault();
            EndTime = currentCous.EndTime.GetValueOrDefault();

            ForeColor = "Black";

            Horraire = StartTime.TimeOfDay.ToString("hh\\:mm") + " - " + EndTime.TimeOfDay.ToString("hh\\:mm");

            ResolveData (coursDay, currentCous.MatiereId, currentCous.StaffId);
            
        }

        /// <summary>
        /// ID du cours
        /// </summary>
        public Guid CoursId { get; set; }

        /// <summary>
        /// Nom de la matiere
        /// </summary>
        public string MatiereName { get; set; }

        /// <summary>
        /// Le Type du Cours: Cours, Control, Test, TP, TD Etc..
        /// </summary>
        public string Type { get; set; } 

        /// <summary>
        /// Le nom de l'instructeur
        /// </summary>
        public string StaffFullName { get; set; }
        
        /// <summary>
        /// La Salle ou le cours Sera Dispenser
        /// </summary>
        public string Salle { get; set; }
        
        /// <summary>
        /// La Journee ou le cours sera dispenser
        /// </summary>
        public DayOfWeek CoursDay { get; set; }

        /// <summary>
        /// L'Horraire Formater
        /// </summary>
        public string Horraire { get; set; }

        /// <summary>
        /// La couleur du cours
        /// </summary>
        public string Couleur { get; set; }

        /// <summary>
        /// Forecolor
        /// </summary>
        public string ForeColor { get; set; }

        /// <summary>
        /// L'heure ou le cours commencera 
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// L'heure ou le cours terminera 
        /// </summary>
        public DateTime EndTime { get; set; }

        private void ResolveData (DateTime coursDay, Guid matiereId, string staffId )
        {           
            using(var db = new Ef ())
            {
                var m = db.Matiere.Find(matiereId);

                MatiereName = m.Name;
                Couleur = m.Couleur;

                StaffFullName = db.Staff.Find (staffId).FullName;
            }

            if (coursDay < DateTime.Today)
            {
                ForeColor = "Gray";
            }
            else if(coursDay == DateTime.Today)
            {
                if (EndTime.TimeOfDay < DateTime.Now.TimeOfDay)
                {                                                            
                    ForeColor = "Gray";                    
                }

                if (StartTime.TimeOfDay <= DateTime.Now.TimeOfDay && EndTime.TimeOfDay >= DateTime.Now.TimeOfDay)
                {
                    ForeColor = "Red";
                }
            }
                      
        }

    }
}
