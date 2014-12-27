using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataService.Context;

namespace DataService.DataManager
{
    /// <summary>
    /// Serveur des Enums
    /// </summary>
    [Obsolete]
    public class DataEnums
    {
        //Todo Considere Abandoning DataEnums
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetNationalities()
        {
            var nationalities = new List<string>();

            using (var db = new Ef())
            {
                var studentNt = (from s in db.Student.ToList() where s.Nationality != null select s.Nationality).ToList().Distinct().ToList();
                nationalities.AddRange(studentNt);

                var staffNt = (from s in db.Staff.ToList() where s.Nationality != null select s.Nationality).ToList().Distinct().ToList();
                nationalities.AddRange(staffNt);
            }

            if (nationalities.Count != 0) return nationalities.Distinct();

            nationalities.Add("Maroc");
            nationalities.Add("Mali");
            nationalities.Add("Senegal");
            nationalities.Add("Algerie");
            nationalities.Add("Liberia");
            nationalities.Add("Guinee");
            nationalities.Add("Afrique Du Sud");
            nationalities.Add("Nigeria");
            nationalities.Add("Soudan");
            nationalities.Add("Gambie");
            nationalities.Add("Congo");
            nationalities.Add("Burkina Fasso");

            return nationalities;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetBIRTH_PLACE()
        {
            var birthPlace = new List<string>();

            using (var db = new Ef())
            {
                var studentBp = (from s in db.Student.ToList() where s.BirthPlace != null select s.BirthPlace).ToList().Distinct().ToList();
                birthPlace.AddRange(studentBp);

                var staffBp = (from s in db.Staff.ToList() where s.BirthPlace != null select s.BirthPlace).ToList().Distinct().ToList();
                birthPlace.AddRange(staffBp);
            }

            if (birthPlace.Count != 0) return birthPlace.Distinct();

            birthPlace.Add("Rabat");
            birthPlace.Add("Casablanca");
            birthPlace.Add("Bamako");
            birthPlace.Add("Toumbouctou");
            birthPlace.Add("Tayba");
            birthPlace.Add("Dakar");

            return birthPlace;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetTitles()
        {
            return new List<string> { "Mr", "Mme", "Mlle", "Dr" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetStudentStatuts()
        {
            return new List<string> { "Regulier", "Abandonner", "Irregulier", "Suspendue" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetStaffStatuts()
        {
            return new List<string> { "Regulier", "Licencier", "Irregulier", "Suspendue" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetStaffPositions()
        {
            using (var db = new Ef())
            {
                var pos = (from s in db.Staff.ToList() where s.Position != null select s.Position).ToList().Distinct().ToList();

                if (pos.Count == 0)
                {
                    return new List<string> {
                        "Professeur",
                        "Enseignant",
                        "Instructeur",
                        "Conferencier",
                        "Chef Departement",
                        "Directeur General",
                        "Directeur Financier",
                        "Directeur Pedagogique",
                        "Secretaire"
                    };
                }
                return pos;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetDepartements()
        {
            using (var db = new Ef())
            {
                var deps = (from s in db.Staff.ToList() where s.Departement != null select s.Departement).Distinct().ToList();

                return deps;
            }
            //Deps.Count == 0 ? new List<string> { "Departement de Mathematique", "Departement de Chimie", "Departement de Physique" } :
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetStaffQualifications()
        {
            using (var db = new Ef())
            {
                var quals = (from s in db.Staff.ToList() where s.Qualification != null select s.Qualification).Distinct().ToList();

                return quals.Count == 0 ? new List<string> { "Engenieur Etat En Informatique", "Doctorat En Mathematique", "Master En Anglais" } : quals;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetFILIERE_NIVEAU_ENTREE()
        {
            using (var db = new Ef())
            {
                var ns = (from s in db.Filiere.ToList() where s.NiveauEntree != null select s.NiveauEntree).ToList();

                ns.AddRange(new List<string> { "Bac", "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat" });

                return ns.Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetFILIERE_NIVEAU_SORTIE()
        {
            using (var db = new Ef())
            {
                var ns = (from s in db.Filiere.ToList() where s.Niveau != null select s.Niveau).ToList();

                ns.AddRange(new List<string> { "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat" });

                return ns.Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetFILIERE_LEVELS(Guid filiereId)
        {
            return GetFILIERE_NIVEAUX(filiereId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereId"></param>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetFILIERE_NIVEAUX(Guid filiereId)
        {
            using (var db = new Ef())
            {
                var myFiliereN = db.Filiere.Find(filiereId).NAnnee;
                var n = new List<int>();

                for (var i = 1; i < myFiliereN + 1; i++) n.Add(i);

                return n;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetFILIERE_LEVELS()
        {
            return new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         [Obsolete]
        public IEnumerable GetMATIERE_HEURES_PAR_SEMAINE()
        {
            return new List<string>
            {
                "1 Heure", "1 Heure 30 min", "2 Heures", "2 Heures 30 min", "3 Heures", "3 Heures 30 min", "4 Heures", "4 Heures 30 min",
                "5 Heure", "5 Heure 30 min", "6 Heures", "6 Heures 30 min", "7 Heures", "7 Heures 30 min", "8 Heures", "8 Heures 30 min",
                "9 Heure", "9 Heure 30 min", "10 Heures", "10 Heures 30 min", "11 Heures", "11 Heures 30 min", "12 Heures", "12 Heures 30 min"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetAllSalles()
        {
            using (var db = new Ef())
            {
                return (from s in db.Cours.ToList() where s.Salle != null select s.Salle).Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public IEnumerable GetAllCoursTypes()
        {
            return new List<string> { "Cours", "Control", "Travaux Pratiques", "Travaux Dirigés", "Examen", "Test", "Revision", "Cours Theorique", "Cours Magistral" };
        }




    }
}
