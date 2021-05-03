using System;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Economat.Enums;

namespace Common.Economat.Entity
{

    /// <summary>
    /// Salaire d'un Staff
    /// </summary>
    public class Payroll : Tracable 
    {
       
        /// <summary>
        /// Guid
        /// </summary>
        [Key]
        public Guid PayrollGuid { get; set; }

        /// <summary>
        /// Reference de l'employement
        /// </summary>
        public Guid EmploymentGuid { get; set; }

        /// <summary>
        /// Exp Salaire de Base
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Date de versement de salaire
        /// </summary>
        public DateTime? PaycheckDate { get; set; }

        /// <summary>
        ///Cette remuneration Est Payer ?
        /// </summary>
        ///[NotMapped]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Le Guid du User qui a Recue/confirmer ce Payement
        /// </summary>
        public Guid IsPaidTo { get; set; }

        /// <summary>
        /// Date de Payement de ce recue
        /// </summary>
        public DateTime? DatePaid { get; set; }

        /// <summary>
        /// Nombre d'heures Travailer ou Enseigner
        /// </summary>
        public TimeSpan HoursWorked { get; set; }

        /// <summary>
        /// Valeur final de la Renumeration
        /// </summary>
        public double FinalPaycheck { get; set; }

        /// <summary>
        /// Numero de Reference de payement
        /// </summary>
        public string NumeroReference { get; set; }

        /// <summary>
        /// Methode de Payement (Espece, Cheque, virement ..)
        /// </summary>
        public PaymentMethode PaymentMethode { get; set; }
        
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual Employment Employment { get; set; }
    }
}
