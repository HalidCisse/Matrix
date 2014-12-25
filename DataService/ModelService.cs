using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;
using DataService.Enum;
using DataService.ViewModel;

namespace DataService
{
    /// <summary>
    /// Modelisateur des donnees 
    /// </summary>
    public class ModelService
    {
             
        /// <summary>
        /// Renvoi la list des departements avec leur employer
        /// </summary>
        /// <returns></returns>
        public List<DepStaffCard> GetDepStaffsCard ( )
        {                       
            using (var db = new Ef())
            {
                var depStaffCardList = new ConcurrentBag<DepStaffCard>();

                var nd = new DepStaffCard("");
                if (nd.StaffsList.Any()) { depStaffCardList.Add(nd); }

                var deps = (db.Staff.ToList()
                    .Where(s => string.IsNullOrEmpty(s.Departement) == false)
                    .Select(s => s.Departement)).Distinct().ToList();
                                
                Parallel.ForEach(deps, dep =>
                {
                    depStaffCardList.Add(new DepStaffCard(dep));
                });

                return depStaffCardList.Any() ? depStaffCardList.OrderBy(d => d.DepartementName).ToList() : null;
            }       
        }


        /// <summary>
        /// Renvoi les filieres avec leurs classes
        /// </summary>
        /// <returns></returns>
        public List<FiliereClassCard> GetFiliereClassCards ( )
        {
            using (var db = new Ef())
            {
                var fls = db.Filiere;

                var classCardList = new List<FiliereClassCard>();
                
                Parallel.ForEach(fls, fil =>
                {
                    var fc = new FiliereClassCard(fil);
                    if (fc.ClassList.Any()) { classCardList.Add(fc); }
                });

                return classCardList.Any() ? classCardList.OrderBy(f => f?.FiliereName).ToList() : classCardList;
            }           
        }
        

        /// <summary>
        /// renvoi la filiere avec ses classes
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public List<ClassCard> GetFiliereClassCards ( Guid filiereId )
        {                        
            using (var db = new Ef())
            {
                var classList = new List<ClassCard> ();

                Parallel.ForEach(db.Classe.Where(c => c.FiliereGuid == filiereId), c =>
                {
                    classList.Add (new ClassCard (c));
                });

                return classList.Any()? classList.OrderBy(c => c.Level).ToList() : classList;
            }
        }


        /// <summary>
        /// Renvoi les informations des cours d'une classe pour une semaine
        /// </summary>
        /// <param name="classId">ID de la Classe</param>
        /// <param name="scheduleDate">Une date de cette Semaine</param>
        /// <returns></returns>
        public ConcurrentBag<DayCoursCards> GetClassWeekAgendaData ( Guid classId, DateTime scheduleDate )
        {
            scheduleDate = scheduleDate.Date; 

            var firstDateOfWeek = scheduleDate.DayOfWeek == DayOfWeek.Sunday ? scheduleDate.AddDays(-6) : scheduleDate.AddDays (-((int)scheduleDate.DayOfWeek - 1));
          
            var scheduleData = new ConcurrentBag<DayCoursCards>();

            var days = new HashSet<DateTime>();

            for (var i = 0; i <= 6; i++) days.Add(firstDateOfWeek.AddDays(i));

            Parallel.ForEach(days, d =>
            {
                var dayCard = new DayCoursCards(classId, d);

                if (dayCard.DayDate.Equals(scheduleDate) && scheduleDate != DateTime.Today) dayCard.DayColor = "Brown";

                if (dayCard.DayCours.Any()) scheduleData.Add(dayCard);
            });
                      
            return new ConcurrentBag<DayCoursCards>(scheduleData.OrderByDescending(d => d.DayDate));
        }


        /// <summary>
        /// Renvoi les Matieres Cards Pour Une Classe
        /// </summary>
        /// <param name="classeGuid">ID de la Classe</param>
        /// <param name="currentDate">La Date Actuelle</param>
        /// <returns></returns>
        public ConcurrentBag<MatiereCard> GetClassMatieresCards(Guid classeGuid, DateTime currentDate)
        {
            using (var db = new Ef())
            {                
                currentDate = currentDate.Date;
                var firstDateOfWeek = currentDate.DayOfWeek == DayOfWeek.Sunday ? currentDate.AddDays(-6) : currentDate.AddDays(-((int)currentDate.DayOfWeek - 1));
                var lastDateOfWeek = firstDateOfWeek.AddDays(6);

                var currentCours = db.Cours.Where(c =>

                                                c.ClasseGuid == classeGuid &&
                                                (
                                                    (
                                                        c.StartDate <= firstDateOfWeek &&
                                                        c.EndDate >= lastDateOfWeek
                                                    )
                                                    |
                                                    (
                                                        c.EndDate >= firstDateOfWeek &&
                                                        c.EndDate <= lastDateOfWeek
                                                    )
                                                    |
                                                    (
                                                        c.StartDate >= firstDateOfWeek &&
                                                        c.StartDate <= lastDateOfWeek
                                                    )
                                                )

                                             ).OrderBy(c => c.StartTime);

                var currentMatieres = new ConcurrentBag<MatiereCard>();

                Parallel.ForEach(currentCours, cour =>
                {
                    var m = GetMatiereById(cour.MatiereGuid);
                    if (m != null) currentMatieres.Add(new MatiereCard(m));                   
                });
                                
                return currentMatieres;               
            }
        }      


