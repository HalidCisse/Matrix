using System;
using System.Collections.Generic;
using CLib;
using Common.Economat.Entity;
using Common.Economat.Enums;

namespace DataService.Helpers
{
    internal static class PayRollHelper
    {
  
        internal static HashSet<Payroll> GeneratePayRolls(Employment employ)
        {
            var payrollsList = new HashSet<Payroll>();
            if (employ.SalaryRecurrence == InstallmentRecurrence.Once)
            {
                payrollsList.Add(new Payroll
                {
                    EmploymentGuid = employ.EmploymentGuid,
                    Designation    = "Salaire",
                    PaycheckDate   = employ.EndDate
                });
                return payrollsList;
            }                 

            var debutJob       = employ.StartDate.GetValueOrDefault();
            var finJob         = employ.EndDate.GetValueOrDefault();
            var jobTotalMonths = DateTimeHelper.MonthDifference(debutJob, finJob);
            var periodLenght   = (int)employ.SalaryRecurrence;
            var totalSalaires  = jobTotalMonths / periodLenght;
            if ((jobTotalMonths % periodLenght) != 0) totalSalaires++;
            var nextPayOff     = debutJob;
            var payDay         = employ.StartDate.GetValueOrDefault().Day;

            for (var i = 0; i < totalSalaires; i++)
            {
                nextPayOff=new DateTime(nextPayOff.AddMonths(periodLenght).Year, nextPayOff.AddMonths(periodLenght).Month, payDay); 

                payrollsList.Add(new Payroll
                {
                    EmploymentGuid = employ.EmploymentGuid,
                    Designation = "Salaire " + employ.SalaryRecurrence.GetEnumDescription() + "(" + nextPayOff.ToString("MMM-yy") + ")",
                    PaycheckDate = nextPayOff
                });
            }
            return payrollsList;
        }
    }
}
