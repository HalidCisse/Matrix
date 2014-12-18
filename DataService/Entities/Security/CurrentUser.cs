using DataService.Context;
using DataService.Enum;

namespace DataService.Entities.Security
{
    /// <summary>
    /// represent l'utilisateur Actuelle Avec ses Parametres et Roles
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// Determine L'Utilisateur Actuelle
        /// </summary>
        /// <param name="userProfile"></param>
        public CurrentUser(UserProfile userProfile = null)
        {
            if (userProfile == null)
            {
                userProfile = new UserProfile
                {
                    UserProfileGuid = MatrixConstants.SystemGuid(),
                    UserSpace = UserSpace.AdminSpace
                };
            }

            using (var db = new Ef())
            {                
                var st = db.Staff.Find(userProfile.UserProfileGuid);

                FirstName = st.FirstName;
                LastName = st.LastName;
                PhotoIdentity = st.PhotoIdentity;
                UserSpace = userProfile.UserSpace;
                UserSettings = db.UserSetting.Find(userProfile.UserProfileGuid);
                UserRoles = db.UserRole.Find(userProfile.UserProfileGuid);
            }

        }

        /// <summary>
        /// Le Prenom de L'Utilisateur
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Le Nom de L'Utilisateur
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Photo de l'Utilisateur
        /// </summary>
        public byte[] PhotoIdentity { get; }

        /// <summary>
        /// Espace de L'Utilisateur
        /// </summary>
        public UserSpace UserSpace { get; }

        /// <summary>
        /// Les Prametres de L'Utilisateur
        /// </summary>
        public UserSetting UserSettings { get; }

        /// <summary>
        /// Les Prametres de L'Utilisateur
        /// </summary>
        public UserRoles UserRoles { get; }


    }
}
