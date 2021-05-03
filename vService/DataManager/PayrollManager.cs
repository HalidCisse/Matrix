using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Web.Security;
using CLib;
using Common.Economat.Entity;
using DataService.Context;
using DataService.Helpers;
using DataService.ViewModel.Economat;

namespace DataService.DataManager
{

    /// <summary>
    /// Gestion des Salaires des Staffs
    /// </summary>
    public class PayrollManager
    {
        /// <summary>
        /// Confirmer paiement d'un salaire
        /// </summary>
        /// <param name="payrollGuid"></param>
        /// <param name="finalPaycheck"></param>
        /// <param name="numeroReference"></param>
        /// <param name="totalHoursWorked"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool Paycheck(Guid payrollGuid, double? finalPaycheck = null, string numeroReference = null, TimeSpan? totalHoursWorked = null)
        {           
            using (var db = new EconomatContext()) {
                var payroll = db.Payrolls.Find(payrollGuid);
                if(payroll==null)
                    throw new InvalidOperationException("PAYROLL_REFERENCE_NOT_FOUND");
                
                if(payroll.IsPaid)
                    throw new InvalidOperationException("PAYCHECK_ALREADY_PAID");

                if(!string.IsNullOrEmpty(numeroReference) && SalarySlipExist(numeroReference))
                    throw new InvalidOperationException("PAYSLIP_REFERENCE_DUPLICATE");

                if(totalHoursWorked!=null)
                    payroll.HoursWorked = (TimeSpan) totalHoursWorked;

                if(finalPaycheck==null)
                    finalPaycheck=(new PayrollCard(payroll)).TotalSalary;

                payroll.FinalPaycheck    = (double) finalPaycheck;
                payroll.IsPaid           = true;
                payroll.IsPaidTo         = Guid.Empty;
                payroll.DatePaid         = DateTime.Now;
                payroll.NumeroReference  = string.IsNullOrEmpty(numeroReference) ? GetNewSalarySlipRef() : numeroReference;

                payroll.LastEditDate     =DateTime.Now;
                payroll.LastEditUserGuid =Guid.Empty;

                db.Payrolls.Attach(payroll);
                db.Entry(payroll).State=EntityState.Modified;

                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Annuler un Paiement d'un salaire
        /// </summary>
        /// <param name="payrollGuid"></param>
        /// <param name="totalSalary"></param>
        public bool CancelPaycheck (Guid payrollGuid) {
            using (var db = new EconomatContext()) {
                var payroll = db.Payrolls.Find(payrollGuid);
                if(payroll==null)
                    throw new InvalidOperationException("PAYROLL_REFERENCE_NOT_FOUND");
                
                payroll.IsPaid           =false;
                payroll.IsPaidTo         =Guid.Empty;
                payroll.DatePaid         =DateTime.Now;
                payroll.NumeroReference  = string.Empty;

                payroll.LastEditDate     =DateTime.Now;
                payroll.LastEditUserGuid =Guid.Empty;

                db.Payrolls.Attach(payroll);
                db.Entry(payroll).State=EntityState.Modified;

                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Ajouter Un Employement Pour un Staff
        /// </summary>
        /// <param name="employ"></param>
        /// <returns>True pour Success</returns>
        public bool AddEmployment(Employment employ)
        {
            if (string.IsNullOrEmpty(employ.Position)) throw new InvalidOperationException("POSITION_CAN_NOT_BE_EMPTY");
            if (employ.StartDate > employ.EndDate)     throw new InvalidOperationException("START_DATE_SUPERIOR_TO_END_DATE");
            using (var db = new SchoolContext()) if (db.Staffs.Find(employ.StaffGuid) == null) throw new InvalidOperationException("STAFF_REFERENCE_NOT_FOUND");

            using (var db = new EconomatContext())
            {
                if (employ.EmploymentGuid == Guid.Empty) employ.EmploymentGuid = Guid.NewGuid();

                employ.DateAdded        = DateTime.Now;
                employ.AddUserGuid      = Guid.Empty;
                employ.LastEditDate     = DateTime.Now;
                employ.LastEditUserGuid = Guid.Empty;
                
                db.Employments.Add(employ);

                if (db.SaveChanges() <= 0)
                    return false;

                foreach (var payRoll in PayRollHelper.GeneratePayRolls(employ))
                    StaticAddPayRoll(payRoll);
                return true;
            }
        }


        /// <summary>
        /// Ajouter Une Renumeration Pour un Staff
        /// </summary>
        /// <param name="salary"></param>
        /// <returns>True pour Success</returns>
        public bool AddSalary(Salary salary)
        {            
            return StaticAddSalary(salary);
        }


        /// <summary>
        /// Modifier les information d'un salaire
        /// </summary>
        /// <param name="salary"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool CancelSalary(Salary salary)
        {
            if (string.IsNullOrEmpty(salary.Designation)) throw new InvalidOperationException("DESIGNATION_CAN_NOT_BE_EMPTY");
            if (salary.StartDate > salary.EndDate) throw new InvalidOperationException("START_DATE_SUPERIOR_TO_END_DATE");
            if (salary.EndDate < DateTime.Today)   throw new InvalidOperationException("END_DATE_CAN_NOT_BE_LESS_THAN_TODAY");

            Employment emp;
            using (var db = new EconomatContext()) emp = db.Employments.Find(salary.EmploymentGuid);
            if (emp == null) throw new InvalidOperationException("EMPLOYEMENT_REFERENCE_NOT_FOUND");
            if ((salary.StartDate < emp.StartDate) || (salary.EndDate > emp.EndDate)) throw new InvalidOperationException("DATES_CAN_NOT_BE_OUT_OF_EMPLOYMENT_BOUNDRIES");

            using (var db = new EconomatContext())
            {
                var newSalary = db.Salaries.Find(salary.SalaryGuid);
                if (newSalary == null) throw new InvalidOperationException("SALARY_REFERENCE_NOT_FOUND");

                newSalary.EndDate      = salary.EndDate;
                newSalary.Description  = salary.Description;

                var user = Membership.GetUser();
                if (user == null) throw new SecurityException("USER_CAN_NOT_DETERMINED");

                // ReSharper disable once PossibleNullReferenceException
                newSalary.LastEditUserGuid = (Guid)user.ProviderUserKey;
                newSalary.LastEditDate = DateTime.Now;

                db.Salaries.Attach(newSalary);
                db.Entry(newSalary).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Modifier les information d'un employement
        /// </summary>
        /// <param name="employ"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool UpdateEmployment(Employment employ)
        {
            if (string.IsNullOrEmpty(employ.Position))                                        throw new InvalidOperationException("POSITION_CAN_NOT_BE_EMPTY");
            if (employ.StartDate > employ.EndDate)                                            throw new InvalidOperationException("START_DATE_SUPERIOR_TO_END_DATE");
            using (var db = new SchoolContext()) if (db.Staffs.Find(employ.StaffGuid) == null) throw new InvalidOperationException("STAFF_REFERENCE_NOT_FOUND");

            using (var db = new EconomatContext())
            {
                var newEmploy = db.Employments.Find(employ.EmploymentGuid);
                if (newEmploy == null)                                                        throw new InvalidOperationException("EMPLOYEMENT_REFERENCE_NOT_FOUND");

                //todo cancel Employ 
                newEmploy.Position         = employ.Position;
                newEmploy.Category         = employ.Category;
                newEmploy.Project          = employ.Project;
                newEmploy.Grade            = employ.Grade;
                newEmploy.Departement      = employ.Departement;
                newEmploy.Division         = employ.Division;
                newEmploy.ReportTo         = employ.ReportTo;
                //newEmploy.SalaryRecurrence = employ.SalaryRecurrence;
                //newEmploy.StartDate        = employ.StartDate;
                //newEmploy.EndDate          = employ.EndDate;
                newEmploy.Description      = employ.Description;

                newEmploy.LastEditDate     = DateTime.Now;
                newEmploy.LastEditUserGuid = Guid.Empty;

                db.Employments.Attach(newEmploy);
                db.Entry(newEmploy).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// La Somme Payer au Staff entre ces deux dates
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public double GetStaffTotalPaid(Guid staffGuid, DateTime? startDate, DateTime? endDate)
        {
            using (var db = new EconomatContext())
                return
                    StaticGetEmployements(staffGuid, startDate, endDate)
                        .Select(e => e.EmploymentGuid)
                        .Where(employ => db.Payrolls.Any(e => e.EmploymentGuid == employ && e.IsPaid))
                        .Sum(
                            employ =>
                                db.Payrolls.Where(e => e.EmploymentGuid == employ && e.IsPaid).Sum(p => p.FinalPaycheck));
        }


        /// <summary>
        /// La nombre de salire non payer au Staff entre ces deux dates
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int GetStaffTotalDue(Guid staffGuid)
        {
            using (var db = new EconomatContext())
                return
                    StaticGetEmployements(staffGuid)
                        .ToList()
                        .Sum(e => db.Payrolls.Count(p => p.EmploymentGuid == e.EmploymentGuid && !p.IsPaid));          
        }


        /// <summary>
        /// Employements D'un Staff 
        /// </summary>
        /// <param name="satffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<EmploymentCard> GetEmployments(Guid satffGuid, DateTime? startDate = null, DateTime? endDate = null) => StaticGetEmployements(satffGuid, startDate, endDate).Select(employ => new EmploymentCard(employ)).ToList();


        /// <summary>
        /// Employment
        /// </summary>
        /// <param name="employGuid"></param>
        /// <returns></returns>
        public Employment GetEmployment(Guid employGuid) => StaticGetEmployment(employGuid);


        /// <summary>
        /// Liste des Renumerations d'un employement
        /// </summary>
        /// <param name="employGuid"></param>
        /// <returns></returns>
        public List<SalaryCard> GetSalaries(Guid employGuid) => StaticGetSalaries(employGuid).Select(s=> new SalaryCard(s)).ToList();


        /// <summary>
        /// Un salaire par son guid
        /// </summary>
        /// <param name="salaryGuid"></param>
        /// <returns></returns>
        public Salary GetSalary(Guid salaryGuid) => StaticGetSalary(salaryGuid);


        /// <summary>
        /// List des Salaires
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<PayrollCard> GetPayrolls (Guid staffGuid, DateTime? fromDate, DateTime? toDate) => StaticGetStaffPayrolls(staffGuid, fromDate, toDate).Select(p => new PayrollCard(p)).ToList();



        #region PROTECTED INTERNAL STATIC



        /// <summary>
        /// Renvoi la somme des salaires de tous les cours enseignées
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="removeAbsencesAndRetards"></param>
        /// <returns></returns>
        public static double StaticGetTeachingSalary (Guid staffGuid, DateTime? startDate, DateTime? endDate, bool removeAbsencesAndRetards = false) {
            using (var db = new SchoolContext())
            {
                var totalSomme = new double();

                foreach (var cours in StudyManager.GetStaffCoursBetween(staffGuid, startDate, endDate))
                    totalSomme +=
                        StudyManager.StaticGetHoursTaught(staffGuid, cours.StudyGuid, startDate, endDate,
                            removeAbsencesAndRetards).TotalMinutes*(db.Subjects.Find(cours.SubjectGuid).HourlyPay/60);
                return totalSomme;
            }           
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static List<Payroll> StaticGetStaffPayrolls (Guid staffGuid, DateTime? startDate = null, DateTime? endDate = null) {
            List<Guid> staffJobs;
            using (var db = new EconomatContext()) {
                if(startDate==null||endDate==null)
                    staffJobs=
                        db.Employments.Where(e => e.StaffGuid==staffGuid).Select(e => e.EmploymentGuid).ToList();
                else
                    staffJobs=db.Employments.Where(
                        e => e.StaffGuid==staffGuid&&
                             (
                                 (
                                     e.StartDate<=startDate&&
                                     e.EndDate>=startDate
                                 )
                                 ||
                                 (
                                     e.StartDate>=startDate&&
                                     e.StartDate<=endDate
                                 )
                                 )).Select(e => e.EmploymentGuid).ToList();
            }

            var payrolls = new List<Payroll>();
            foreach(var jobGuid in staffJobs)              
                payrolls.AddRange(StaticGetPayrolls(jobGuid, startDate, endDate));
            return payrolls.OrderByDescending(p=> p.PaycheckDate).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static List<Payroll> StaticGetPayrolls (Guid employGuid, DateTime? startDate = null, DateTime? endDate = null)
        {           
            using (var db = new EconomatContext()) {
                if(startDate==null||endDate==null)
                    return db.Payrolls.Where(e => e.EmploymentGuid==employGuid).OrderByDescending(e => e.PaycheckDate).ToList();

                return db.Payrolls.Where(
                    e => e.EmploymentGuid==employGuid&&
                    (
                        e.PaycheckDate>=startDate&&
                        e.PaycheckDate<=endDate
                    )).OrderByDescending(e => e.PaycheckDate).ToList();
            }
        }

        /// <summary>
        /// Ajouter un Payroll
        /// </summary>
        /// <returns></returns>
        protected internal static bool StaticAddPayRoll(Payroll payRoll)
        {
            if (PayRollExist(payRoll)) return true;
 
            using (var db = new EconomatContext())
            {
                if (payRoll.PayrollGuid == Guid.Empty) payRoll.PayrollGuid = Guid.NewGuid();

                payRoll.DateAdded        = DateTime.Now;
                payRoll.AddUserGuid      = Guid.Empty;
                payRoll.LastEditDate     = DateTime.Now;
                payRoll.LastEditUserGuid = Guid.Empty;

                db.Payrolls.Add(payRoll);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Employment
        /// </summary>
        /// <param name="employGuid"></param>
        /// <returns></returns>
        protected internal static Employment StaticGetEmployment(Guid employGuid)
        {
            using (var db = new EconomatContext())
                return db.Employments.Find(employGuid);
        }

        /// <summary>
        /// Return La liste des EMployements D'un Staff Actuelles
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static List<Employment> StaticGetEmployements(Guid staffGuid, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var db = new EconomatContext())
            {
                if (startDate == null || endDate == null) return db.Employments.Where(e => e.StaffGuid == staffGuid).OrderByDescending(e=> e.StartDate).ToList();

                return db.Employments.Where(
                    e => e.StaffGuid == staffGuid && 
                    (
                         (
                             e.StartDate <= startDate &&
                             e.EndDate >= startDate
                         )
                         ||
                         (
                             e.StartDate >= startDate &&
                             e.StartDate <= endDate
                         )
                    )).OrderByDescending(e => e.StartDate).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        protected internal static bool StaticAddSalary(Salary salary)
        {
            if (SalaryExist(salary)) return true;
            if (string.IsNullOrEmpty(salary.Designation)) throw new InvalidOperationException("DESIGNATION_CAN_NOT_BE_EMPTY");
            if (salary.StartDate > salary.EndDate)        throw new InvalidOperationException("START_DATE_SUPERIOR_TO_END_DATE");

            Employment emp;
            using (var db = new EconomatContext()) emp = db.Employments.Find(salary.EmploymentGuid);

            if (emp == null) throw new InvalidOperationException("EMPLOYEMENT_REFERENCE_NOT_FOUND");
            if ((salary.StartDate < emp.StartDate) || (salary.EndDate > emp.EndDate)) throw new InvalidOperationException("DATES_CAN_NOT_BE_OUT_OF_EMPLOYMENT_BOUNDRIES");

            using (var db = new EconomatContext())
            {
                if (salary.SalaryGuid == Guid.Empty) salary.SalaryGuid = Guid.NewGuid();
                if (salary.Description == string.Empty) salary.Description = salary.Designation;

                salary.DateAdded        = DateTime.Now;
                salary.AddUserGuid      = Guid.Empty;
                salary.LastEditDate     = DateTime.Now;
                salary.LastEditUserGuid = Guid.Empty;

                db.Salaries.Add(salary);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Return La liste des salairs d'un Employements
        /// </summary>
        /// <param name="employGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static List<Salary> StaticGetSalaries(Guid employGuid, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var db = new EconomatContext())
            {
                if (startDate == null || endDate == null) return db.Salaries.Where(e => e.EmploymentGuid == employGuid).OrderByDescending(e => e.StartDate).ToList();

                return db.Salaries.Where(
                    e => e.EmploymentGuid == employGuid &&
                    (                        
                         e.StartDate<=endDate &&
                         e.EndDate>=endDate
                    )).OrderBy(e => e.StartDate).ToList();
            }            
        }

        /// <summary>
        /// un salaire par son guid
        /// </summary>
        /// <param name="salaryGuid"></param>
        /// <returns></returns>
        protected internal static Salary StaticGetSalary(Guid salaryGuid)
        {
            using (var db = new EconomatContext())
                return db.Salaries.Find(salaryGuid);
        }

        /// <summary>
        /// Verifie L'existence d'une Salaire
        /// </summary>
        /// <param name="salary"></param>
        /// <returns>True pour oui</returns>
        protected internal static bool SalaryExist(Salary salary)
        {
            using (var db = new EconomatContext())
            {
                if (db.Salaries.Find(salary.SalaryGuid) != null) return true;

                return db.Salaries.Any(r =>   r.EmploymentGuid == salary.EmploymentGuid &&
                                              r.Designation.Equals(salary.Designation) &&
                                              r.StartDate == salary.StartDate &&
                                              r.EndDate == salary.EndDate &&
                                              Math.Abs(r.Remuneration - salary.Remuneration) < .001);
            }
        }

        /// <summary>
        /// Verifie L'existence d'une payroll
        /// </summary>
        /// <param name="payroll"></param>
        /// <returns>True pour oui</returns>
        protected internal static bool PayRollExist(Payroll payroll)
        {
            using (var db = new EconomatContext())
            {
                if (db.Payrolls.Find(payroll.PayrollGuid) != null) return true;

                return db.Payrolls.Any(r => r.EmploymentGuid == payroll.EmploymentGuid &&
                                            r.Designation.Equals(payroll.Designation)  &&
                                            r.PaycheckDate == payroll.PaycheckDate);
            }
        }

        /// <summary>
        /// Verifie si un recue avec ce numero exist
        /// </summary>
        /// <param name="theReference"></param>
        /// <returns></returns>
        protected internal static bool SalarySlipExist (string theReference) {
            using (var mc = new EconomatContext())
                return
                    mc.Payrolls.Any(
                        s => s.NumeroReference.Equals(theReference, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Nouveau Numero de recu Unique
        /// </summary>       
        /// <returns>Renvoi un nouveau Numéro de recu Unique</returns>
        protected internal static string GetNewSalarySlipRef () {
            string newId;
            do
                newId="SR"+RandomHelper.GetRandLetters(1)+"-"+DateTime.Today.Month+DateTime.Today.Year.ToString().Substring(2)+"-"+RandomHelper.GetRandNum(4);
            while(SalarySlipExist(newId));
            return newId;
        }

        #endregion


    }
}
