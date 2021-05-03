using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using CLib;
using Common.Economat.Entity;
using DataService.Context;
using DataService.ViewModel.Economat;

namespace DataService.DataManager {

    /// <summary>
    /// Tresorerie
    /// </summary>
    public sealed class TreasuryManager {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTransaction"></param>
        /// <returns></returns>
        public bool NewTransaction (Transaction newTransaction)
        {
            //todo security
            return StaticNewTransaction(newTransaction);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionGuid"></param>
        /// <returns></returns>
        public bool CancelTransaction (Guid transactionGuid) {            
            using (var db = new EconomatContext())
            {
                var theTransaction = db.Transactions.Find(transactionGuid);
                if (theTransaction == null) throw new InvalidOperationException("CAN_NOT_FIND_REFERENCE_TRANSACTION");
              
                theTransaction.IsDeleted        = true;
                theTransaction.DeleteDate       = DateTime.Now;
                theTransaction.DeleteUserGuid   = Guid.Empty;

                theTransaction.LastEditDate     = DateTime.Now;
                theTransaction.LastEditUserGuid =Guid.Empty;

                db.Transactions.Attach(theTransaction);
                db.Entry(theTransaction).State=EntityState.Modified;
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Nouvelle Numero de Reference
        /// </summary>
        public string GetNewTransactionReference () {
            string newId;
            do
                newId="T"+RandomHelper.GetRandLetters(1)+"-"+DateTime.Today.Month+DateTime.Today.Year.ToString().Substring(2)+"-"+RandomHelper.GetRandNum(4);
            while(
                     RefTransactionExist(newId)
                  );
            return newId;
        }


        /// <summary>
        /// Verifie si cette reference exist
        /// </summary>
        /// <param name="theReference"></param>
        /// <returns></returns>
        public bool RefTransactionExist (string theReference) {
            using (var mc = new EconomatContext())
                return
                    mc.Transactions.Any(
                        s => s.TransactionReference.Equals(theReference, StringComparison.CurrentCultureIgnoreCase));
        }


        /// <summary>
        /// liste des transactions
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        public IEnumerable GetTransactions (DateTime? startDate, DateTime? endDate, bool includeDeleted = false)
        {
            if (includeDeleted)
                using (var db = new EconomatContext())
                {
                    if (startDate == null || endDate == null)
                        return
                            db.Transactions.OrderByDescending(t => t.TransactionDate)
                                .ToList()
                                .Select(t => new TransactionCard(t));

                    return db.Transactions.Where(t =>
                        t.TransactionDate >= startDate &&
                        t.TransactionDate <= endDate
                        ).OrderByDescending(t => t.TransactionDate).ToList().Select(t => new TransactionCard(t));
                }

            using (var db = new EconomatContext()) {
                if(startDate==null||endDate==null)
                    return db.Transactions.Where(t=> !t.IsDeleted).OrderByDescending(t => t.TransactionDate).ToList().Select(t => new TransactionCard(t));

                return db.Transactions.Where(t => !t.IsDeleted &&
                                                  t.TransactionDate>=startDate&&
                                                  t.TransactionDate<=endDate
                    ).OrderByDescending(t => t.TransactionDate).ToList().Select(t => new TransactionCard(t));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public double GetTotalRecette (DateTime? startDate, DateTime? endDate) 
            => StaticGetTotalRecette(startDate, endDate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public double GetTotalDepense (DateTime? startDate, DateTime? endDate) 
            => StaticGetTotalDepense(startDate, endDate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public double GetTotalPaidSchoolFee (DateTime? startDate, DateTime? endDate) 
            => StaticGetTotalPaidSchoolFee(startDate, endDate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public double GetTotalPaidSalaries (DateTime? startDate, DateTime? endDate) 
            => StaticGetTotalPaidSalaries(startDate, endDate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public double GetSoldeCaisse (DateTime? startDate, DateTime? endDate) 
            => StaticGetSoldeCaisse(startDate, endDate);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public double GetSolde (DateTime? startDate, DateTime? endDate)
        {
            var solde  = StaticGetSoldeCaisse(startDate, endDate);
            solde     -= StaticGetTotalPaidSalaries(startDate, endDate);
            solde     += StaticGetTotalPaidSchoolFee(startDate, endDate);
            return solde;
        }



        #region Protected Internal Static


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal static double StaticGetTotalRecette (DateTime? startDate, DateTime? endDate) {
            using (var db = new EconomatContext()) {
                if(!db.Transactions.Any(t => !t.IsDeleted&&t.Amount>0))
                    return 0;

                if(startDate==null||endDate==null)
                    return db.Transactions.Where(t => !t.IsDeleted&&t.Amount>0).Sum(t => t.Amount);

                if(!db.Transactions.Any(t =>
                                                !t.IsDeleted&&t.Amount>0&&
                                                t.TransactionDate>=startDate&&
                                                t.TransactionDate<=endDate
                                            ))
                    return 0;

                return db.Transactions.Where(t =>
                                                !t.IsDeleted&&t.Amount>0&&
                                                t.TransactionDate>=startDate&&
                                                t.TransactionDate<=endDate
                                            ).Sum(t => t.Amount);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal static double StaticGetTotalDepense (DateTime? startDate, DateTime? endDate) {
            using (var db = new EconomatContext()) {

                if(!db.Transactions.Any(t => !t.IsDeleted&&t.Amount<0))
                    return 0;

                if(startDate==null||endDate==null)
                    return db.Transactions.Where(t => !t.IsDeleted&&t.Amount<0).Sum(t => t.Amount);

                if(!db.Transactions.Any(t =>
                                                !t.IsDeleted&&t.Amount<0&&
                                                t.TransactionDate>=startDate&&
                                                t.TransactionDate<=endDate
                                            ))
                    return 0;

                return db.Transactions.Where(t =>
                                                !t.IsDeleted&&t.Amount<0&&
                                                t.TransactionDate>=startDate&&
                                                t.TransactionDate<=endDate
                                            ).Sum(t => t.Amount);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        internal static double StaticGetTotalPaidSchoolFee (DateTime? startDate, DateTime? endDate) {
            using (var db = new EconomatContext()) {
                if(!db.SchoolFees.Any(t => !t.IsDeleted&&t.IsPaid))
                    return 0;

                if(startDate==null||endDate==null)
                    return db.SchoolFees.Where(t => !t.IsDeleted&&t.IsPaid).Sum(t => t.NetAmount);

                if(!db.SchoolFees.Any(t =>
                                                !t.IsDeleted&&t.IsPaid&&
                                                t.DatePaid>=startDate&&
                                                t.DatePaid<=endDate
                                            ))
                    return 0;

                return db.SchoolFees.Where(t =>
                                                !t.IsDeleted&&t.IsPaid&&
                                                t.DatePaid>=startDate&&
                                                t.DatePaid<=endDate
                                            ).Sum(t => t.NetAmount);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        internal static double StaticGetTotalPaidSalaries (DateTime? startDate, DateTime? endDate) {
            using (var db = new EconomatContext()) {
                if(!db.Payrolls.Any(t => !t.IsDeleted&&t.IsPaid))
                    return 0;

                if(startDate==null||endDate==null)
                    return db.Payrolls.Where(t => !t.IsDeleted&&t.IsPaid).Sum(t => t.FinalPaycheck);

                if(!db.Payrolls.Any(t =>
                                            !t.IsDeleted&&t.IsPaid&&
                                            t.DatePaid>=startDate&&
                                            t.DatePaid<=endDate
                                        ))
                    return 0;

                return db.Payrolls.Where(t =>
                                            !t.IsDeleted&&t.IsPaid&&
                                            t.DatePaid>=startDate&&
                                            t.DatePaid<=endDate
                                        ).Sum(t => t.FinalPaycheck);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        internal static double StaticGetSoldeCaisse (DateTime? startDate, DateTime? endDate) {
            using (var db = new EconomatContext()) {
                if(!db.Transactions.Any(t => !t.IsDeleted))
                    return 0;

                if(startDate==null||endDate==null)
                    return db.Transactions.Where(t => !t.IsDeleted).Sum(t => t.Amount);

                if(!db.Transactions.Any(t =>
                                                !t.IsDeleted&&
                                                t.TransactionDate>=startDate&&
                                                t.TransactionDate<=endDate
                                            ))
                    return 0;

                return db.Transactions.Where(t =>
                                                !t.IsDeleted&&
                                                t.TransactionDate>=startDate&&
                                                t.TransactionDate<=endDate
                                            ).Sum(t => t.Amount);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal static double StaticGetSolde (DateTime? startDate, DateTime? endDate) {
            var solde = StaticGetSoldeCaisse(startDate, endDate);
            solde-=StaticGetTotalPaidSalaries(startDate, endDate);
            solde+=StaticGetTotalPaidSchoolFee(startDate, endDate);
            return solde;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTransaction"></param>
        /// <returns></returns>
        internal static bool StaticNewTransaction (Transaction newTransaction) {

            //if(Math.Abs(newTransaction.Amount) < 0.000000000000000001)
             //   return true;

            if(TransactionExist(newTransaction))
                throw new InvalidOperationException("TRANSACTION_REFERENCE_ALREADY_EXIST");

            if(newTransaction.TransactionDate < DateTime.Today.AddDays(-1))
                throw new InvalidOperationException("TRANSACTION_DATE_NOT_VALIDE");

            using (var db = new EconomatContext())
            {                    
                newTransaction.TransactionGuid =newTransaction.TransactionGuid == Guid.Empty ? Guid.NewGuid() : newTransaction.TransactionGuid;
                var transDate = newTransaction.TransactionDate.GetValueOrDefault();
                newTransaction.TransactionDate = new DateTime(transDate.Year, transDate.Month, transDate.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if (string.IsNullOrEmpty(newTransaction.Description)) newTransaction.Description =newTransaction.Designation;
              
                newTransaction.DateAdded        = DateTime.Now;
                newTransaction.AddUserGuid      = Guid.Empty;
                newTransaction.LastEditDate     = DateTime.Now;
                newTransaction.LastEditUserGuid = Guid.Empty;

                db.Transactions.Add(newTransaction);
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Verifie L'existence d'une transaction
        /// </summary>
        /// <param name="newTransaction"></param>
        /// <returns>True pour oui</returns>
        internal static bool TransactionExist (Transaction newTransaction) {
            using (var db = new EconomatContext()) {
                if(db.Transactions.Find(newTransaction.TransactionGuid)!=null)
                    return true;
                if(db.Transactions.Any(t => t.TransactionReference.Equals(newTransaction.TransactionReference, StringComparison.CurrentCultureIgnoreCase)))
                    return true;

                return db.Transactions.Any(t => t.PaymentMethode==newTransaction.PaymentMethode&&
                                              t.Designation.Equals(newTransaction.Designation)&&
                                              t.TransactionDate==newTransaction.TransactionDate&&
                                              Math.Abs(t.Amount-newTransaction.Amount)<.0000001);
            }
        }


        #endregion



    }
}
