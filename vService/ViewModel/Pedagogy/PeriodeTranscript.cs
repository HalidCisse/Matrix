using System;
using System.Collections.Generic;
using Common.Pedagogy.Entity;

namespace DataService.ViewModel.Pedagogy {


    /// <summary>
    /// Student Transcript For a periode
    /// </summary>
    public class PeriodeTranscript {
       
        internal PeriodeTranscript(Guid studentGuid, SchoolPeriod schoolPeriod)
        {           
            //var studentGrades = StudentsGradesManager.StaticGetStudentGrades(studentGuid, periodeScolaire.StartDate, periodeScolaire.EndDate);

            //foreach(var grade in studentGrades)
            //    GradesListCards.Add(new GradeCard(grade));

            //PeriodeName=periodeScolaire.Name.ToUpper();

            //PeriodeTime="  ("+periodeScolaire.StartDate.GetValueOrDefault().ToShortDateString()+" -> "+
            //                periodeScolaire.EndDate.GetValueOrDefault().ToShortDateString()+")";
             
            //Average = studentGrades.Sum(g => g.GradeCoeff).ToString("0.##" + CultureInfo.CurrentCulture) + "/20";
        }

        /// <summary>
        /// PeriodeName
        /// </summary>
        public string PeriodeName { get; }

        /// <summary>
        /// PeriodeTime
        /// </summary>
        public string PeriodeTime { get; }
 
        /// <summary>
        /// Moyenne
        /// </summary>
        public string Average { get; }

        /// <summary>
        /// La list des tickets d'absences
        /// </summary>
        public HashSet<GradeCard> GradesListCards { get; }
        = new HashSet<GradeCard>();

    }
}
