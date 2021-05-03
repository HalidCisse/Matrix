using System.ComponentModel;

namespace Common.Shared.Enums
{
    /// <summary>
    /// Status d'un Staff
    /// </summary>
    public enum StaffStatus
    {
        /// <summary>
        /// Actif
        /// </summary>
        [Description("Actif")]
        Actif,

        /// <summary>
        /// Non Actif
        /// </summary>
        [Description("Non Actif")]
        NonActif,

        /// <summary>
        /// Licencier
        /// </summary>
        [Description("Licencier")]
        Licencier,

        /// <summary>
        /// Irregulier
        /// </summary>
        [Description("Irregulier")]
        Irregulier,

        /// <summary>
        /// Suspendue
        /// </summary>
        [Description("Suspendue")]
        Suspendue

        



        
    }
}
