using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService
{
    public class DbService //: Interface
    {










        #region CLASSE C-R-U-D

        public bool AddClasse ( Classe MyClasse )
        {
            using(var Db = new EF ())
            {
                Db.CLASSE.Add (MyClasse);
                return Db.SaveChanges () > 0;
            }
        }

        public bool UpdateClasse ( Classe MyClasse )
        {
            using(var Db = new EF ())
            {
                Db.CLASSE.Attach (MyClasse);
                Db.Entry (MyClasse).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public bool DeleteClasse ( string ClasseID )
        {
            using(var Db = new EF ())
            {
                Db.CLASSE.Remove (Db.CLASSE.Find (ClasseID));
                return Db.SaveChanges () > 0;
            }
        }

        public Classe GetClasseByID ( string ClasseID )
        {
            using(var Db = new EF ())
            {
                var MyClasse = Db.CLASSE.Find (ClasseID);
                return MyClasse;
            }
        }

        public static Classe GetClasseByName ( string ClasseName )
        {
            using(var Db = new EF ())
            {
                var MyClasse = Db.CLASSE.SingleOrDefault (S => S.NAME == ClasseName);

                return MyClasse;
            }
        }

        public List<Classe> GetAllClasse ( )
        {
            using(var Db = new EF ())
            {
                IList<Classe> MesClasses = Db.CLASSE.ToList ();

                return (List<Classe>)MesClasses;
            }
        }

        public string GetClasseName ( string ClasseID )
        {
            using(var Db = new EF ())
            {
                var MyClasseName = Db.CLASSE.Find (ClasseID).NAME;
                return MyClasseName;
            }
        }

        public bool ClasseExist ( string ClasseID )
        {
            using(var Db = new EF ())
            {
                return Db.CLASSE.Find (ClasseID) != null;
            }
        }

        #endregion




        #region MATIERE C-R-U-D

        public bool AddMatiere ( Matiere MyMatiere )
        {
            using(var Db = new EF ())
            {
                Db.MATIERE.Add (MyMatiere);
                return Db.SaveChanges () > 0;
            }
        }

        public bool UpdateMatiere ( Matiere MyMatiere )
        {
            using(var Db = new EF ())
            {
                Db.MATIERE.Attach (MyMatiere);
                Db.Entry (MyMatiere).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public bool DeleteMatiere ( string MatiereID )
        {
            using(var Db = new EF ())
            {
                Db.MATIERE.Remove (Db.MATIERE.Find (MatiereID));
                return Db.SaveChanges () > 0;
            }
        }

        public Matiere GetMatiereByID ( string MatiereID )
        {
            using(var Db = new EF ())
            {
                var MyMatiere = Db.MATIERE.Find (MatiereID);
                return MyMatiere;
            }
        }

        public static Matiere GetMatiereByName ( string MatiereName )
        {
            using(var Db = new EF ())
            {
                var MyMatiere = Db.MATIERE.SingleOrDefault (S => S.NAME == MatiereName);

                return MyMatiere;
            }
        }

        public List<Matiere> GetAllMatieres ( )
        {
            using(var Db = new EF ())
            {
                IList<Matiere> MesMatieres = Db.MATIERE.ToList ();

                return (List<Matiere>)MesMatieres;
            }
        }

        public string GetMatiereName ( string MatiereID )
        {
            using(var Db = new EF ())
            {
                var MyMatiereName = Db.MATIERE.Find (MatiereID).NAME;
                return MyMatiereName;
            }
        }

        public bool MatiereExist ( string MatiereID )
        {
            using(var Db = new EF ())
            {
                return Db.MATIERE.Find (MatiereID) != null;
            }
        }

        #endregion





        #region Filiere C-R-U-D

        public bool AddFiliere ( Filiere MyFiliere )
        {
            MyFiliere.FILIERE_ID = Guid.NewGuid().ToString();
            using(var Db = new EF ())
            {
                Db.FILIERE.Add (MyFiliere);
                return Db.SaveChanges () > 0;
            }
        }

        public bool UpdateFiliere ( Filiere MyFiliere )
        {
            using(var Db = new EF ())
            {
                Db.FILIERE.Attach (MyFiliere);
                Db.Entry (MyFiliere).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public bool DeleteFiliere ( string FiliereID )
        {
            using(var Db = new EF ())
            {
                Db.FILIERE.Remove (Db.FILIERE.Find (FiliereID));
                return Db.SaveChanges () > 0;
            }
        }

        public Filiere GetFiliereByID ( string FiliereID )
        {
            using(var Db = new EF ())
            {
                var MyFiliere = Db.FILIERE.Find (FiliereID);
                return MyFiliere;
            }
        }

        public static Filiere GetFiliereByName ( string FiliereName )
        {
            using(var Db = new EF ())
            {
                var MyFiliere = Db.FILIERE.SingleOrDefault (S => S.NAME == FiliereName);

                return MyFiliere;
            }
        }

        public List<Filiere> GetAllFilieres ( )
        {
            using(var Db = new EF ())
            {

                IList<Filiere> MesFilieres = Db.FILIERE.ToList ();

                return (List<Filiere>)MesFilieres;
            }
        }

        public string GetFiliereName ( string FiliereID )
        {
            using(var Db = new EF ())
            {
                var MyFiliereName = Db.FILIERE.Find (FiliereID).NAME;
                return MyFiliereName;
            }
        }

        public bool FiliereExist ( string FiliereID )
        {
            using(var Db = new EF ())
            {
                return Db.FILIERE.Find (FiliereID) != null;
            }
        }

        #endregion




        #region Patternes DATA

        public IEnumerable GetNATIONALITIES ( )
        {
            var NATIONALITIES = new List<string> ();

            using(var Db = new EF ())
            {
                var StudentNT = (from S in Db.STUDENT.ToList () where S.NATIONALITY != null select S.NATIONALITY).ToList ().Distinct ().ToList ();
                NATIONALITIES.AddRange (StudentNT);

                var StaffNT = (from S in Db.STAFF.ToList () where S.NATIONALITY != null select S.NATIONALITY).ToList ().Distinct ().ToList ();
                NATIONALITIES.AddRange (StaffNT);
            }

            if (NATIONALITIES.Count != 0) return NATIONALITIES.Distinct();

            NATIONALITIES.Add ("Maroc");
            NATIONALITIES.Add ("Mali");
            NATIONALITIES.Add ("Senegal");
            NATIONALITIES.Add ("Algerie");
            NATIONALITIES.Add ("Liberia");
            NATIONALITIES.Add ("Guinee");
            NATIONALITIES.Add ("Afrique Du Sud");
            NATIONALITIES.Add ("Nigeria");
            NATIONALITIES.Add ("Soudan");
            NATIONALITIES.Add ("Gambie");
            NATIONALITIES.Add ("Congo");
            NATIONALITIES.Add ("Burkina Fasso");

            return NATIONALITIES;
        }

        public IEnumerable GetBIRTH_PLACE ( )
        {
            var BIRTH_PLACE = new List<string> ();

            using(var Db = new EF ())
            {
                var StudentBP = (from S in Db.STUDENT.ToList () where S.BIRTH_PLACE != null select S.BIRTH_PLACE).ToList ().Distinct ().ToList ();
                BIRTH_PLACE.AddRange (StudentBP);

                var StaffBP = (from S in Db.STAFF.ToList () where S.BIRTH_PLACE != null select S.BIRTH_PLACE).ToList ().Distinct ().ToList ();
                BIRTH_PLACE.AddRange (StaffBP);
            }

            if(BIRTH_PLACE.Count != 0) return BIRTH_PLACE.Distinct ();

            BIRTH_PLACE.Add ("Rabat");
            BIRTH_PLACE.Add ("Casablanca");
            BIRTH_PLACE.Add ("Bamako");
            BIRTH_PLACE.Add ("Toumbouctou");
            BIRTH_PLACE.Add ("Tayba");
            BIRTH_PLACE.Add ("Dakar");
            
            return BIRTH_PLACE;
        }

        public IEnumerable GetTITLES ( )
        {
             return new List<string> { "Mr", "Mme", "Mlle", "Dr" };
        }

        public IEnumerable GetStudentSTATUTS ( )
        {           
            return new List<string> { "Regulier", "Abandonner", "Irregulier", "Suspendue" };
        }

        public IEnumerable GetStaffSTATUTS ( )
        {            
            return new List<string> { "Regulier", "Licencier", "Irregulier", "Suspendue"};
        }

        public IEnumerable GetStaffPOSITIONS ( )
        {
            using(var Db = new EF ())
            {
                var Pos = (from S in Db.STAFF.ToList () where S.POSITION != null select S.POSITION).ToList ().Distinct ().ToList ();

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

        public List<string> GetDEPARTEMENTS ( )
        {
            using(var Db = new EF ())
            {
                var Deps = (from S in Db.STAFF.ToList() where S.DEPARTEMENT != null select S.DEPARTEMENT).ToList().Distinct().ToList();

                return Deps;
            }
            //Deps.Count == 0 ? new List<string> { "Departement de Mathematique", "Departement de Chimie", "Departement de Physique" } :
        }

        public IEnumerable GetStaffQUALIFICATIONS ( )
        {
            using(var Db = new EF ())
            {
                var Quals = (from S in Db.STAFF.ToList () where S.QUALIFICATION != null select S.QUALIFICATION).ToList ().Distinct ().ToList ();

                return Quals.Count == 0 ? new List<string> { "Engenieur Etat En Informatique", "Doctorat En Mathematique", "Master En Anglais" } : Quals;
            }           
        }

        public IEnumerable GetFILIERE_NIVEAU_ENTREE ( )
        {
            using(var Db = new EF ())
            {
                var Ns = (from S in Db.FILIERE.ToList () where S.NIVEAU_ENTREE != null select S.NIVEAU_ENTREE).ToList ();

                Ns.AddRange(new List<string> {"Bac", "Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat"});

                return Ns.Distinct().ToList();
            }  
        }

        public IEnumerable GetFILIERE_NIVEAU_SORTIE ( )
        {
            using(var Db = new EF ())
            {
                var Ns = (from S in Db.FILIERE.ToList () where S.NIVEAU != null select S.NIVEAU).ToList ();

                Ns.AddRange (new List<string> {"Bac+1", "Bac+2", "Licence", "Bac+4", "Master", "Engenieur", "Doctorat" });

                return Ns.Distinct ().ToList ();
            }  
        }

        public IEnumerable GetFILIERE_ANNEE ( )
        {            
            return new List<string>{ "1", "2", "3", "4", "5", "6", "7", "8" };
        }

        #endregion



        #region STUDENTS  C-R-U-D 

        public bool AddStudent(Student MyStudent)
        {
            using (var Db = new EF())
            {
                Db.STUDENT.Add (MyStudent);               
                return Db.SaveChanges() > 0;  
            }
                                             
                    
        }

        public bool UpdateStudent(Student MyStudent)
        {
            using (var Db = new EF())
            {
                Db.STUDENT.Attach(MyStudent);
                Db.Entry(MyStudent).State = EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        public bool DeleteStudent ( string StudentID )
        {
            using (var Db = new EF())
            {
                Db.STUDENT.Remove(Db.STUDENT.Find(StudentID));
                return Db.SaveChanges() > 0;
            }
        }

        public Student GetStudentByID (string STudentID )
        {
            using (var Db = new EF())
            {
                var MyStudent = Db.STUDENT.Find(STudentID);
                return MyStudent;
            }
        }

        public static Student GetStudentByFullName (string FirstANDLastName )
        {
            using (var Db = new EF())
            {
                var MyStudent = Db.STUDENT.SingleOrDefault(S => S.FIRSTNAME + S.LASTNAME == FirstANDLastName);

                return MyStudent;
            }
        }

        public List<Student> GetAllStudents ( )
        {
            using (var Db = new EF())
            {

                IList<Student> DaraNatadjis = Db.STUDENT.ToList();

                return (List<Student>) DaraNatadjis;
            }
        }

        public string GetStudentName(string StudentID)
        {
            using (var Db = new EF())
            {
                var MyStudentName = Db.STUDENT.Find(StudentID).FIRSTNAME + " " + Db.STUDENT.Find(StudentID).LASTNAME;
                return MyStudentName;
            }
        }

        public bool StudentExist(string StudentID)
        {
            using (var Db = new EF())
            {

                return Db.STUDENT.Find(StudentID) != null;

            }
        }

        #endregion



        #region STAFF C-R-U-D

        public bool AddStaff(Staff MyStaff)
        {
            using (var Db = new EF())
            {
                Db.STAFF.Add(MyStaff);
                return Db.SaveChanges() > 0;
            }
        }

        public bool UpdateStaff ( Staff MStaff )
        {
            using (var Db = new EF())
            {               
                Db.STAFF.Attach(MStaff);
                Db.Entry(MStaff).State = EntityState.Modified;

                return Db.SaveChanges() > 0;
            }
        }

        public bool DeleteStaff(string StaffID)
        {
            using (var Db = new EF())
            {
                Db.STAFF.Remove(Db.STAFF.Find(StaffID));
                return Db.SaveChanges() > 0;
            }
        }

        public Staff GetStaffByID(string StaffID)
        {
            using (var Db = new EF())
            {
                //    var StaffIDs = 
                //from S in Db.STAFF
                //select S.STAFF_ID; 
                return Db.STAFF.Find(StaffID);
            }
        }

        public Staff GetStaffByFullName(string FirstANDLastName)
        {
            using (var Db = new EF())
            {
                var MyStaff = Db.STAFF.SingleOrDefault(S => S.FULL_NAME == FirstANDLastName);

                return MyStaff;
            }
        }

        public List<Staff> GetAllStaffs()
        {
            using (var Db = new EF())
            {            
                return Db.STAFF.ToList();
            }
        }

        public List<string> GetAllStaffsID()
        {
            var IDs = new List<string>();
            using (var Db = new EF())
            {
                IDs.AddRange(Db.STAFF.ToList().Select(S => S.STAFF_ID));
                return IDs;
            }
        }

        public List<Staff> GetDepStaffs(string DepName = null)
        {
            using(var Db = new EF ())
            {
                return DepName == null ? Db.STAFF.ToList().Where(S => string.IsNullOrEmpty(S.DEPARTEMENT)).ToList() : Db.STAFF.ToList ().Where (S => S.DEPARTEMENT == DepName).ToList ();
            }
        }
      
        public string GetStaffFullName(string StaffID)
        {
            using (var Db = new EF())
            {
                return Db.STAFF.Find(StaffID).FULL_NAME;
            }
        }

        public bool StaffExist(string StaffID)
        {
            using (var Db = new EF())
            {
                return Db.STAFF.Find(StaffID) != null;
            }
        }

        #endregion

        
    }
}
