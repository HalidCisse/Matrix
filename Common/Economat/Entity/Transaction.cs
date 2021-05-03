using System;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Economat.Enums;

namespace Common.Economat.Entity {


    /// <summary>
    /// Transaction Recette, Depense, Encaissement, Decaissement, versement
    /// </summary>
    public sealed  class Transaction: Tracable {

        /// <summary>
        /// TransactionGuid
        /// </summary>
        [Key]
        public Guid TransactionGuid { get; set; }

        /// <summary>
        ///TransactionNumber
        /// </summary>
        public string TransactionReference { get; set; }

        /// <summary>
        /// Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// PaidToward
        /// </summary>
        public string PaidToward { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// PaymentMethode
        /// </summary>
        public PaymentMethode PaymentMethode { get; set; }

        /// <summary>
        /// Numero de virement ou de cheque du payement
        /// </summary>
        public string NumeroVirement { get; set; }

        /// <summary>
        /// La banque du cheque ou du virement
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



    }
}
