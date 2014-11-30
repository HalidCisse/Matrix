using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService
{
    public class DbService 
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

        public bool DeleteClasse ( Guid ClasseID )
        {
            using(var Db = new EF ())
            {
                Db.CLASSE.Remove (Db.CLASSE.Find (ClasseID));
                return Db.SaveChanges () > 0;
            }
        }

        public Classe GetClasseByID ( Guid ClasseID )
        {
            using(var Db = new EF ())
            {                
                return Db.CLASSE.Find (ClasseID);
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
       
        public string GetClasseName ( Guid ClasseID )
        {
            using(var Db = new EF ())
            {               
                return Db.CLASSE.Find (ClasseID).NAME;
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

        public bool DeleteMatiere ( Guid MatiereID )
        {
            using(var Db = new EF ())
            {
                Db.MATIERE.Remove (Db.MATIERE.Find (MatiereID));
                return Db.SaveChanges () > 0;
            }
        }

        public Matiere GetMatiereByID ( Guid MatiereID )
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

        public string GetMatiereName ( Guid MatiereID )
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
                var MAT = Db.MATIERE.FirstOrDefault (M => M.NAME == MatiereName);
                return MAT == null ? null : MAT.MATIERE_ID.ToString();
            }            
        }

        public bool MatiereExist ( Guid MatiereID )
        {
            using(var Db = new EF ())
            {
                return Db.MATIERE.Find (MatiereID) != null;
            }
        }

        #endregion

       


        #region FILIERE C-R-U-D

        public bool AddFiliere ( Filiere MyFiliere )
        {
            MyFiliere.FILIERE_ID = Guid.NewGuid();
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

        public bool DeleteFiliere ( Guid FiliereID )
        {
            using(var Db = new EF ())
            {
                Db.FILIERE.Remove (Db.FILIERE.Find (FiliereID));
                return Db.SaveChanges () > 0;
            }
        }

        public Filiere GetFiliereByID ( Guid FiliereID )
        {
            using(var Db = new EF ())
            {
                var MyFiliere = Db.FILIERE.Find (FiliereID);
                return MyFiliere;
            }
        }

        public Filiere GetFiliereByName ( string FiliereName )
        {
            using(var Db = new EF ())
            {
                var MyFiliere = Db.FILIERE.First (S => S.NAME == FiliereName);

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

        public int GetFiliereClassCount ( Guid filiereId )
        {
            using(var Db = new EF ())
            {               
                return Db.CLASSE.Count (C => C.FILIERE_ID == filiereId);
            }
        }

        public string GetFiliereName ( Guid FiliereID )
        {
            using(var Db = new EF ())
            {
                var MyFiliereName = Db.FILIERE.Find (FiliereID).NAME;
                return MyFiliereName;
            }
        }

        public bool FiliereExist ( Guid FiliereID )
        {
            using(var Db = new EF ())
            {
                return Db.FILIERE.Find (FiliereID) != null;
            }
        }

        public IEnumerable GetFILIERE_NIVEAUX ( Guid FiliereID )
        {
            using(var Db = new EF ())
            {
                var MyFiliereN = Db.FILIERE.Find (FiliereID).N_ANNEE;
                var N = new List<int>();

                for (var i = 1; i < MyFiliereN + 1; i++) N.Add(i);

                return N;
            }
        }

       
        #endregion




        #region PATTERNES DATA

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
                var Quals = (from S in Db.STAFF.ToList () where S.QUALIFICATION != null select S.QUALIFICATION).Distinct ().ToList ();

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

        public IEnumerable GetFILIERE_LEVELS ( Guid filiereID )
        {
            return GetFILIERE_NIVEAUX (filiereID);
        }

        public IEnumerable GetFILIERE_LEVELS ()
        {
            return new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" } ;
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

        public IEnumerable GetAllSalles ( )
        {
            using(var Db = new EF ())
            {                
                return (from S in Db.COURS.ToList () where S.SALLE != null select S.SALLE).Distinct ().ToList ();
            }  
        }

        public IEnumerable GetAllCoursTypes ( )
        {
            return new List<string> { "Cours", "Control", "Travaux Pratiques", "Travaux Dirigés", "Examen", "Test", "Revision", "Cours Theorique", "Cours Magistral" };
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



        #region PEDAGOGY


        public bool IsMatiereInstructor ( string StaffID, Guid MatiereID )
        {
            using(var Db = new EF ())
            {                                                         
                return Db.MATIERES_INSTRUCTEURS.First(S => S.MATIERE_ID == MatiereID && S.STAFF_ID == StaffID) != null;
            }
        }
             
        public bool AddMatiereInstructor ( Guid MatiereID, string StaffID )
        {
            using(var Db = new EF ())
            {
                
                var RL = Db.MATIERES_INSTRUCTEURS.First (MI => MI.MATIERE_ID == MatiereID && MI.STAFF_ID == StaffID);
                if(RL != null) return true;

                var R = new Matiere_Instructeurs
                {
                    MATIERE_INSTRUCTEURS_ID = Guid.NewGuid(),
                    MATIERE_ID = MatiereID,
                    STAFF_ID = StaffID
                };

                Db.MATIERES_INSTRUCTEURS.Add (R);

                return Db.SaveChanges () > 0;

            }  
        }

        public bool DeleteMatiereInstructor ( Guid MatiereID, string StaffID )
        {
            using(var Db = new EF ())
            {
                var MI = Db.MATIERES_INSTRUCTEURS.First (M => M.MATIERE_ID == MatiereID && M.STAFF_ID == StaffID);
                if(MI == null) return true;
                
                Db.MATIERES_INSTRUCTEURS.Remove (MI);

                return Db.SaveChanges () > 0;
            } 
        }

        /// <summary>
        /// Renvoi les matieres Enregistrees pour cette classe
        /// </summary>
        /// <param name="ClasseID"></param>
        /// <returns></returns>
        public List<Matiere> GetClassMatieres ( Guid ClasseID )
        {
            using(var Db = new EF ())
            {               
                return Db.MATIERE.Where (M => M.CLASSE_ID == ClasseID).ToList ();
            }
        }

        public List<Staff> GetClassStaffs(Guid ClassID)
        {
            var Staffs = new List<Staff>();

            using(var Db = new EF ())
            {
                foreach(var ST in Db.COURS.Where (C => C.CLASSE_ID == ClassID))
                {
                    Staffs.Add (Db.STAFF.Find (ST.STAFF_ID));
                }
                return Staffs;
            }
        }

        public List<Student> GetClassStudents ( Guid ClassID )
        {
            var Students = new List<Student> ();

            using(var Db = new EF ())
            {
                foreach(var ST in Db.INSCRIPTION.Where (C => C.CLASSE_ID == ClassID))
                {
                    Students.Add (Db.STUDENT.Find (ST.STUDENT_ID));
                }
                return Students;
            }
        }





        #endregion




        #region COURS C-R-U-D


        public bool AddCours ( Cours MyCours )
        {
            MyCours.COURS_ID = Guid.NewGuid ();
            using(var Db = new EF ())
            {
                Db.COURS.Add (MyCours);
                return Db.SaveChanges () > 0;
            }
        }

        public bool UpdateCours ( Cours MyCours )
        {
            using(var Db = new EF ())
            {
                Db.COURS.Attach (MyCours);
                Db.Entry (MyCours).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public bool DeleteCours ( Guid CoursID )
        {
            using(var Db = new EF ())
            {
                Db.COURS.Remove (Db.COURS.Find (CoursID));
                return Db.SaveChanges () > 0;
            }
        }

        public Cours GetCoursByID ( Guid CoursID )
        {
            using(var Db = new EF ())
            {
                var MyCours = Db.COURS.Find (CoursID);
                return MyCours;
            }
        }


        #endregion

        
    }
}


//Task.Factory.StartNew( () => Parallel.ForEach<Item>(items, item => DoSomething(item)));

