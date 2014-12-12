using System;
using DataService.Context;
using DataService.Entities.Pedagogy;

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
            newPeriodeScolaire.PeriodeScolaireId = Guid.NewGuid();
            using (var db = new Ef())
            {
                db.PeriodeScolaire.Add(newPeriodeScolaire);
                return db.SaveChanges() > 0;
            }
        }

    }
}
