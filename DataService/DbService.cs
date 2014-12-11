
using DataService.DataManager;

namespace DataService
{
    /// <summary>
    /// Serveur de Donnees
    /// </summary>
    public class DbService 
    {

        /// <summary>
        ///Serveur des Enums
        /// </summary>
        public DataEnums DataEnums = new DataEnums();

        /// <summary>
        /// System de Gestion D'Etude
        /// </summary>
        public PedagogyManager Pedagogy = new PedagogyManager();

        /// <summary>
        /// Gestion D'Etudiants
        /// </summary>
        public StudentsManager Students = new StudentsManager();

        /// <summary>
        /// Gestion Des Ressources Humaines
        /// </summary>
        public HrManager Hr = new HrManager();

        /// <summary>
        /// Gestion Des Parametres
        /// </summary>
        public SettingsManager Settings = new SettingsManager();
 
    }
}









//Task.Factory.StartNew( () => Parallel.ForEach<Item>(items, item => DoSomething(item)));








///// <summary>
///// 
///// </summary>
///// <param name="StaffID"></param>
///// <param name="MatiereID"></param>
///// <returns></returns>
//public bool IsMatiereInstructor(string StaffID, Guid MatiereID)
//{
//    using (var Db = new EF())
//    {
//        return Db.MATIERES_INSTRUCTEURS.First(S => S.MATIERE_ID == MatiereID && S.STAFF_ID == StaffID) != null;
//    }
//}

///// <summary>
///// 
///// </summary>
///// <param name="MatiereID"></param>
///// <param name="StaffID"></param>
///// <returns></returns>
//public bool AddMatiereInstructor(Guid MatiereID, string StaffID)
//{
//    using (var Db = new EF())
//    {

//        var RL = Db.MATIERES_INSTRUCTEURS.First(MI => MI.MATIERE_ID == MatiereID && MI.STAFF_ID == StaffID);
//        if (RL != null) return true;

//        var R = new Matiere_Instructeurs
//        {
//            MATIERE_INSTRUCTEURS_ID = Guid.NewGuid(),
//            MATIERE_ID = MatiereID,
//            STAFF_ID = StaffID
//        };

//        Db.MATIERES_INSTRUCTEURS.Add(R);

//        return Db.SaveChanges() > 0;

//    }
//}

///// <summary>
///// 
///// </summary>
///// <param name="MatiereID"></param>
///// <param name="StaffID"></param>
///// <returns></returns>
//public bool DeleteMatiereInstructor(Guid MatiereID, string StaffID)
//{
//    using (var Db = new EF())
//    {
//        var MI = Db.MATIERES_INSTRUCTEURS.First(M => M.MATIERE_ID == MatiereID && M.STAFF_ID == StaffID);
//        if (MI == null) return true;

//        Db.MATIERES_INSTRUCTEURS.Remove(MI);

//        return Db.SaveChanges() > 0;
//    }
//}