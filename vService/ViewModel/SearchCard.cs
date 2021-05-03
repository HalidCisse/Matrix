using System;
using System.Linq;
using DataService.Context;
using DataService.DataManager;

namespace DataService.ViewModel
{

    /// <summary>
    /// Card Renvoyer Apres une Recherche
    /// </summary>
    public class SearchCard
    {
        ///// <summary>
        ///// Card Renvoyer Apres une Recherche
        ///// </summary>
        //public SearchCard(Guid personGuid, Guid anneeScolaireGuid)
        //{
        //    using (var db = new SchoolContext())
        //    {
        //        PersonGuid = personGuid;
        //        FullName = db.Student.Find(personGuid)?.FullName;
        //        PhotoIdentity = db.Student.Find(personGuid)?.PhotoIdentity;

        //        var ins = db.Inscription.FirstOrDefault(i => i.AnneeScolaireGuid == anneeScolaireGuid && i.StudentGuid == personGuid);
        //        Description = db.Classe.Find(ins?.ClasseGuid)?.Name;
        //        Description = Description + " " + db.Filiere.Find(db.Classe.Find(ins?.ClasseGuid)?.FiliereGuid)?.Name;
        //    }
        //}


        /// <summary>
        /// Pour un ComboBox
        /// </summary>
        public SearchCard(Guid personGuid, bool isStaff = false)
        {
            if (!isStaff)
            {
                using (var db = new SchoolContext())
                {
                    PersonGuid = personGuid;
                    FullName = db.Students.Find(personGuid)?.Person.FullName;
                    PhotoIdentity = db.Students.Find(personGuid)? .Person.PhotoIdentity;

                    var anneeScolaireGuid = PedagogyManager.StaticGetStudentDefaultYearGuid(personGuid);
                    var ins = db.Enrollements.FirstOrDefault(i => i.SchoolYearGuid == anneeScolaireGuid && i.StudentGuid == personGuid);
                    Description = db.Classes.Find(ins?.ClasseGuid)?.Name;
                    Description = Description + " " + db.Filieres.Find(db.Classes.Find(ins?.ClasseGuid)?.FiliereGuid)?.Name;
                }
            }
            else
            {
                using (var db = new SchoolContext())
                {
                    PersonGuid = personGuid;
                    FullName = db.Staffs.Find(personGuid)?.Person.FullName;
                    PhotoIdentity = db.Staffs.Find(personGuid)?.Person.PhotoIdentity;

                    Description = db.Staffs.Find(personGuid)?.PositionPrincipale;                   
                }
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        public SearchCard()
        {
        }


        /// <summary>
        /// PersonGuid
        /// </summary>
        public Guid PersonGuid { get;}


        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        /// PhotoIdentity
        /// </summary>
        public byte[] PhotoIdentity { get; set; }


        /// <summary>
        /// Contient la Sigle du Filiere et Classe de l'Etudiant exp: 1 ere Année- GC
        /// </summary>
        public string Description { get; set; }
    }
}
