using System;
using CLib;
using Common.Pedagogy.Entity;
using DataService.Context;
using DataService.Helpers;

namespace DataService.ViewModel.Pedagogy {


    /// <summary>
    /// Carte de Rapport de Note
    /// </summary>
    public class GradeCard {

        internal GradeCard(StudentGrade grade)
        {
            using (var db = new SchoolContext())
            {
                var theCours      = db.Studies.Find(grade.CoursGuid);

                MatiereName       = db.Subjects.Find(theCours.SubjectGuid).Name;
                GradeString       = grade.Mark.ToString("0.##") + " /20";                
                TypeControl       = theCours.Type.GetEnumDescription();
                PassedColor       = grade.Mark > 10 ? "Blue" : "Red";
                Appreciation      = grade.GetAppreciation().GetEnumDescription();

                Description ="Note obtenue : " +GradeString + "\nCoefficient : " + grade.Coefficient + "\nDate : " +
                              theCours.StartDate.GetValueOrDefault().ToShortDateString() + "\nHeure : " +
                              theCours.StartTime.ToString("hh\\:mm") + "-" + theCours.EndTime.ToString("hh\\:mm");

                if (!string.IsNullOrEmpty(grade.Appreciation))
                    Description +="\nAppreciation du Correcteur : "+grade.Appreciation;

            }
        }

        /// <summary>
        /// StudentGradeGuid
        /// </summary>
        public Guid StudentGradeGuid { get; set; }

        /// <summary>
        /// MatiereName
        /// </summary>
        public string MatiereName { get; }

        /// <summary>
        /// TypeControl
        /// </summary>
        public string TypeControl { get; }

        /// <summary>
        /// GradeString
        /// </summary>
        public string GradeString { get; }      

        /// <summary>
        /// PassedColor
        /// </summary>
        public string PassedColor { get; }

        /// <summary>
        /// Appreciation
        /// </summary>
        public string Appreciation { get;  }

        /// <summary>
        /// Rank
        /// </summary>
        public string Rank { get; } = "5 éme/30";

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; }


    }
}
