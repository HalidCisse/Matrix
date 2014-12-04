using DataService.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Serveur des Enums
    /// </summary>
    public class DataEnums
    {      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetNATIONALITIES()
        {
            var NATIONALITIES = new List<string>();

            using (var Db = new EF())
            {
                var StudentNT = (from S in Db.STUDENT.ToList() where S.NATIONALITY != null select S.NATIONALITY).ToList().Distinct().ToList();
                NATIONALITIES.AddRange(StudentNT);

                var StaffNT = (from S in Db.STAFF.ToList() where S.NATIONALITY != null select S.NATIONALITY).ToList().Distinct().ToList();
                NATIONALITIES.AddRange(StaffNT);
            }

            if (NATIONALITIES.Count != 0) return NATIONALITIES.Distinct();

            NATIONALITIES.Add("Maroc");
            NATIONALITIES.Add("Mali");
            NATIONALITIES.Add("Senegal");
            NATIONALITIES.Add("Algerie");
            NATIONALITIES.Add("Liberia");
            NATIONALITIES.Add("Guinee");
            NATIONALITIES.Add("Afrique Du Sud");
            NATIONALITIES.Add("Nigeria");
            NATIONALITIES.Add("Soudan");
            NATIONALITIES.Add("Gambie");
            NATIONALITIES.Add("Congo");
            NATIONALITIES.Add("Burkina Fasso");

            return NATIONALITIES;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetBIRTH_PLACE()
        {
            var BIRTH_PLACE = new List<string>();

            using (var Db = new EF())
            {
                var StudentBP = (from S in Db.STUDENT.ToList() where S.BIRTH_PLACE != null select S.BIRTH_PLACE).ToList().Distinct().ToList();
                BIRTH_PLACE.AddRange(StudentBP);

                var StaffBP = (from S in Db.STAFF.ToList() where S.BIRTH_PLACE != null select S.BIRTH_PLACE).ToList().Distinct().ToList();
                BIRTH_PLACE.AddRange(StaffBP);
            }

            if (BIRTH_PLACE.Count != 0) return BIRTH_PLACE.Distinct();

            BIRTH_PLACE.Add("Rabat");
            BIRTH_PLACE.Add("Casablanca");
            BIRTH_PLACE.Add("Bamako");
            BIRTH_PLACE.Add("Toumbouctou");
            BIRTH_PLACE.Add("Tayba");
            BIRTH_PLACE.Add("Dakar");

            return BIRTH_PLACE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetTITLES()
        {
            return new List<string> { "Mr", "Mme", "Mlle", "Dr" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetStudentSTATUTS()
        {
            return new List<string> { "Regulier", "Abandonner", "Irregulier", "Suspendue" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetStaffSTATUTS()
        {
            return new List<string> { "Regulier", "Licencier", "Irregulier", "Suspendue" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetStaffPOSITIONS()
        {
            using (var Db = new EF())
            {
                var Pos = (from S in Db.STAFF.ToList() where S.POSITION != null select S.POSITION).ToList().Distinct().ToList();

                if (Pos.Count == 0)
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
                return Pos;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetDEPARTEMENTS()
        {
            using (var Db = new EF())
            {
                var Deps = (from S in Db.STAFF.ToList() where S.DEPARTEMENT != null select S.DEPARTEMENT).ToList().Distinct().ToList();

                return Deps;
            }
            //Deps.Count == 0 ? new List<string> { "Departement de Mathematique", "Departement de Chimie", "Departement de Physique" } :
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetStaffQUALIFICATIONS()
        {
            using (var Db = new EF())
            {
                var Quals = (from S in Db.STAFF.ToList() where S.QUALIFICATION != null select S.QUALIFICATION).Distinct().ToList();

                return Quals.Count == 0 ? new List<string> { "Engenieur Etat En Informatique", "Doctorat En Mathematique", "Master En Anglais" } : Quals;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetFILIERE_NIVEAU_ENTREE()
        {
            using (var Db = new EF())
            {
                var Ns = (from S in Db.FILIERE.ToList() where S.NIVEAU_ENTREE != null select S.NIVEAU_ENTREE).ToList();

                Ns.AddRange(new List<string> { "Bac", "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat" });

                return Ns.Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetFILIERE_NIVEAU_SORTIE()
        {
            using (var Db = new EF())
            {
                var Ns = (from S in Db.FILIERE.ToList() where S.NIVEAU != null select S.NIVEAU).ToList();

                Ns.AddRange(new List<string> { "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat" });

                return Ns.Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereID"></param>
        /// <returns></returns>
        public IEnumerable GetFILIERE_LEVELS(Guid filiereID)
        {
            return GetFILIERE_NIVEAUX(filiereID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FiliereID"></param>
        /// <returns></returns>
        public IEnumerable GetFILIERE_NIVEAUX(Guid FiliereID)
        {
            using (var Db = new EF())
            {
                var MyFiliereN = Db.FILIERE.Find(FiliereID).N_ANNEE;
                var N = new List<int>();

                for (var i = 1; i < MyFiliereN + 1; i++) N.Add(i);

                return N;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetFILIERE_LEVELS()
        {
            return new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        public IEnumerable GetAllSalles()
        {
            using (var Db = new EF())
            {
                return (from S in Db.COURS.ToList() where S.SALLE != null select S.SALLE).Distinct().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetAllCoursTypes()
        {
            return new List<string> { "Cours", "Control", "Travaux Pratiques", "Travaux Dirigés", "Examen", "Test", "Revision", "Cours Theorique", "Cours Magistral" };
        }

    }
}
