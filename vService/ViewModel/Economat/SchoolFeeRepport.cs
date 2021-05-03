using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Economat.Entity;
using DataService.Context;

namespace DataService.ViewModel.Economat
{
    /// <summary>
    ///contient la Liste des Payements d'un Etudiants
    /// </summary>
    public class SchoolFeeRepport
    {
        /// <summary>
        /// contient la Liste des Payements d'un Etudiants
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public SchoolFeeRepport(Guid studentGuid, DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;

            using (var db = new SchoolContext())
            {
                StudentFullName    = db.Students.Find(studentGuid)? .Person.FullName;
                PhotoIdentity      = db.Students.Find(studentGuid)?.Person.PhotoIdentity;
            }

            using (var mb = new EconomatContext())
            {
                var fees = new List<SchoolFee>(mb.SchoolFees.Where(f => f.StudentGuid == studentGuid && f.DueDate >= fromDate && f.DueDate <= toDate)).OrderBy(f=> f.DueDate);

                foreach (var fee in fees) SchoolFeeCards.Add(new SchoolFeeCard(fee));

                TotalPaid = fees.Where(f =>  f.IsPaid).Sum(f => f.NetAmount).ToString(CultureInfo.CurrentCulture) + " dh";
                TotalDue  = fees.Where(f => !f.IsPaid).Sum(f => f.NetAmount).ToString(CultureInfo.CurrentCulture) + " dh";
            }
        }


        /// <summary>
        /// StudentFullName
        /// </summary>
        public string StudentFullName { get;  }


        /// <summary>
        /// PhotoIdentity
        /// </summary>
        public byte[] PhotoIdentity { get; set; }


        /// <summary>
        /// TotalPaid
        /// </summary>
        public string TotalPaid { get; set; }


        /// <summary>
        /// TotalDue
        /// </summary>
        public string TotalDue { get; set; }


        /// <summary>
        /// FromDate
        /// </summary>
        public DateTime FromDate { get; set; }


        /// <summary>
        /// ToDate
        /// </summary>
        public DateTime ToDate { get; set; }


        /// <summary>
        /// SchoolFeeCards
        /// </summary>
        public List<SchoolFeeCard> SchoolFeeCards { get; set; } = new List<SchoolFeeCard>();
    }
}



//foreach (var fee in fees) SchoolFeeCards.Add(new SchoolFeeCard(fee));

//TotalPaid = fees.Where(f => f.IsPaid).Sum(f => f.NetAmount);
//TotalDue = fees.Where(f => !f.IsPaid).Sum(f => f.NetAmount);

//Parallel.ForEach(fees, fee =>
//{
//    SchoolFeeCards.Add(new SchoolFeeCard(fee));
//});

//Parallel.Invoke(
//() =>TotalPaid = fees.Where(f => f.IsPaid).Sum(f => f.NetAmount).ToString(CultureInfo.CurrentCulture) + " dh",

//() => TotalDue = fees.Where(f => !f.IsPaid).Sum(f => f.NetAmount).ToString(CultureInfo.CurrentCulture) + " dh"
//                               );