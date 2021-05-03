using System;
using CLib;
using Common.Economat.Entity;
using DataService.Context;

namespace DataService.ViewModel.Economat
{

    /// <summary>
    /// Les Informtions d'une renumeration
    /// </summary>
    public class SalaryCard
    {

        /// <summary>
        /// Les Informtions d'une renumeration
        /// </summary>
        /// <param name="salary"></param>
        public SalaryCard(Salary salary)
        {
            SalaryGuid = salary.SalaryGuid;
            Denomination = salary.Designation;

            Description = salary.Remuneration + " dhs (";
            using (var db = new EconomatContext()) Description += db.Employments.Find(salary.EmploymentGuid).SalaryRecurrence.GetEnumDescription() + ")";

            DateString = salary.StartDate.GetValueOrDefault().ToShortDateString() + " -> " +
                          salary.EndDate.GetValueOrDefault().ToShortDateString();

            IsExpiredColor = salary.EndDate.GetValueOrDefault() < DateTime.Today ? "Beige" : "LightGray";

        }


        /// <summary>
        /// SalaryGuid
        /// </summary>
        public Guid SalaryGuid { get; set; }

        /// <summary>
        /// Exp Salaire de Base
        /// </summary>
        public string Denomination { get; set; }

        /// <summary>
        /// Date String
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date String
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// IsExpiredColor
        /// </summary>
        public string IsExpiredColor { get; set; }


    }
}
