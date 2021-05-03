using System;
using System.ComponentModel.DataAnnotations;
using CLib.Database;

namespace Common.Economat.Entity
{

	/// <summary>
	/// Definie la methode de versement des salairs au staff et enseignants
	/// </summary>
	public class Salary : Tracable
    {		
        /// <summary>
        /// Le Guid de la renumeration
        /// </summary>
        [Key]
        public Guid SalaryGuid { get; set; }

        /// <summary>
        /// Reference de l'employement
        /// </summary>
        public Guid EmploymentGuid { get; set; }

        /// <summary>
        /// Exp Salaire de Base
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Remuneration fixe
        /// </summary>
        public double Remuneration { get; set; }

        /// <summary>
        /// Debut Remuneration
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Fin Remuneration
        /// </summary>
        public DateTime? EndDate { get; set; }
       
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual Employment Employment { get; set; }
    }
}
