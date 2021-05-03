using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Context;
using DataService.Helpers;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// Retourne les Informations d'une Journee Avec la liste de ses Cours
    /// </summary>
    public class DayCoursCards
    {
        /// <summary>
        /// Retourne les Informations d'une Journee Avec la liste de ses Cours pour un staff
        /// </summary>
        /// <param name="staffGuid">ID de la Classe</param>
        /// <param name="scheduleDate"> Date De la Journee</param>
        /// <param name="isStaff"></param>
        public DayCoursCards (Guid staffGuid, DateTime scheduleDate, bool isStaff) {
            DayCours=new List<CoursCard>();
            DayName = scheduleDate.DayOfWeek.Friendly();           
            DayColor=scheduleDate==DateTime.Today ? "Green" : "#25A0DA";
            DayDate=scheduleDate;

            using (var db = new SchoolContext())
            {
                var myCours = db.Studies.Where(c => c.ProffGuid==staffGuid&&!c.IsDeleted&&
                                                (
                                                    c.StartDate<=scheduleDate&&
                                                    c.EndDate>=scheduleDate
                                                )).OrderBy(c => c.StartTime);

                foreach (var cr in from cr in myCours
                    let dayNum = ((int) scheduleDate.DayOfWeek).ToString()
                    where cr.RecurrenceDays.Contains(dayNum)
                    select cr)
                    DayCours.Add(new CoursCard(cr, scheduleDate, isStaff));

                foreach(
                    var coursCardX in
                        DayCours.Where(
                            coursCardX =>
                                db.StudyExceptions.Any(
                                    c => c.StudyGuid==coursCardX.CoursGuid&&c.ExceptionDate==scheduleDate))
                            .ToList())
                    DayCours.Remove(coursCardX);
            }           
        }

        /// <summary>
        /// Retourne les Informations d'une Journee Avec la liste de ses Cours
        /// </summary>
        /// <param name="classId">ID de la Classe</param>
        /// <param name="scheduleDate"> Date De la Journee</param>
        public DayCoursCards ( Guid classId, DateTime scheduleDate )
        {
            DayCours = new List<CoursCard> ();
            DayName = scheduleDate.DayOfWeek.Friendly();           
            DayColor = scheduleDate == DateTime.Today ? "Green" : "#25A0DA";        
            DayDate  = scheduleDate;

            using (var db = new SchoolContext())
            {
                var myCours = db.Studies.Where(c => c.ClasseGuid==classId && !c.IsDeleted &&
                                                (
                                                    c.StartDate<=scheduleDate &&
                                                    c.EndDate>=scheduleDate
                                                )).OrderBy(c => c.StartTime);

                foreach (var cr in myCours)
                {
                    var dayNum = ((int)scheduleDate.DayOfWeek).ToString();

                    if (cr.RecurrenceDays.Contains(dayNum)) DayCours.Add(new CoursCard(cr, scheduleDate));                   
                }                 
            }

            using (var db = new SchoolContext())
            {               
                foreach (
                    var coursCardX in
                        DayCours.Where(
                            coursCardX =>
                                db.StudyExceptions.Any(
                                    c => c.StudyGuid == coursCardX.CoursGuid && c.ExceptionDate == scheduleDate))
                            .ToList())
                    DayCours.Remove(coursCardX);
            }
        }
       
        /// <summary>
        /// Le Nom De la Journee WeekDay Name
        /// </summary>
        public string DayName { get; }

        /// <summary>
        /// La Couleur de la journee
        /// </summary>
        public string DayColor { get; set; }
        
        /// <summary>
        /// La Date du Jour
        /// </summary>
        public DateTime? DayDate { get; }

        /// <summary>
        /// La Liste des Cours Enseigner Dans la Journee
        /// </summary>
        public List<CoursCard> DayCours { get; }

    }
}



