using System;
using System.Collections.Generic;
using System.Linq;
using CLib.Exceptions;
using Common.Pedagogy.Entity;
using Common.Shared.Entity;
using Common.Shared.Enums;
using DataService.Context;

namespace DataService.DataManager
{
    /// <summary>
    /// Systeme de Gestion d'Etudes
    /// </summary>
    public class PedagogyManager
    {

        #region COMPOSITION MEMBERS 


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
        public SubjectsManager Subjects = new SubjectsManager();


        /// <summary>
        /// Gestion Des Cours
        /// </summary>
        public StudyManager Study = new StudyManager();


        /// <summary>
        /// Gestion Des Inscription
        /// </summary>
        public EnrollementManager Enrollments = new EnrollementManager();


        /// <summary>
        /// Gestion Des Absence/Retard
        /// </summary>
        public AbsenceManager AbsenceTickets = new AbsenceManager();


        /// <summary>
        /// Gestion Des Notes des Etudiants
        /// </summary>
        public GradesManager Grades = new GradesManager();


        #endregion





        #region MEMBERS METHODES


        /// <summary>
        /// Periode Scolaire
        /// </summary>
        /// <param name="periodeGuid"></param>
        /// <returns></returns>
        public SchoolPeriod GetPeriodeScolaire (Guid periodeGuid) {
            using (var db = new SchoolContext())
                return db.SchoolPeriods.Find(periodeGuid);
        }


        /// <summary>
        /// List des periodes dont un etudiant est Inscris
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetStudentPeriodesScolaires (Guid studentGuid) {
            var periodes = new List<SchoolPeriod>();

            foreach (
                var anScoGuid in
                    EnrollementManager.GetStudentInscriptions(studentGuid)
                        .Select(i => i.SchoolYearGuid)
                        .ToList())
                periodes.AddRange(GetPeriodeScolaires(anScoGuid).ToList());

            return periodes.Where(p => p.StartDate<=DateTime.Today).OrderBy(a => a.StartDate)
                .ToDictionary(a => a.Name + " (" + a.StartDate.GetValueOrDefault().Year +")", a => a.SchoolPeriodGuid.ToString());                         
        }


        /// <summary>
        /// Ajouter Une Nouvelle Annee Scolaire, Return True if Successful
        /// </summary>
        /// <param name="newSchoolYear">Object AnneeScolaire</param>
        /// <param name="periodeScolaires">Les Periodes Scolaire de cette Annee Scolaire</param>
        /// <exception cref="InvalidOperationException">SCHOOL_YEAR_NAME_ALREADY_EXIST || SCHOOL_YEAR_SESSION_ALREADY_EXIST</exception>
        /// <returns>True for success</returns>
        public bool AddAnneeScolaire(SchoolYear newSchoolYear, List<SchoolPeriod> periodeScolaires )
        {
            if (AnneeScolaireNameExist(newSchoolYear.Name))  throw new InvalidOperationException("SCHOOL_YEAR_NAME_ALREADY_EXIST");
            if (SessionValid(newSchoolYear.Session))     throw new InvalidOperationException("SCHOOL_YEAR_SESSION_ALREADY_EXIST");

            using (var db = new SchoolContext())
            {
                if (newSchoolYear.SchoolYearGuid == Guid.Empty)
                    newSchoolYear.SchoolYearGuid = Guid.NewGuid();
                newSchoolYear.DateAdded    = DateTime.Now;
                newSchoolYear.LastEditDate = DateTime.Now;

                db.SchoolYears.Add(newSchoolYear);

                if (db.SaveChanges() <= 0) return false;

                foreach (var ps in periodeScolaires)
                {
                    ps.SchoolYearGuid = newSchoolYear.SchoolYearGuid;
                    if (AddPeriodeScolaire(ps) != true) break;
                }
                return true;
            }
        }


