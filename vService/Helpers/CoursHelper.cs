
using System;
using System.Collections.Generic;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;

namespace DataService.Helpers {

    /// <summary>
    /// Helpers pour les cours
    /// </summary>
    public static class CoursHelper {


        /// <summary>
        /// Appreciation d'un Note System Francais
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static GradeAppreciations GetAppreciation (this StudentGrade grade)
        {
            if(Math.Abs(grade.Mark) <= 0) 
                return GradeAppreciations.Null;
            if(grade.Mark<4)
                return GradeAppreciations.TrèsMal;
            if (grade.Mark<7)
                return GradeAppreciations.Mal;
            if(grade.Mark<9)
                return GradeAppreciations.Mediocre;
            if(grade.Mark<12)
                return GradeAppreciations.Passable;
            if(grade.Mark<14)
                return GradeAppreciations.AssezBien;
            if(grade.Mark<17)
                return GradeAppreciations.Bien;
            return grade.Mark<20 ? GradeAppreciations.TresBien : GradeAppreciations.Excellent;
        }


        /// <summary>
        /// Determine si le type de Cours est noté ou pas
        /// </summary>
        /// <returns></returns>
        public static bool IsGraded(CoursTypes coursType) => coursType== CoursTypes.Control ||coursType== CoursTypes.Examen ||coursType== CoursTypes.Test ||
                                                             coursType== CoursTypes.Devoir ||coursType== CoursTypes.Composition;


        /// <summary>
        /// return la list des jours a partir du string de recurrence
        /// </summary>
        /// <param name="recurrenceString"></param>
        /// <returns></returns>
        public static List<DayOfWeek> DecodeRecurrence (string recurrenceString) {
            var weekDays = new List<DayOfWeek>();

            if(recurrenceString.Contains(((int)DayOfWeek.Monday).ToString())) { weekDays.Add(DayOfWeek.Monday); }
            if(recurrenceString.Contains(((int)DayOfWeek.Tuesday).ToString())) { weekDays.Add(DayOfWeek.Tuesday); }
            if(recurrenceString.Contains(((int)DayOfWeek.Wednesday).ToString())) { weekDays.Add(DayOfWeek.Wednesday); }
            if(recurrenceString.Contains(((int)DayOfWeek.Thursday).ToString())) { weekDays.Add(DayOfWeek.Thursday); }
            if(recurrenceString.Contains(((int)DayOfWeek.Friday).ToString())) { weekDays.Add(DayOfWeek.Friday); }
            if(recurrenceString.Contains(((int)DayOfWeek.Saturday).ToString())) { weekDays.Add(DayOfWeek.Saturday); }
            if(recurrenceString.Contains(((int)DayOfWeek.Sunday).ToString())) { weekDays.Add(DayOfWeek.Sunday); }

            return weekDays;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string Friendly (this DayOfWeek day) {
            
            switch(day) {
                default:
                    return "Lundi";
                case DayOfWeek.Tuesday:
                    return "Mardi";
                case DayOfWeek.Wednesday:
                    return "Mercredi";
                case DayOfWeek.Thursday:
                    return "Jeudi";
                case DayOfWeek.Friday:
                    return "Vendredi";
                case DayOfWeek.Saturday:
                    return "Samedi";
                case DayOfWeek.Sunday:
                    return "Dimanche";
            }
        }

    }
}
