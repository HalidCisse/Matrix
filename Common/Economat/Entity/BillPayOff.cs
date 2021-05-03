using System;
using Common.Economat.Enums;

namespace Common.Economat.Entity
{
    /// <summary>
    /// Recue de payement des frais d'etudes
    /// </summary>
    public class BillPayOff
    {


        /// <summary>
        /// Le Guid du Receipt
        /// </summary>
        public Guid SchoolFeeGuid { get; set; }


        /// <summary>
        /// Le Nom du Payeur
        /// </summary>
        public string IsPaidBy { get; set; }


        /// <summary>
        /// Methode de Payement (Espece, Cheque, virement ..)
        /// </summary>
        public PaymentMethode PaymentMethode { get; set; }


        /// <summary>
        /// Numero de Reference de payement
        /// </summary>
        public string NumeroReference { get; set; }


        /// <summary>
        /// Numero de virement ou de cheque du payement
        /// </summary>
        public string NumeroVirement { get; set; }


        /// <summary>
        /// La banque du cheque ou du virement
        /// </summary>
        public string Bank { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



    }
}
