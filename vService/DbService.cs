
using DataService.DataManager;

namespace DataService
{
    /// <summary>
    /// Serveur de Donnees
    /// </summary>
    public sealed class DbService 
    {

        /// <summary>
        /// System de Gestion D'Etude
        /// </summary>
        public PedagogyManager Pedagogy               = new PedagogyManager();


        /// <summary>
        /// Gestion D'Etudiants
        /// </summary>
        public StudentsManager Students               = new StudentsManager();


        /// <summary>
        /// Gestion Des Ressources Humaines
        /// </summary>
        public HrManager HumanResource                = new HrManager();


        /// <summary>
        /// Gestion des Profiles d'utilisateurs et de Securité
        /// </summary>
        public SecurityManager Authentication         = new SecurityManager();


        /// <summary>
        /// Gestion des Ressources Financieres
        /// </summary>
        public EconomatManager Economat               = new EconomatManager();


        /// <summary>
        /// Gestion des Documents Etudiants et Staffs
        /// </summary>
        public DocumentsManager Documents             = new DocumentsManager();


        /// <summary>
        /// Messagerie
        /// </summary>
        public CommManager Comm = new CommManager();


        /// <summary>
        /// Analytics
        /// </summary>
        public AnalyticsManager Analytics = new AnalyticsManager();




        //// <summary>
        //// Gestion Des Parametres System
        //// </summary>
        //public MatrixSettingsManager Etablissement = new MatrixSettingsManager();

        ///// <summary>
        ///// l'Utilisateur Actuelle
        ///// </summary>
        //public CurrentUser CurrentUser = new CurrentUser();


        //// <summary>
        //// l'Utilisateur Actuelle
        //// </summary>
        //public UserProfilesManager UserProfile = new UserProfilesManager();



    }
}









//Task.Factory.StartNew( () => Parallel.ForEach<Item>(items, item => DoSomething(item)));





