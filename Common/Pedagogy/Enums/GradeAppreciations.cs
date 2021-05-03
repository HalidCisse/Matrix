using System.ComponentModel;

namespace Common.Pedagogy.Enums
{
    /// <summary>
    /// Appreciations de Notes de Corrections
    /// </summary>
    public enum GradeAppreciations
    {       

        /// <summary>
        /// Excellent/Parfait/Exceptionnel  20/20
        /// </summary>
        [Description("Excellent")]        
        Excellent = 20,

        /// <summary>
        /// Tres Bien (19, 18, 17)/20
        /// </summary>
        [Description("Tres Bien")]
        TresBien = 17,

        /// <summary>
        /// Bien (16, 15, 14)/20
        /// </summary>
        [Description("Bien")]
        Bien = 14,

        /// <summary>
        /// Assez Bien (13, 12)/20
        /// </summary>
        [Description("Assez bien")]
        AssezBien = 12,

        /// <summary>
        /// Passable (11, 10, 9)/20
        /// </summary>
        [Description("Passable")]
        Passable = 9,

        /// <summary>
        /// Mediocre (8, 7)/20
        /// </summary>
        [Description("Mediocre")]
        Mediocre = 7,

        /// <summary>
        /// Mediocre (6, 5, 4)/20
        /// </summary>
        [Description("Mal")]
        Mal = 4,

        /// <summary>
        /// Mediocre (3, 2, 1)/20
        /// </summary>
        [Description("Très Mal")]
        TrèsMal = 1,

        /// <summary>
        /// Null 0/20
        /// </summary>
        [Description("Null")]
        Null = 0

    }
}
