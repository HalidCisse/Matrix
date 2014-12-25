using System;
using System.Collections.Generic;
using System.Linq;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;
using DataService.Enum;

namespace DataService.DataManager
{
    /// <summary>
    /// Systeme de Gestion d'Etudes
    /// </summary>
    public class PedagogyManager
    {
        /// <summary>
        /// Gestion Des Classes
        /// </summary>
        public ClassesManager Classes = new ClassesManager();


        /// <summary>
        /// Gestion des Filieres
        /// </summary>
        public FilieresManager Filieres = new FilieresManager();


        /// <summary>
        /// Gestion des Matieres
        /// </summary>
        public MatieresManager Matieres = new MatieresManager();


        /// <summary>
        /// Gestion Des Cours
        /// </summary>
        public CoursManager Cours = new CoursManager();


        /// <summary>
        /// Gestion Des Inscription
        /// </summary>
        public InscriptionsManager Inscriptions = new InscriptionsManager();


        /// <summary>
        /// Ajouter Une Nouvelle Annee Scolaire, Return True if Successful
        /// </summary>
        /// <param name="newAnneeScolaire">Object AnneeScolaire</param>
        /// <returns></returns>
        public bool AddAnneeScolaire(AnneeScolaire newAnneeScolaire)
        {
            using (var db = new Ef())
            {
                db.AnneeScolaire.Add(newAnneeScolaire);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Ajouter Nouvelle Periode Pour Une Annee Scolaire, Return True if Successful
        /// </summary>
        /// <param name="newPeriodeScolaire"> Object PeriodeScolaire</param>
        public bool AddPeriodeScolaire(PeriodeScolaire newPeriodeScolaire)
        {
            newPeriodeScolaire.PeriodeScolaireGuid = Guid.NewGuid();
            using (var db = new Ef())
            {
                db.PeriodeScolaire.Add(newPeriodeScolaire);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Renvoi la List de Tous les Annee Scolaires
        /// </summary>
        /// <returns>Une Dictionaire</returns>        
        public Dictionary<string, string> GetAllAnneeScolaires()
        {
            using (var db = new Ef())
            {                
                return db.AnneeScolaire.OrderBy(a => a.DateDebut).ToDictionary(a => a.Name, a => a.AnneeScolaireGuid.ToString());
            }
        }


        /// <summary>
        ///L'Annee Scolaire Actuelle
        /// </summary>
        public Guid CurrentAnneeScolaireGuid
        {
            get
            {
                using (var db = new Ef())
                {
                    if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null) db.MatrixSetting.Add(new MatrixSetting());

                    return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
                }
            }
            set
            {
                using (var db = new Ef())
                {
                    if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null) db.MatrixSetting.Add(new MatrixSetting());

                    db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid = value;
                    db.SaveChanges();
                }
            }
        }


        /// <summary>
        /// La Periode Scolaire Actuelle
        /// </summary>
        public Guid CurrentPeriodeScolaireGuid
        {
            get
            {
                using (var db = new Ef())
                {
                    if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null) db.MatrixSetting.Add(new MatrixSetting());

                    return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;
                }
            }
            set
            {
                using (var db = new Ef())
                {
                    if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null) db.MatrixSetting.Add(new MatrixSetting());

                    db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid = value;
                    db.SaveChanges();
                }
            }
        }


        /// <summary>
        /// La Periode Scolaire Actuelle
        /// </summary>
        public Guid GetCurrentPeriodeScolaireGuid
        {
            get
            {
                using (var db = new Ef())
                {
                    var psList = db.PeriodeScolaire.Where(ps => ps.AnneeScolaireGuid.Equals(CurrentPeriodeScolaireGuid));

                    foreach (var ps in psList)
                    {
                        if (ps.StartDate <= DateTime.Today && DateTime.Today <= ps.EndDate) return ps.PeriodeScolaireGuid;                      
                    }
                    return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;
                }
            }           
        }




    }
}














///// <summary>
///// return la L'Annee Scolaire Actuelle
///// </summary>
///// <returns></returns>       
//private Guid CurrentAnneeScolaireGuid1()
//{
//    using (var db = new Ef())
//    {
//        if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null)
//            db.MatrixSetting.Add(new MatrixSetting());
//        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
//    }
//}


///// <summary>
///// return la Periode Scolaire Actuelle
///// </summary>
///// <returns></returns>
//private Guid CurrentPeriodeScolaireGuid1()
//{
//    using (var db = new Ef())
//    {
//        if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null)
//            db.MatrixSetting.Add(new MatrixSetting());
//        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;
//    }
//}
