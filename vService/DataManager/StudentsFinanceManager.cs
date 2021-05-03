using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using CLib;
using Common.Economat.Entity;
using DataService.Context;
using DataService.ViewModel;
using DataService.ViewModel.Economat;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Payements de Frais de Scolarité
    /// </summary>
    public class StudentsFinanceManager
    {

        /// <summary>
        /// Payer Frais d'Etude 
        /// </summary>
        /// <param name="myPayOff"></param>
        /// <exception cref="InvalidDataException">CAN_NOT_FIND_SCHOOL_FEE_REFERENCE</exception>
        /// <exception cref="InvalidOperationException">SCHOOL_FEE_ALREADY_PAID</exception>
        /// <returns>True pour Success</returns>
        public bool PaySchoolFee(BillPayOff myPayOff)
        {
            using (var db = new EconomatContext())
            {
                var newSchoolFee = db.SchoolFees.Find(myPayOff.SchoolFeeGuid);

                if (newSchoolFee == null)       throw new InvalidOperationException("CAN_NOT_FIND_BILL_REFERENCE");
                if (newSchoolFee.IsPaid)        throw new InvalidOperationException("BILL_ALREADY_PAID");
                if (string.IsNullOrEmpty(newSchoolFee.NumeroReference)) newSchoolFee.NumeroReference = GetNewReceipRef();
                if (RefRecueExist(newSchoolFee.NumeroReference)) throw new InvalidOperationException("RECEIPT_REFERENCE_ALREADY_EXIST");
                if (string.IsNullOrEmpty(newSchoolFee.Description)) newSchoolFee.Description = "Reglement effectuer le " + DateTime.Now;

                newSchoolFee.IsPaid               = true;
                newSchoolFee.IsPaidBy             = myPayOff.IsPaidBy;
                newSchoolFee.PaymentMethode       = myPayOff.PaymentMethode;
                newSchoolFee.NumeroReference      = myPayOff.NumeroReference;
                newSchoolFee.NumeroVirement       = myPayOff.NumeroVirement;
                newSchoolFee.Bank                 = myPayOff.Bank;
                newSchoolFee.Description          = myPayOff.Description;

                newSchoolFee.LastEditDate         = DateTime.Now;
                newSchoolFee.DatePaid             = DateTime.Now;

                db.SchoolFees.Attach(newSchoolFee);
                db.Entry(newSchoolFee).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Ajouter Un Recue a Payer Par l'Etudiant 
        /// </summary>
        /// <param name="myBill"></param>
        /// <returns>True pour Success</returns>
        public bool AddFeeReceipt(StudentBill myBill)
        {
            if (myBill.NetAmount < 0.0000001) return true;
            if (FeeReceiptExist(myBill)) return true;
            using (var db = new SchoolContext()) if (db.Students.Find(myBill.StudentGuid) == null) throw new InvalidOperationException("STUDENT_REFERENCE_NOT_FOUND");

            using (var db = new EconomatContext())
            {
                var newSchoolFee = new SchoolFee
                {
                    SchoolFeeGuid          = myBill.SchoolFeeGuid == Guid.Empty ? Guid.NewGuid() : myBill.SchoolFeeGuid,
                    StudentGuid            = myBill.StudentGuid,
                    Designation            = myBill.Designation,
                    NetAmount              = myBill.NetAmount,
                    DueDate                = myBill.DueDate,

                    DateAdded              = DateTime.Now,
                    AddUserGuid            = Guid.Empty,
                    LastEditUserGuid       = Guid.Empty
                };

                db.SchoolFees.Add(newSchoolFee);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myBill"></param>
        /// <returns></returns>
        protected internal static bool StaticAddFeeReceipt(StudentBill myBill)
        {
            if (myBill.NetAmount < 0.0000001) return true;
            if (FeeReceiptExist(myBill)) return true;
            using (var db = new SchoolContext()) if (db.Students.Find(myBill.StudentGuid) == null) throw new InvalidOperationException("STUDENT_REFERENCE_NOT_FOUND");

            using (var db = new EconomatContext())
            {
                var newSchoolFee = new SchoolFee
                {
                    SchoolFeeGuid    = myBill.SchoolFeeGuid == Guid.Empty ? Guid.NewGuid() : myBill.SchoolFeeGuid,
                    StudentGuid      = myBill.StudentGuid,
                    Designation      = myBill.Designation,
                    NetAmount        = myBill.NetAmount,
                    DueDate          = myBill.DueDate,
                    IsInstallement   = myBill.IsInstallement,

                    DateAdded        = DateTime.Now,
                    AddUserGuid      = Guid.Empty,
                    LastEditUserGuid = Guid.Empty
                };

                db.SchoolFees.Add(newSchoolFee);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Modifier un Recue 
        /// </summary>
        /// <param name="myPayOff"></param>
        /// <returns>True pour Success</returns>
        protected internal static bool UpdateFeeReceipt(BillPayOff myPayOff)
        {
            //myPayement.DateEdited = DateTime.Now;

            using (var db = new EconomatContext())
            {
                //db.FeeReceipts.Attach(myReceipt);
                db.Entry(myPayOff).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Soft Supprime Un Recue
        /// </summary>
        /// <param name="myPayOff">Guid du Ticket</param>
        /// <returns>True pour Success</returns>
        protected internal static bool DeleteFeeReceipt(BillPayOff myPayOff)
        {
           

            using (var db = new EconomatContext())
            {
                //db.FeeReceipts.Attach(myReceipt);
                db.Entry(myPayOff).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Supprime Un Recue
        /// </summary>
        /// <param name="myReceiptGuid">Guid du Ticket</param>
        /// <returns>True pour Success</returns>
        protected internal static bool HardDeleteFeeReceipt(Guid myReceiptGuid)
        {
            using (var db = new EconomatContext())
            {
                //db.FeeReceipts.Remove(db.FeeReceipts.Find(myReceiptGuid));
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Verifie L'existence d'un Recue
        /// </summary>
        /// <param name="myBill"></param>
        /// <returns>True pour oui</returns>
        protected internal static bool FeeReceiptExist(StudentBill myBill)
        {           
            using (var db = new EconomatContext())
            {
                if (db.SchoolFees.Find(myBill.SchoolFeeGuid) != null) return true;

                return db.SchoolFees.Any(r => r.StudentGuid == myBill.StudentGuid       &&
                                              r.Designation.Equals(myBill.Designation)  &&
                                              r.DueDate     == myBill.DueDate               &&
                                              Math.Abs(r.NetAmount - myBill.NetAmount) < .00001);
            }
        }


        /// <summary>
        /// Verifie si un recue avec ce numero exist
        /// </summary>
        /// <param name="theReference"></param>
        /// <returns></returns>
        public bool RefRecueExist(string theReference)
        {
            using (var mc = new EconomatContext())
                return
                    mc.SchoolFees.Any(
                        s => s.NumeroReference.Equals(theReference, StringComparison.CurrentCultureIgnoreCase));
        }


        /// <summary>
        /// Return La List des etudiants avec facture non payer
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetStudentWithDebt()
        {
            using (var mc = new EconomatContext())
            {
                var studentMiniCard = new List<SearchCard>();
                var guidList = mc.SchoolFees.Where(s => !s.IsPaid).Select(f=> f.StudentGuid).Distinct();

                foreach (var guid in guidList)
                    studentMiniCard.Add(new SearchCard(guid));
                return studentMiniCard;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        public IEnumerable GetStudentFactures(Guid studentGuid)
        {
            using (var mc = new EconomatContext())
                return
                    mc.SchoolFees.Where(s => s.StudentGuid == studentGuid && !s.IsPaid)
                        .ToList()
                        .Select(schoolFee => new StudentBill(schoolFee))
                        .ToList().OrderBy(f=> f.DueDate);
        }


        /// <summary>
        /// Renvoi la Liste des Payements d'un Etudiants
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public SchoolFeeRepport GetTuitionCard (Guid studentGuid, DateTime fromDate, DateTime toDate) => new SchoolFeeRepport(studentGuid, fromDate, toDate);


        /// <summary>
        /// Nouveau Numero de recu Unique
        /// </summary>       
        /// <returns>Renvoi un nouveau Numéro de recu Unique</returns>
        public string GetNewReceipRef()
        {
            string newId;
            do newId = "R" + RandomHelper.GetRandLetters(1) + "-" + DateTime.Today.Month + DateTime.Today.Year.ToString().Substring(2) + "-" + RandomHelper.GetRandNum(4);
            while (
                     RefRecueExist(newId)
                  );
            return newId;
        }



    }
}

          