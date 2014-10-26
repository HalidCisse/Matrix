using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Model;
using DataService.ViewModel;

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
                return Db.CLASSE.ToList ();
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
                return Db.MATIERE.ToList ();
            }
        }

        public IEnumerable GetAllMatieresNames ( )
        {
            using(var Db = new EF ())
            {               
                return Db.MATIERE.Select (M => M.NAME).ToList ();
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

        public string GetMatiereIDFromName ( string MatiereName )
        {
            using(var Db = new EF ())
            {
                var Default = Db.MATIERE.SingleOrDefault (M => M.NAME == MatiereName);
                return Default == null ? null : Default.MATIERE_ID;
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
                return Db.FILIERE.ToList ();
            }
        }
      
        public IEnumerable GetAllFilieresNames ( )
        {
            var Names = new List<string> ();
            using(var Db = new EF ())
            {
                Names.AddRange (Db.FILIERE.Select (S => S.NAME));
                return Names;
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

        public IEnumerable GetFILIERE_NIVEAUX ( string FiliereID )
        {
            using(var Db = new EF ())
            {
                var MyFiliereN = Db.FILIERE.Find (FiliereID).N_ANNEE;
                var N = new List<int>();

                for (var i = 1; i < MyFiliereN + 1; i++) N.Add(i);

                return N;
            }
        }

        public List<Matiere> GetMatieresOfFiliereYear ( string FiliereID, int FiliereYear )
        {
            using(var Db = new EF ())
            {                
                var MatieresIDs = Db.FILIERE_MATIERE.Where (F => F.FILIERE_ID == FiliereID && F.FILIERE_LEVEL == FiliereYear).Select (F => F.MATIERE_ID).ToList ();

                var Matieres = MatieresIDs.Select(M => Db.MATIERE.Find(M)).ToList();

                return Matieres;
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

        public IEnumerable GetFILIERE_LEVELS ( string filiereID = "" )
        {
            if (filiereID == "")
                return new List<string> {"1", "2", "3", "4", "5", "6", "7", "8"};
            else
            {
                



            }


        }

        public IEnumerable GetMATIERE_HEURES_PAR_SEMAINE ( )
        {
            return new List<string>
            {
                "1 Heure", "1 Heure 30 min", "2 Heures", "2 Heures 30 min", "3 Heures", "3 Heures 30 min", "4 Heures", "4 Heures 30 min",
                "5 Heure", "5 Heure 30 min", "6 Heures", "6 Heures 30 min", "7 Heures", "7 Heures 30 min", "8 Heures", "8 Heures 30 min",
                "9 Heure", "9 Heure 30 min", "10 Heures", "10 Heures 30 min", "11 Heures", "11 Heures 30 min", "12 Heures", "12 Heures 30 min"            
            };
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

        public IEnumerable GetAllStaffNames ( )
        {
            var Names = new List<string> ();
            using(var Db = new EF ())
            {
                Names.AddRange (Db.STAFF.ToList ().Select (S => S.FULL_NAME));
                return Names;
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



        #region Pedagogy


        public bool IsMatiereInstructor ( string StaffID, string MatiereID )
        {
            using(var Db = new EF ())
            {                                                         
                return Db.MATIERES_INSTRUCTEURS.SingleOrDefault(S => S.MATIERE_ID == MatiereID && S.STAFF_ID == StaffID) != null;
            }
        }

        public int GetFiliereMatiereNiveau ( string FiliereID, string MatiereID )
        {
            using(var Db = new EF ())
            {
                var Matiere = Db.FILIERE_MATIERE.SingleOrDefault(S => S.MATIERE_ID == MatiereID && S.FILIERE_ID == FiliereID);
                if (Matiere !=null) return Matiere.FILIERE_LEVEL;
            }
            return 1;
        }

        public string GetFiliereMatiereHeuresParSemaine ( string FiliereID, string MatiereID )
        {
            using(var Db = new EF ())
            {
                var Matiere = Db.FILIERE_MATIERE.SingleOrDefault (S => S.MATIERE_ID == MatiereID && S.FILIERE_ID == FiliereID);
                if(Matiere !=null) return Matiere.HEURE_PAR_SEMAINE;
            }
            return null;
        }
        
        public bool SaveFiliereMatiere(string FiliereID, string MatiereID, int Level , string HeuresParSemaine)
        {            
            using(var Db = new EF ())
            {
                if (Db.FILIERE_MATIERE.Find(FiliereID + MatiereID + Level) != null)
                {
                    Db.FILIERE_MATIERE.Find(FiliereID + MatiereID + Level).HEURE_PAR_SEMAINE = HeuresParSemaine;
                }
                else
                {
                    var FM = new Filiere_Matieres
                    {
                        FILIERE_MATIERE_ID = FiliereID + MatiereID + Level,
                        FILIERE_ID = FiliereID,
                        MATIERE_ID = MatiereID,
                        FILIERE_LEVEL = Level,
                        HEURE_PAR_SEMAINE = HeuresParSemaine
                    };
                    Db.FILIERE_MATIERE.Add(FM);
                }
                return Db.SaveChanges() > 0;
            }
        }

        public bool DeleteFiliereMatiere ( string FiliereID, string MatiereID, int Level )
        {
            using(var Db = new EF ())
            {
                var MT = Db.FILIERE_MATIERE.Find (FiliereID +MatiereID +Level);
                if (MT == null) return true;
                Db.FILIERE_MATIERE.Remove (MT);

                return Db.SaveChanges () > 0;
            }
        }
        
        public bool AddMatiereInstructor ( string MatiereID, string StaffID )
        {
            using(var Db = new EF ())
            {
                var RL = Db.MATIERES_INSTRUCTEURS.Find (MatiereID + StaffID);
                if(RL != null) return true;

                var R = new Matiere_Instructeurs
                {
                    MATIERE_INSTRUCTEURS_ID = MatiereID + StaffID,                    
                    MATIERE_ID = MatiereID,
                    STAFF_ID = StaffID
                };

                Db.MATIERES_INSTRUCTEURS.Add (R);

                return Db.SaveChanges () > 0;
            }  
        }

        public bool DeleteMatiereInstructor ( string MatiereID, string StaffID )
        {
            using(var Db = new EF ())
            {
                var MI = Db.MATIERES_INSTRUCTEURS.Find (MatiereID + StaffID);
                if(MI == null) return true;
                
                Db.MATIERES_INSTRUCTEURS.Remove (MI);

                return Db.SaveChanges () > 0;
            } 
        }

        public int GetNofMatiereInstructor ( string MatiereID )
        {
            using(var Db = new EF ())
            {              
                return Db.MATIERES_INSTRUCTEURS.Count (M => M.MATIERE_ID == MatiereID);
            }
        }
        

        #endregion



        #region VIEW MODELS

        public List<FiliereCard> GetAllFilieresCards()
        {                       
            using(var Db = new EF ())
            {
                var FL = new List<FiliereCard> ();

                Parallel.ForEach(Db.FILIERE, F =>
                {                  
                    FL.Add(new FiliereCard(F));
                });
                return FL;
            }           
        }

        public List<FiliereLevelCard> GetFiliereMatieresCards( string FiliereID)
        {           
            var MatiereCardList = new List<FiliereLevelCard> ();              

            foreach(int Level in GetFILIERE_NIVEAUX (FiliereID))
            {                                   
                MatiereCardList.Add (new FiliereLevelCard (FiliereID, Level ));
            }
            return MatiereCardList;
        }

        public List<DepStaffCard> GetDepStaffsCard ( )
        {
            var DepStaffCardList = new List<DepStaffCard> {new DepStaffCard("")};

            Parallel.ForEach(GetDEPARTEMENTS(), Dep =>
            {
                DepStaffCardList.Add(new DepStaffCard(Dep));
            });

            return DepStaffCardList;
        }



        public List<FiliereClassCard> GetFiliereClassCards ( )
        {
            var ClassCardList = new List<FiliereClassCard> ();

            foreach(var Fil in GetAllFilieres())
            {
                ClassCardList.Add (new FiliereClassCard (Fil));
            }
            return ClassCardList;
        }


        #endregion



        //Task.Factory.StartNew( () => Parallel.ForEach<Item>(items, item => DoSomething(item)));
       
    }
}
