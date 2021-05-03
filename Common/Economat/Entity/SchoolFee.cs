using System;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Economat.Enums;

namespace Common.Economat.Entity
{

    /// <summary>
    /// Frais de Scolarité
    /// </summary>
    public class SchoolFee : Tracable
    {

        /// <summary>
        /// Le Guid du Receipt
        /// </summary>
        [Key]
        public Guid SchoolFeeGuid { get; set; }

        /// <summary>
        /// Le Guid de l'étudiant concernée
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// Motif de ce Payement, exp: Oct-2015
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// La Somme a payer
        /// </summary>
        public double NetAmount { get; set; }

        /// <summary>
        /// Date de payement de la Recue sinon considerer comme dette
        /// </summary>
        public DateTime? DueDate { get; set; }
        
        /// <summary>
        /// Numero de Reference de payement
        /// </summary>
        public string NumeroReference { get; set; }

        /// <summary>
        ///Ce Recue Est Payer ?
        /// </summary>
        ///[NotMapped]
        public bool IsPaid { get; set; }              ///=> IsPaidTo != Guid.Empty;  // todo and if user Guid Exist !

        /// <summary>
        /// IsInstallement
        /// </summary>
        public bool IsInstallement { get; set; }

        /// <summary>
        /// Le Nom du Payeur
        /// </summary>
        public string IsPaidBy { get; set; }

        /// <summary>
        /// Le Guid du User qui a Recue/confirmer ce Payement
        /// </summary>
        public Guid IsPaidTo { get; set; }

        /// <summary>
        /// Date de Payement de ce recue
        /// </summary>
        public DateTime? DatePaid { get; set; }

        /// <summary>
        /// Methode de Payement (Espece, Cheque, virement ..)
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
        /// Description
        /// </summary>
        public string Description { get; set; }



        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual Student Student { get; set; }
    }
}
