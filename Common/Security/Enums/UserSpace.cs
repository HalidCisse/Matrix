using System.ComponentModel;

namespace Common.Security.Enums
{
    /// <summary>
    /// Represent L'Espace UI de L'Utilisateur
    /// </summary>
    public enum UserSpace
    {
        /// <summary>
        /// Espace Administrateur
        /// </summary>
        [Description("Espace Admin")]
        AdminSpace,

        /// <summary>
        /// Espace Staff ou Enseignant
        /// </summary>
        [Description("Espace Enseignant")]
        TeacherSpace ,

        /// <summary>
        /// Espace Economat
        /// </summary>
        [Description("Espace Comptable")]
        EconomatSpace ,

        /// <summary>
        /// Espace Secretaire
        /// </summary>
        [Description("Espace Secretaire")]
        SecretarySpace ,

        /// <summary>
        /// Espace Etudes
        /// </summary>
        [Description("Espace Etudes")]
        StudySpace ,

        /// <summary>
        /// Espace Etudiant
        /// </summary>
        [Description("Espace Etudiant")]
        StudentSpace 

    }
}
