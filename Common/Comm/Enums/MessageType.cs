using System.ComponentModel;

namespace Common.Comm.Enums {

    /// <summary>
    /// MessageType
    /// </summary>
    public enum MessageType {

        /// <summary>
        /// Privé
        /// </summary>
        [Description("Privé")]
        Personal,

        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        Email,

        /// <summary>
        /// Une Classe
        /// </summary>
        [Description("Une Classe")]
        ToClasse,

        /// <summary>
        /// Aux Etudiants
        /// </summary>
        [Description("Aux Etudiants")]
        ToStudents,

        /// <summary>
        /// Aux Staffs
        /// </summary>
        [Description("Aux Staffs")]
        ToStaffs ,

        /// <summary>
        /// Public
        /// </summary>
        [Description("Public")]
        Open

    }
}
