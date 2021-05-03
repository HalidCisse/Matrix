using System;
using Common.Economat.Entity;

namespace DataService.ViewModel.Economat
{
    /// <summary>
    /// Contient des Informations d'un Employement
    /// </summary>
    public class EmploymentCard
    {
        /// <summary>
        /// Contient des Informations d'un Employement
        /// </summary>
        /// <param name="employ"></param>
        public EmploymentCard(Employment employ)
        {
            EmploymentGuid = employ.EmploymentGuid;
            Position = employ.Position;

            Job = !string.IsNullOrEmpty(employ.Project)
                ? employ.Departement + " - " + employ.Project
                : employ.Departement;

            Description = employ.StartDate.GetValueOrDefault().ToShortDateString() + " -> " +
                          employ.EndDate.GetValueOrDefault().ToShortDateString();

            IsExpiredColor = employ.EndDate.GetValueOrDefault() < DateTime.Today ? "Beige" : "LightGray";
        }

        /// <summary>
        /// EmploymentGuid
        /// </summary>
        public Guid EmploymentGuid { get; set; }


        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }


        /// <summary>
        /// Departement-Project
        /// </summary>
        public string Job { get; set; }


        /// <summary>
        /// Date String
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// IsExpiredColor
        /// </summary>
        public string IsExpiredColor { get; set; }

    }
}
