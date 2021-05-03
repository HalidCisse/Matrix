using System;
using Common.Economat.Enums;

namespace Common.Economat.Entity
{
    /// <summary>
    /// Payement des salaires
    /// </summary>
    public class Paycheck
    {

        /// <summary>
        /// Guid
        /// </summary>
        public Guid PayrollGuid { get; set; }


        /// <summary>
        /// Exp Salaire de Base
        /// </summary>
        public string Designation { get; set; }


        /// <summary>
        /// Salaire Fixe, Heures Enseignées, Heures Travailer
        /// </summary>
        public PayType PayType { get; set; }


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


    }
}