        /// <summary>
        /// Return Toutes Les Matieres de cette Classe
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <returns></returns>
        public IOrderedEnumerable<MatiereCard> GetClassMatieresCards(Guid classeGuid)
        {
            using (var db = new Ef())
            {               
                var matierCardList = new ConcurrentBag<MatiereCard>();

                Parallel.ForEach(db.Matiere.Where(m => m.ClasseGuid == classeGuid), mc =>
                {
                    matierCardList.Add(new MatiereCard(mc));
                });

                return matierCardList.OrderBy(m => m.Name);
            }
        }


        /// <summary>
        /// Model des Presences
        /// </summary>
        /// <param name="currentCoursGuid"></param>
        /// <param name="coursDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable GetAbsencesTiketCards(Guid currentCoursGuid, DateTime coursDate)
        {                        
                var tiketList = new ConcurrentBag<AbsenceTicketCard>() {new AbsenceTicketCard(GetCoursStaffGuid(currentCoursGuid), currentCoursGuid, coursDate) };
                
                var stdsGuids = GetClassStudentsGuids(GetCoursClasseGuid(currentCoursGuid), GetCoursAnneeScolaireGuid(currentCoursGuid));

                Parallel.ForEach(stdsGuids, std =>
                {
                    tiketList.Add(new AbsenceTicketCard(std, currentCoursGuid, coursDate));
                });

                return tiketList.OrderBy(m => m.FullName);
            
        }





        #region HELPERS

        private static List<Guid> GetClassStudentsGuids(Guid classGuid, Guid anneeScolaireGuid)
        {
            using (var db = new Ef())
            {
                var students =
                    new List<Guid>(db.Inscription.Where(
                        i => i.ClasseGuid.Equals(classGuid) && i.AnneeScolaireGuid.Equals(anneeScolaireGuid)).Select(i => i.StudentGuid));

                return students;
            }
        }

        private static Guid GetCoursClasseGuid(Guid coursGuid)
        {
            using (var db = new Ef())
            {
                return db.Cours.Find(coursGuid).ClasseGuid;
            }
        }

        private static Cours GetCours(Guid coursGuid)
        {
            using (var db = new Ef())
            {
                return db.Cours.Find(coursGuid);
            }
        }

        private static Guid GetCoursStaffGuid(Guid coursGuid)
        {
            using (var db = new Ef())
            {
                return db.Cours.Find(coursGuid).StaffGuid;
            }
        }

        private static Guid GetCoursAnneeScolaireGuid(Guid coursGuid)
        {
            using (var db = new Ef())
            {               
                return db.PeriodeScolaire.Find(db.Cours.Find(coursGuid).PeriodeScolaireGuid)?.AnneeScolaireGuid ?? GetCurrentAnneeScolaireGuid;
            }
        }

        private static Guid GetCurrentAnneeScolaireGuid
        {
            get
            {
                using (var db = new Ef())
                {
                    if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null)
                        db.MatrixSetting.Add(new MatrixSetting());

                    return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
                }
            }
        }

        private static Matiere GetMatiereById(Guid matiereId)
        {
            using (var db = new Ef())
            {                
                return db.Matiere.Find(matiereId);
            }
        }

        #endregion

       
    }
 
}











//for (var i = 0; i <= 6; i++)
//{
//    var dayCard = new DayCoursCards(classId, firstDateOfWeek.AddDays(i));

//    if (dayCard.DayDate.Equals(scheduleDate)  && scheduleDate != DateTime.Today) dayCard.DayColor = "Red";

//    if (dayCard.DayCours.Any()) scheduleData.Add(dayCard);                            
//}




//private readonly DbService DS = new DbService ();

//public List<FiliereCard> GetAllFilieresCards ( )
//{
//    using(var Db = new EF ())
//    {
//        var FL = new List<FiliereCard> ();

//        Parallel.ForEach (Db.FILIERE, F =>
//        {
//            FL.Add (new FiliereCard (F));
//        });
//        return FL;
//    }
//}

//public List<FiliereLevelCard> GetFiliereMatieresCards ( Guid FiliereID )
//{

//    var MatiereCardList = new List<FiliereLevelCard> ();
//    var Ds = new DbService();

//    foreach(int Level in Ds.GetFILIERE_NIVEAUX (FiliereID))
//    {
//        MatiereCardList.Add (new FiliereLevelCard (FiliereID, Level));
//    }
//    return MatiereCardList;                       
//}


//public List<MatiereCard> GetClassMatieresCards ( Classe MyClasse )
//{                        
//    using(var Db = new EF ())
//    {
//        var MATIERES_LIST = new List<MatiereCard> ();
//        foreach(var M in Db.MATIERE.Where (M => M.FILIERE_ID == MyClasse.FILIERE_ID && M.FILIERE_LEVEL == MyClasse.LEVEL))
//        {
//            MATIERES_LIST.Add (new MatiereCard (M));
//        }
//        return MATIERES_LIST;
//    }
//}
