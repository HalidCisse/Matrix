using System.ComponentModel;

namespace Common.Economat.Enums
{
    /// <summary>
    /// Type de Reglement des frais d'etudes
    /// </summary>
    public enum InstallmentRecurrence
    {
        /// <summary>
        /// Une Fois
        /// </summary>
        [Description("Une Fois")]
        Once = 0,


        /// <summary>
        /// Mensuel
        /// </summary>
        [Description("Mensuel")]
        Monthly = 1,


        /// <summary>
        /// Trimestriel
        /// </summary>
        [Description("Trimestriel")]
        Quarterly = 3,


        /// <summary>
        /// Semestriel
        /// </summary>
        [Description("Semestriel")]
        HalfYearly = 6,


        /// <summary>
        /// Annuel
        /// </summary>
        [Description("Annuel")]
        Yearly = 12


        ///// <summary>
        ///// Hebdomadaire
        ///// </summary>
        //[Description("Hebdomadaire")]
        //WklySalary

    }
}
