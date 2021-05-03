using System.ComponentModel;

namespace Common.Security.Enums {

    /// <summary>
    /// Les Roles des Proffs
    /// </summary>
    public enum TeacherClearances {

        /// <summary>
        /// Corriger
        /// </summary>
        [Description("Saisir les Notes")]
        Correcteur,

        /// <summary>
        /// Presence
        /// </summary>
        [Description("Saisir la Presence")]
        Superviseur      

    }
}