        /// <summary>
        /// Ajouter Nouvelle Periode Pour Une Annee Scolaire, Return True if Successful
        /// </summary>
        /// <param name="newSchoolPeriod"> Object PeriodeScolaire</param>
        private static bool AddPeriodeScolaire(SchoolPeriod newSchoolPeriod)
        {
            if (newSchoolPeriod.SchoolPeriodGuid == Guid.Empty)
                newSchoolPeriod.SchoolPeriodGuid = Guid.NewGuid();

            using (var db = new SchoolContext())
            {
                db.SchoolPeriods.Add(newSchoolPeriod);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Renvoi la Liste de Toutes Les Sessions 
        /// </summary>
        /// <returns></returns>
        public List<string> GetSessions()
        {
            using (var db = new SchoolContext())
            {
                var ss = new List<string>();
                ss.AddRange(db.SchoolYears.Select(a => a.Session).ToList());
                ss.Add("Eté");
                ss.Add("Printemps");
                ss.Add("Automne");
                ss.Add("Jour");
                ss.Add("Soir");
                return ss.Distinct().ToList() ;
            }
        }


        /// <summary>
        /// Renvoi la List de Tous les Annee Scolaires
        /// </summary>
        /// <returns>Une Dictionaire</returns>        
        public Dictionary<string, string> AllAnneeScolaires()
        {
            using (var db = new SchoolContext())
                return db.SchoolYears.OrderBy(a => a.DateDebut)
                    .ToDictionary(a => a.Name, a => a.SchoolYearGuid.ToString());
        }


        /// <summary>
        ///L'Annee Scolaire Actuelle
        /// </summary>
        public Guid? CurrentAnneeScolaireGuid
        {
            get
            {
                using (var db = new SchoolContext())
                {
                    if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null) db.SystemSetting.Add(new MatrixSetting());

                    if (!db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid.Equals(Guid.Empty))
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
                   //throw new NoValidDataFoundException("AUCUNE_ANNEE_SCOLAIRE_CORRESPONDANTE");
                    return null;
                }
            }
            set
            {
                using (var db = new SchoolContext())
                {
                    if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null) db.SystemSetting.Add(new MatrixSetting());

                    if (value != null)
                        db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid = (Guid) value;
                    db.SaveChanges();
                }
            }
        }


        /// <summary>
        /// Renvoi l'annee Par Defaut de ce Etudiant
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SchoolYear GetStudentDefaultYear(Guid studentGuid)
        {
            using (var db = new SchoolContext())
            {
                var studIns = db.Enrollements.Where(ins => ins.StudentGuid == studentGuid);

                foreach (var ins in studIns)
                {
                    var an = db.SchoolYears.Find(ins.SchoolYearGuid);

                    if (an.DateDebut<= DateTime.Today && an.DateFin >= DateTime.Today) return an;
                }
                return null;
            }
        }


        /// <summary>
        /// La Periode Scolaire Actuelle
        /// </summary>
        public Guid GetCurrentLogicPeriodeScolaireGuid
        {
            get
            {
                using (var db = new SchoolContext())
                {
                    var psList = db.SchoolPeriods.Where(ps => ps.SchoolYearGuid.Equals(CurrentAnneeScolaireGuid));

                    foreach (var ps in psList)
                    {
                        if (ps.StartDate <= DateTime.Today && DateTime.Today <= ps.EndDate) return ps.SchoolPeriodGuid;                      
                    }
                    throw new NoValidDataFoundException("AUCUNE_PERIODE_SCOLAIRE_CORRESPONDANTE");                  
                }
            }           
        }


        /// <summary>
        /// Revoie La Liste de Toutes Les Salles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> AllSalles()
        {
            using (var db = new SchoolContext())
                return
                    (from s in db.Studies.ToList() where !String.IsNullOrEmpty(s.Room) select s.Room).Distinct().ToList();                        
        }   


        /// <summary>
        /// Verifie si l annee Scolaire Existe
        /// </summary>
        /// <param name="anneeScolaireGuid"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool AnneeScolaireExist(Guid anneeScolaireGuid)
        {
            using (var db = new SchoolContext())
            {
                return db.SchoolYears.Find(anneeScolaireGuid) != null;
            }
        }


        /// <summary>
        /// Verifie l'existence d'une Annee Scolaire courant de Meme nom
        /// </summary>
        /// <param name="anneeScolaireName"></param>
        /// <returns></returns>
        public bool AnneeScolaireNameExist(string anneeScolaireName)
        {
            return StaticAnneeScolaireNameExist(anneeScolaireName);
        }


        /// <summary>
        /// Verifie l'existence d'une Annee Scolaire courant de Meme session
        /// </summary>
        /// <param name="nomSession"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SessionValid(string nomSession)
        {
            return StaticSessionValid(nomSession);
        }


        /// <summary>
        /// Verifie l'existence d'une Annee Scolaire courant de Meme session
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <returns></returns>
        public bool ClasseValid(Guid classeGuid)
        {
            return ClassesManager.StaticClasseValid(classeGuid);
        }


        /// <summary>
        /// Return La List des Sessions Actives
        /// </summary>
        /// <returns></returns>
        public List<string> GetActiveSession()
        {
            using (var db = new SchoolContext())
                return
                    db.SchoolYears.Where(a => a.DateDebut <= DateTime.Today && a.DateFin >= DateTime.Today)
                        .OrderBy(a => a.Session)
                        .Select(a => a.Session)
                        .Distinct()
                        .ToList();
        }


