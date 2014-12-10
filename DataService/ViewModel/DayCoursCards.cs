using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Context;

namespace DataService.ViewModel
{
    /// <summary>
    /// Retourne les Informations d'une Journee Avec la liste de ses Cours
    /// </summary>
    public class DayCoursCards
    {
        /// <summary>
        /// Retourne les Informations d'une Journee Avec la liste de ses Cours
        /// </summary>
        /// <param name="classId">ID de la Classe</param>
        /// <param name="scheduleDate"> Date De la Journee</param>
        public DayCoursCards ( Guid classId, DateTime scheduleDate )
        {
            DayCours = new List<CoursCard> ();

            DayName = scheduleDate.DayOfWeek.ToString ().ToUpper ();

            DayColor = scheduleDate == DateTime.Today.Date ? "Green" : "#25A0DA";

            using (var db = new Ef())
            {
                var myCours = db.Cours.Where(c => c.ClasseId == classId && c.StartDate <= scheduleDate && c.EndDate >= scheduleDate).OrderBy(c => c.StartTime);

                foreach (var cr in myCours)
                {
                    var dayNum = (int)scheduleDate.DayOfWeek;

                    if (cr.RecurrenceDays.Contains(dayNum.ToString()))
                    {
                        DayCours.Add(new CoursCard(cr, scheduleDate));
                    }
                }
            }
        }
       
        /// <summary>
        /// Le Nom De la Journee WeekDay Name
        /// </summary>
        public string DayName { get; set; }

        /// <summary>
        /// La Couleur de la journee
        /// </summary>
        public string DayColor { get; set; }
      
        /// <summary>
        /// La Liste des Cours Enseigner Dans la Journee
        /// </summary>
        public List<CoursCard> DayCours { get; set; }

        //public string DAY_START_TIME { get; set; }

        //public string DAY_END_TIME { get; set; }

        //private void ResolveData ( Guid ClassID, DateTime scheduleDate )
        //{
        //    using(var Db = new EF ())
        //    {               
        //        var MyCours = Db.COURS.Where (C => C.CLASSE_ID == ClassID && C.START_DATE <= scheduleDate && C.END_DATE >= scheduleDate).OrderBy(C => C.START_TIME);
    
        //        foreach(var CR in MyCours) {                    
        //            var DayNum = (int)scheduleDate.DayOfWeek;

        //            if(CR.RECURRENCE_DAYS.Contains (DayNum.ToString ())) {
        //                DAY_COURS.Add (new CoursCard (CR, scheduleDate));
        //            }                   
        //        }              
        //    }            
        //}




    }
}






//var WeekDays = new List<DayOfWeek> {
//                        DayOfWeek.Monday,
//                        DayOfWeek.Tuesday,
//                        DayOfWeek.Wednesday,
//                        DayOfWeek.Thursday,
//                        DayOfWeek.Friday,
//                        DayOfWeek.Saturday,
//                        DayOfWeek.Sunday
//                    };

//foreach(var CR in MyCours)
//                {
//                    foreach(var D in WeekDays)
//                    {
//                        var Num = (int) D;

//                        if(CR.RECURRENCE_DAYS.Contains ( Num.ToString(CultureInfo.InvariantCulture) ))
//                        {
//                            DAY_COURS.Add (new CoursCard (CR, D));
//                        }
//                    }                
//                }





//DayOfWeek.Monday,
//DayOfWeek.Tuesday,
//DayOfWeek.Wednesday,
//DayOfWeek.Thursday,
//DayOfWeek.Friday,
//DayOfWeek.Saturday,
//DayOfWeek.Sunday

//if(CR.RECURRENCE_DAYS.Contains ("1"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Monday));
//                    }

//                    if(CR.RECURRENCE_DAYS.Contains ("2"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Tuesday));
//                    }

//                    if(CR.RECURRENCE_DAYS.Contains ("3"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Wednesday));
//                    }

//                    if(CR.RECURRENCE_DAYS.Contains ("4"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Thursday));
//                    }

//                    if(CR.RECURRENCE_DAYS.Contains ("5"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Friday));
//                    }

//                    if(CR.RECURRENCE_DAYS.Contains ("6"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Saturday));
//                    }

//                    if(CR.RECURRENCE_DAYS.Contains ("0"))
//                    {
//                        DAY_COURS.Add (new CoursCard (CR, DayOfWeek.Sunday));
//                    }


   //#region Resolve Day

   //         var TheDayCode = "";

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Monday)
   //         {
   //             TheDayCode = "LUN";
   //             DAY_NAME = "LUNDI";
   //         }

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Tuesday)
   //         {
   //             TheDayCode = "MAR";
   //             DAY_NAME = "MARDI";
   //         }

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Monday)
   //         {
   //             TheDayCode = "MER";
   //             DAY_NAME = "MERCREDI";
   //         }

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Monday)
   //         {
   //             TheDayCode = "JEU";
   //             DAY_NAME = "JEUDI";
   //         }

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Tuesday)
   //         {
   //             TheDayCode = "VEND";
   //             DAY_NAME = "VENDREDI";
   //         }

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Monday)
   //         {
   //             TheDayCode = "SAM";
   //             DAY_NAME = "SAMEDI";
   //         }

   //         if(scheduleDate.DayOfWeek == DayOfWeek.Monday)
   //         {
   //             TheDayCode = "DIM";
   //             DAY_NAME = "DIAMANCHE";
   //         }
            

   //         #endregion
