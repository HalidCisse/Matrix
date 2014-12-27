

using System.ComponentModel;

namespace DataService.Enum
{
    /// <summary>
    /// Les Types de Cours
    /// </summary>
    public enum CoursTypes
    {
        /// <summary>
        /// Cours
        /// </summary>
        [Description("Cours")]
        Cours,

        /// <summary>
        /// Cours Theorique
        /// </summary>
        [Description("Cours Theorique")]
        CoursTheorique,

        /// <summary>
        /// Cours Magistral
        /// </summary>
        [Description("Cours Magistral")]
        CoursMagistral,

        /// <summary>
        /// Travaux Pratiques
        /// </summary>
        [Description("Travaux Pratiques")]
        TravauxPratiques,

        /// <summary>
        /// Travaux Dirigés
        /// </summary>
        [Description("Travaux Dirigés")]
        TravauxDirigés,

        /// <summary>
        /// Revision
        /// </summary>
        [Description("Revision")]
        Revision,

        /// <summary>
        /// Test
        /// </summary>
        [Description("Test")]
        Test,

        /// <summary>
        /// Control
        /// </summary>
        [Description("Control")]
        Control,

        /// <summary>
        /// Examen
        /// </summary>
        [Description("Examen")]
        Examen,

        /// <summary>
        /// Examen
        /// </summary>
        [Description("None")]
        None


    }
}