        /// <summary>
        /// Return La List des Sessions deja planifier
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> AllSession ()
        {
            using (var db = new SchoolContext())
                return
                    db.SchoolYears.OrderBy(a => a.Session)
                        .Select(a => a.Session)
                        .Distinct().ToList();
        }




        #endregion






        #region INTERNAL STATIC

        internal static List<SchoolPeriod> GetPeriodeScolaires(Guid anneeScolaireGuid)
        {
            using (var db = new SchoolContext())
                return db.SchoolPeriods.Where(ps => ps.SchoolYearGuid == anneeScolaireGuid).ToList();
        }

        /// <summary>
        /// Renvoi l'annee Par Defaut de ce Etudiant
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        internal static SchoolYear StaticGetStudentDefaultYear(Guid studentGuid)
        {
            using (var db = new SchoolContext())
            {
                var studIns = db.Enrollements.Where(ins => ins.StudentGuid == studentGuid);

                foreach (var ins in studIns)
                {
                    var an = db.SchoolYears.Find(ins.SchoolYearGuid);

                    if (an.DateDebut <= DateTime.Today && an.DateFin >= DateTime.Today) return an;
                }
                return null;
            }
        }

        /// <summary>
        /// Renvoi l'annee Par Defaut de ce Etudiant
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        internal static Guid? StaticGetStudentDefaultYearGuid(Guid studentGuid)
        {
            using (var db = new SchoolContext())
            {
                var studIns = db.Enrollements.Where(ins => ins.StudentGuid == studentGuid);

                foreach (var ins in studIns)
                {
                    var an = db.SchoolYears.Find(ins.SchoolYearGuid);

                    if (an.DateDebut <= DateTime.Today && an.DateFin >= DateTime.Today) return an.SchoolYearGuid;
                }
                return null;
            }
        }

        internal static SchoolYear StaticGetAnneeScolaireByGuid(Guid anneeGuid)
        {
            using (var db = new SchoolContext())
            {
                return db.SchoolYears.Find(anneeGuid);
            }
        }

        internal static Guid StaticThisCurrentAnneeScolaireGuid
        {
            get
            {
                using (var db = new SchoolContext())
                {
                    if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null)
                        db.SystemSetting.Add(new MatrixSetting());

                    if (!db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid.Equals(Guid.Empty))
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
                    throw new NoValidDataFoundException("AUCUNE_ANNEE_SCOLAIRE_CORRESPONDANTE");
                }
            }
        }

        internal static Guid StaticGetCurrentPeriodeScolaireGuid
        {
            get
            {
                using (var db = new SchoolContext())
                {
                    var psList = db.SchoolPeriods.Where(ps => ps.SchoolYearGuid.Equals(StaticGetDefaultAnneeScolaireGuid));

                    foreach (var ps in psList) if (ps.StartDate <= DateTime.Today && DateTime.Today <= ps.EndDate) return ps.SchoolPeriodGuid;

                    return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;
                }
            }
        }

        internal static bool StaticAnneeScolaireNameExist(string anneeScolaireName)
        {
            using (var db = new SchoolContext())
            {
                return db.SchoolYears.Any(a => a.DateDebut <= DateTime.Today && a.DateFin >= DateTime.Today && a.Name.Equals(anneeScolaireName, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        internal static bool StaticAnneeScolaireValid(Guid anneeScolaireGuid)
        {
            using (var db = new SchoolContext())
            {
                var anneeScolaire = db.SchoolYears.Find(anneeScolaireGuid);
                return (anneeScolaire.DateDebut <= DateTime.Today && anneeScolaire.DateFin >= DateTime.Today);
            }
        }

        internal static bool StaticSessionValid(string nomSession)
        {
            using (var db = new SchoolContext())
            {
                return db.SchoolYears.Any(a => a.DateDebut <= DateTime.Today && a.DateFin >= DateTime.Today && a.Session.Equals(nomSession, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        internal static Guid StaticGetDefaultAnneeScolaireGuid
        {
            get
            {
                using (var db = new SchoolContext())
                {
                    if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null)
                        db.SystemSetting.Add(new MatrixSetting());

                    return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
                }
            }
        }

        internal static Guid? StaticGetSessionAnneeScolaireGuid(string classeSession)
        {
            using (var db = new SchoolContext())
            {
                return db.SchoolYears.FirstOrDefault(a => a.DateDebut <= DateTime.Today && a.DateFin >= DateTime.Today && a.Session.Equals(classeSession, StringComparison.CurrentCultureIgnoreCase))?.SchoolYearGuid;
            }
        }


        #endregion

        
    }
}













