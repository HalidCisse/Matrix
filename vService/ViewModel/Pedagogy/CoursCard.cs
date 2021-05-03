using System;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
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
        /// <param name="isStaff"></param>
        public CoursCard ( Study currentCous, DateTime coursDay, bool isStaff = false )
        {            
            Type      = currentCous.Type;
            CoursGuid = currentCous.StudyGuid;                      
            Salle     = currentCous.Room?.ToUpper();
            CoursDate = coursDay;
            StartTime = currentCous.StartTime;
            EndTime   = currentCous.EndTime;
            ForeColor = "Black";

            using (var db = new SchoolContext()) {
                var m = db.Subjects.Find(currentCous.SubjectGuid);

                MatiereName=m.Name.Substring(0, 1).ToUpper()+m.Name.Substring(1).ToLower();
                Couleur=m.Couleur;

                StaffFullName = isStaff ? db.Classes.Find(currentCous.ClasseGuid).Sigle : db.Staffs.Find(currentCous.ProffGuid).Person.FullName;
            }

            if(coursDay<DateTime.Today)
                ForeColor="Gray";
            else if(coursDay==DateTime.Today) {
                if(EndTime<DateTime.Now.TimeOfDay)
                    ForeColor="Gray";

                if(StartTime<=DateTime.Now.TimeOfDay&&EndTime>=DateTime.Now.TimeOfDay)
                    ForeColor="Red";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursEvent"></param>
        /// <param name="isStaff"></param>
        public CoursCard(CoursEvent coursEvent, bool isStaff = false)
        {            
            using (var db = new SchoolContext())
            {
                var currentCous = db.Studies.Find(coursEvent.CoursGuid);

                Type=currentCous.Type;
                CoursGuid=currentCous.StudyGuid;
                Salle=currentCous.Room?.ToUpper();
                CoursDate=coursEvent.EventDate;
                StartTime=currentCous.StartTime;
                EndTime=currentCous.EndTime;
                ForeColor="Black";

                var m = db.Subjects.Find(currentCous.SubjectGuid);

                MatiereName=m.Name.Substring(0, 1).ToUpper()+m.Name.Substring(1).ToLower();
                Couleur=m.Couleur;

                StaffFullName=isStaff ? currentCous.Classe.Sigle : currentCous.Proff.Person.FullName;
            }

            if(coursEvent.EventDate<DateTime.Today)
                ForeColor="Gray";
            else if(coursEvent.EventDate==DateTime.Today) {
                if(EndTime<DateTime.Now.TimeOfDay)
                    ForeColor="Gray";

                if(StartTime<=DateTime.Now.TimeOfDay&&EndTime>=DateTime.Now.TimeOfDay)
                    ForeColor="Red";
            }
        }

        /// <summary>
        /// ID du cours
        /// </summary>
        public Guid CoursGuid { get;  }

        /// <summary>
        /// Nom de la matiere
        /// </summary>
        public string MatiereName { get; set; }

        /// <summary>
        /// Le Type du Cours: Cours, Control, Test, TP, TD Etc..
        /// </summary>
        public CoursTypes Type { get; set; } 

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
        public DateTime CoursDate { get; set; }

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
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// L'heure ou le cours terminera 
        /// </summary>
        public TimeSpan EndTime { get; set; }

       

    }
}
