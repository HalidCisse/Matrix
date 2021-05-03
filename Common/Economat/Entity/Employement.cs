using System;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Economat.Enums;

namespace Common.Economat.Entity
{
    /// <summary>
    /// Employement d'un Staff
    /// </summary>
    public class Employment : Tracable
    {

        /// <summary>
        /// Guid 
        /// </summary>
        [Key]
        public Guid EmploymentGuid { get; set; }

        /// <summary>
        /// Staff Guid
        /// </summary>
        public Guid StaffGuid { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Grade, junior , senior
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// Departement
        /// </summary>
        public string Departement { get; set; }

        /// <summary>
        /// Division
        /// </summary>
        public string Division { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public string Project { get; set; }
        
        /// <summary>
        /// Rend Cmmpte a
        /// </summary>
        public string ReportTo { get; set; }
       
        /// <summary>
        /// InstallmentRecurrence
        /// </summary>
        public InstallmentRecurrence SalaryRecurrence { get; set; }

        /// <summary>
        /// Salaire Fixe, Heures Enseignées, Heures Travailer
        /// </summary>
        public PayType PayType { get; set; }

        /// <summary>
        /// Salaire Horaire si Payer par heures travaillées
        /// </summary>
        public double HourlyPay { get; set; }

        /// <summary>
        /// Debut Employement
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Fin Employement
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual Staff Staff { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<Salary> Salaries { get; set; } = new HashSet<Salary>();        
        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<Payroll> Payrolls { get; set; } = new HashSet<Payroll>();

    }
}
