


using System.ComponentModel;

namespace DataService.Enum
{
    /// <summary>
    /// Determine les Status d'Une Inscription
    /// </summary>
    public enum InscriptionStatus
    {
        /// <summary>
        /// Active
        /// </summary>
        [Description("Active")]
        Active = 1,

        /// <summary>
        /// Suspendue
        /// </summary>
        [Description("Suspendue")]
        Suspendue = 2

    }
}
