using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using CLib;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using Common.Security.Enums;
using DataService.Context;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Filieres
    /// </summary>
    public class FilieresManager
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFiliere"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool AddFiliere(Filiere myFiliere)
        {            
            using (var db = new SchoolContext())
            {
                if (myFiliere.FiliereGuid == Guid.Empty) myFiliere.FiliereGuid = Guid.NewGuid();

                myFiliere.DateAdded = DateTime.Now;
                myFiliere.LastEditDate = DateTime.Now;

                db.Filieres.Add(myFiliere);
                return ((db.SaveChanges() > 0));    //todo ((db.SaveChanges() > 0) && DbHelper.GenerateClasses(myFiliere, )) ;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myFiliere"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool UpdateFiliere(Filiere myFiliere)
        {
            using (var db = new SchoolContext())
            {
                myFiliere.LastEditDate = DateTime.Now;

                db.Filieres.Attach(myFiliere);
                db.Entry(myFiliere).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// SoftDelete Une Filiere
        /// </summary>
        /// <param name="filiereGuid"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool DeleteFiliere(Guid filiereGuid)
        {
            using (var db = new SchoolContext())
            {
                var myFiliere = db.Filieres.Find(filiereGuid);

                myFiliere.LastEditDate = DateTime.Now;
                myFiliere.DeleteDate = DateTime.Now;
                myFiliere.IsDeleted = true;

                db.Filieres.Attach(myFiliere);
                db.Entry(myFiliere).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereGuid"></param>
        /// <returns></returns>
        public Filiere GetFiliereByGuid(Guid filiereGuid)
        {
            using (var db = new SchoolContext())
            {
                var myFiliere = db.Filieres.Find(filiereGuid);
                return myFiliere;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereName"></param>
        /// <returns></returns>
        public Filiere GetFiliereByName(string filiereName)
        {
            using (var db = new SchoolContext())
            {
                var myFiliere = db.Filieres.First(s => s.Name == filiereName);

                return myFiliere;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Filiere> GetAllFilieres()
        {
            using (var db = new SchoolContext())
            {
                return db.Filieres.ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAllFilieresNames()
        {
            var names = new List<string>();
            using (var db = new SchoolContext())
            {
                names.AddRange(db.Filieres.Select(s => s.Name));
                return names;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public int GetFiliereClassCount(Guid filiereId)
        {
            using (var db = new SchoolContext())
            {
                return db.Classes.Count(c => c.FiliereGuid == filiereId);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public string GetFiliereName(Guid? filiereId)
        {
            using (var db = new SchoolContext())
            {                
                return db.Filieres.Find(filiereId)?.Name; 
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public bool FiliereExist(Guid filiereId)
        {
            using (var db = new SchoolContext())
            {
                return db.Filieres.Find(filiereId) != null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereGuid"></param>
        /// <returns></returns>
        public HashSet<Classe> GetFiliereClasses(Guid filiereGuid)
        {
            using (var db = new SchoolContext())
            {
                return new HashSet<Classe>(db.Classes.Where(c => c.FiliereGuid == filiereGuid).OrderBy(c => c.ClassGrade));
            }
        }


        /// <summary>
        /// Renvoi une dictionnaire des classes de cette filiere (Nom, Guid)
        /// </summary>
        /// <param name="filiereGuid"></param>
        /// <returns></returns>
        public Dictionary<string,Guid> GetFiliereClassesDictionary(Guid filiereGuid)
        {
            using (var db = new SchoolContext())
            {
                return
                    db.Classes.Where(c => c.FiliereGuid == filiereGuid)
                        .OrderBy(c => c.ClassGrade)
                        .ToDictionary(c => c.Name, c => c.ClasseGuid);
            }
        }


        /// <summary>
        /// Renvoi les filieres avec leurs classes
        /// </summary>
        /// <returns></returns>
        public List<FiliereClassCard> GetFiliereClassCards () {
            using (var db = new SchoolContext()) {
                var fls = db.Filieres;

                var classCardList = new List<FiliereClassCard>();

                Parallel.ForEach(fls, fil => {
                    var fc = new FiliereClassCard(fil);
                    if(fc.ClassList.Any()) { classCardList.Add(fc); }
                });

                return classCardList.Any() ? classCardList.OrderBy(f => f?.FiliereName).ToList() : classCardList;
            }
        }


        /// <summary>
        /// renvoi la filiere avec ses classes
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public List<ClassCard> GetFiliereClassCards (Guid filiereId) {
            using (var db = new SchoolContext()) {
                var classList = new List<ClassCard>();

                Parallel.ForEach(db.Classes.Where(c => c.FiliereGuid==filiereId), c => {
                    classList.Add(new ClassCard(c));
                });

                return classList.Any() ? classList.OrderBy(c => c.Level).ToList() : classList;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> FilieresAdmissions()
        {
            using (var db = new SchoolContext())
            {
                var ns = db.Filieres.Where(f => !string.IsNullOrEmpty(f.Admission)).Select(f=> f.Admission).ToList();

                ns.AddRange(new List<string> { "Bac", "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat" });

                return ns.Distinct().ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> FilieresDiplomes()
        {
            using (var db = new SchoolContext())
            {
                var ns = db.Filieres.Where(f => !string.IsNullOrEmpty(f.Diplome)).Select(f => f.Diplome).ToList();

                ns.AddRange(new List<string> { "Bac", "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Ingénieur", "Doctorat" });

                return ns.Distinct().ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        public Dictionary<string,ClassGrades> GetFiliereGrades(Guid filiereId)
        {
            using (var db = new SchoolContext())
            {
                var dic = new Dictionary<string, ClassGrades>();

                for (var i = 1; i <= (int)db.Filieres.Find(filiereId).Scolarite; i++)
                    dic.Add(((ClassGrades) i).GetEnumDescription(), (ClassGrades) i);
                return dic;
            }
        }


        /// <summary>
        /// Verifie L'existance d'un Filiere de meme nom
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool FiliereNameExist(string name)
        {
            return StaticFiliereNameExist(name);
        }


        /// <summary>
        /// Verifie L'existance d'un Filiere de meme sigle
        /// </summary>
        /// <param name="sigle"></param>
        /// <returns></returns>
        public bool FiliereSigleExist(string sigle)
        {
            return StaticFiliereSigleExist(sigle);
        }



        #region PRIVATE MEMBERS



        //private static bool GenerateClasses(Filiere filiere)
        //{
        //    var upper = (int)filiere.Scolarite;

        //    for (var i = 1; i <= upper; i++)
        //    {
        //        var newClasse = new Classe { ClasseGuid = Guid.NewGuid(), FiliereGuid = filiere.FiliereGuid, ClassAnnee = (ClassAnnee)i, AnneeScolaireGuid = ThisCurrentAnneeScolaireGuid };

        //        if (i == 1) newClasse.Name = "1 ère Année";
        //        else newClasse.Name = i + " ème Année";
        //        newClasse.Description = newClasse.Name;

        //        using (var db = new SchoolContext())
        //        {
        //            db.Classe.Add(newClasse);
        //            db.SaveChanges();
        //        }                
        //    }
        //    return true;
        //}

        //private static Guid ThisCurrentAnneeScolaireGuid
        //{
        //    get
        //    {
        //        using (var db = new SchoolContext())
        //        {
        //            if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null)
        //                db.SystemSetting.Add(new MatrixSetting());

        //            if (!db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid.Equals(Guid.Empty))
        //                return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;
        //            throw new NoValidDataFoundException("AUCUNE_ANNEE_SCOLAIRE_CORRESPONDANTE");
        //        }
        //    }
        //}




        #endregion



        /// <summary>
        /// Renvoie la List des filieres et leurs Guid => Key = Nom, Value = Guid 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Guid> GetFilieresDictionary()
        {
            using (var db = new SchoolContext())
            {
                return db.Filieres.OrderBy(f => f.Name).ToDictionary(f => f.Name, f => f.FiliereGuid);
            }
        }




        #region Internal Static


        internal static bool StaticFiliereNameExist(string filiereName)
        {
            using (var db = new SchoolContext())
            {
                return db.Filieres.Any(f => f.Name.Equals(filiereName, StringComparison.CurrentCultureIgnoreCase));
            }
        }


        internal static bool StaticFiliereSigleExist(string sigle)
        {
            using (var db = new SchoolContext())
            {
                return db.Filieres.Any(f => f.Sigle.Equals(sigle, StringComparison.CurrentCultureIgnoreCase));
            }
        }





        #endregion

	   
    }
}
