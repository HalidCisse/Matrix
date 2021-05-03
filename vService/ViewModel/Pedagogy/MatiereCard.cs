using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
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
        public MatiereCard ( Subject mat )
        {
			MatiereGuid = mat.SubjectGuid;
            Name = mat.Name;
            Coeff = mat.Coefficient;
            //COLOR = Mat.COULEUR;

            using (var db = new SchoolContext())
            {
                var sId = db.Studies.FirstOrDefault (c => c.SubjectGuid== mat.SubjectGuid)?.Proff.StaffGuid;

                InstructeurName = db.Staffs?.Find(sId)?.Person.FullName; //--------

                InstructeurPhoto = db.Staffs?.Find(sId)?.Person.PhotoIdentity; //------

                var weekDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
                var T = new TimeSpan(0,0,0);
                var scheduleDate = DateTime.Today.Date;

                foreach (var mc in db.Studies.Where(c => c.SubjectGuid== mat.SubjectGuid && c.StartDate <= scheduleDate && c.EndDate >= scheduleDate))
                {                             
                    var coursDuree = mc.EndTime - mc.StartTime;
                   
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
        public Guid MatiereGuid { get; }
         
        /// <summary>
        /// Nomination
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Coefficient
        /// </summary>
        public int Coeff { get; }

        ///// <summary>
        ///// Nomination
        ///// </summary>
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
