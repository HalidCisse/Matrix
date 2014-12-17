using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DataService.Context;
using DataService.Entities.Pedagogy;

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
        /// <param name="mat"></param>
        public MatiereCard ( Matiere mat )
        {
            MatiereId = mat.MatiereGuid;
            Name = mat.Name;
            Coeff = mat.Coefficient;
            //COLOR = Mat.COULEUR;

            using (var db = new Ef())
            {
                var sId = db.Cours.FirstOrDefault (c => c.MatiereGuid == mat.MatiereGuid)?.StaffGuid;

                InstructeurName = db.Staff?.Find(sId)?.FullName; //--------

                InstructeurPhoto = db.Staff?.Find(sId)?.PhotoIdentity; //------

                var weekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                var T = new TimeSpan(0,0,0);
                var scheduleDate = DateTime.Today.Date;

                foreach (var mc in db.Cours.Where(c => c.MatiereGuid == mat.MatiereGuid && c.StartDate <= scheduleDate && c.EndDate >= scheduleDate))
                {                             
                    var coursDuree = mc.EndTime.GetValueOrDefault().TimeOfDay - mc.StartTime.GetValueOrDefault().TimeOfDay;

                    foreach (var d in weekDays)
                    {
                        var dayNum = (int)d;

                        if (mc.RecurrenceDays.Contains(dayNum.ToString()))
                        {
                            T = T.Add(coursDuree);
                        }
                    }
                    HeuresParSemaine = T.TotalHours.ToString(CultureInfo.InvariantCulture); //---
                }                
            }  
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid MatiereId { get; }
         
        /// <summary>
        /// Nomination
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Coefficient
        /// </summary>
        public int Coeff { get; }

        /// <summary>
        /// Nomination
        /// </summary>
        //public string COLOR { get; }

        /// <summary>
        /// Heures par semaine
        /// </summary>
        public string HeuresParSemaine { get; }

        /// <summary>
        /// Nom de l'instructeur
        /// </summary>
        public string InstructeurName { get; }
        
        /// <summary>
        /// Photo de l'instructeur
        /// </summary>
        public byte[] InstructeurPhoto { get; set; }


       
    }
}
