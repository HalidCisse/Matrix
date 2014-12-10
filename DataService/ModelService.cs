using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
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
            using (var db = new EF())
            {
                var depStaffCardList = new List<DepStaffCard>();
                var nd = new DepStaffCard("");

                if (nd.STAFFS_LIST.Any()) { depStaffCardList.Add(nd);}

                var deps = (from s in db.STAFF.ToList() where s.DEPARTEMENT != null select s.DEPARTEMENT).Distinct().ToList();

                Parallel.ForEach(deps, dep =>
                {
                    depStaffCardList.Add(new DepStaffCard(dep));
                });

                return depStaffCardList.Any() ? depStaffCardList.OrderBy(d => d?.DEPARTEMENT_NAME).ToList() : depStaffCardList;
            }       
        }

        /// <summary>
        /// Renvoi les filieres avec leurs classes
        /// </summary>
        /// <returns></returns>
        public List<FiliereClassCard> GetFiliereClassCards ( )
        {
            using (var db = new EF())
            {
                var fls = db.FILIERE;

                var classCardList = new List<FiliereClassCard>();
                
                Parallel.ForEach(fls, fil =>
                {
                    var fc = new FiliereClassCard(fil);
                    if (fc.CLASS_LIST.Any()) { classCardList.Add(fc); }
                });

                return classCardList.Any() ? classCardList.OrderBy(f => f?.FILIERE_NAME).ToList() : classCardList;
            }           
        }
        
        /// <summary>
        /// renvoi la filiere avec ses classes
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public List<ClassCard> GetFiliereClassCards ( Guid filiereId )
        {                        
            using (var db = new EF())
            {
                var classList = new List<ClassCard> ();

                Parallel.ForEach(db.CLASSE.Where(c => c.FILIERE_ID == filiereId), c =>
                {
                    classList.Add (new ClassCard (c));
                });

                return classList.Any()? classList.OrderBy(c => c.LEVEL).ToList() : classList;
            }
        }

        /// <summary>
        /// Renvoi les informations des cours d'une classe pour une semaine
        /// </summary>
        /// <param name="classId">ID de la Classe</param>
        /// <param name="scheduleDate">Une date de cette Semaine</param>
        /// <returns></returns>
        public List<DayCoursCards> GetClassWeekAgendaData ( Guid classId, DateTime scheduleDate )
        {
            scheduleDate = scheduleDate.Date; 

            var firstDateOfWeek = scheduleDate.DayOfWeek == DayOfWeek.Sunday ? scheduleDate.AddDays(-6) : scheduleDate.AddDays (-((int)scheduleDate.DayOfWeek - 1));
          
            var scheduleData = new List<DayCoursCards>();

            for (var i = 0; i <= 6; i++)
            {
                var dayCard = new DayCoursCards(classId, firstDateOfWeek.AddDays(i));

                if (dayCard.DAY_COURS.Any())
                {
                    scheduleData.Add(dayCard);
                }                
            }
 
            return scheduleData;
        }

        /// <summary>
        /// Renvoi les Matieres Cards Pour Une Classe
        /// </summary>
        /// <param name="classeId">ID de la Classe</param>
        /// <returns></returns>
        public List<MatiereCard> GetClassMatieresCards(Guid classeId)
        {
            using (var db = new EF())
            {
                var matierCardList = new List<MatiereCard>();

                Parallel.ForEach(db.MATIERE.Where(m => m.CLASSE_ID == classeId), mc =>
                {
                    matierCardList.Add(new MatiereCard(mc));
                });

                return matierCardList.Any() ? matierCardList.OrderBy(m => m?.NAME).ToList() : matierCardList;
            }
        }







    }
 
}









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
