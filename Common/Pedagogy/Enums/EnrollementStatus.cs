using System.ComponentModel;

namespace Common.Pedagogy.Enums
{

    /// <summary>
    /// Determine les Status d'Une Inscription
    /// </summary>
    public enum EnrollementStatus {

        /// <summary>
        /// Active
        /// </summary>
        [Description("Inachevé")]
        NotCompleted,

        /// <summary>
        /// Suspendue
        /// </summary>
        [Description("Suspendue")]
        Canceled,       

        /// <summary>
        /// Redouble
        /// </summary>
        [Description("Redouble")]
        Failed,

        /// <summary>
        /// Rattrapage
        /// </summary>
        [Description("En Rattrapage")]
        Rattrapage,

        /// <summary>
        /// Passer
        /// </summary>
        [Description("Completer")]
        Passed

    }
}
