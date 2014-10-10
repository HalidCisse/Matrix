using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using DataService.Context;
using DataService.Entities;
namespace DataService
{
    public class DbService //: Interface
    {


        #region Patternes DATA

        public IEnumerable GetNATIONALITIES ( )
        {
            var BIRTH_PLACE = new List<string> ();

            // Get Nationalities From Data Base

            BIRTH_PLACE.Add ("Mali");
            BIRTH_PLACE.Add ("Maroc");
            BIRTH_PLACE.Add ("Senegal");
            BIRTH_PLACE.Add ("Algerie");
            BIRTH_PLACE.Add ("Liberia");
            BIRTH_PLACE.Add ("Guinee");
            BIRTH_PLACE.Add ("Afrique Du Sud");
            BIRTH_PLACE.Add ("Nigeria");
            BIRTH_PLACE.Add ("Soudan");
            BIRTH_PLACE.Add ("Gambie");
            BIRTH_PLACE.Add ("Congo");
            BIRTH_PLACE.Add ("Burkina Fasso");


            return BIRTH_PLACE;
        }

        public IEnumerable GetBIRTH_PLACE ( )
        {
            var BIRTH_PLACE = new List<string> ();

            // GetBirth Place From Data Base

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
            return new List<string> { "Mr", "Mme", "Mlle", "Inspecifie" };
        }

        public IEnumerable GetSTATUTS ( )
        {
            return new List<string> { "Regulier", "Irregulier", "Abandonner", "Radier", "Suspendue" };
        }

        public IEnumerable GetStaffSTATUTS ( )
        {
            return new List<string> { "Regulier", "Licencier", "Suspendue"};
        }

        public IEnumerable GetStaffPOSITIONS ( )
        {
            return new List<string> {"Professeur", "Enseignant", "Instructeur", "Conferencier", "Chef Departement", "Directeur General", "Directeur Financier", "Directeur Pedagogique", "Secretaire" };
        }

        public IEnumerable GetDEPARTEMENTS ( )
        {
            return new List<string> { "Departement de Mathematique", "Departement de Chimie", "Departement de Physique" };
        }

        public IEnumerable GetStaffQUALIFICATIONS ( )
        {
            return new List<string> { "Engenieur Etat En Informatique", "Doctorat En Mathematique", "Master En Anglais" };
        }


        #endregion


        #region STUDENTS

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
            using(var Db = new EF ())
            {
                Db.STUDENT.Attach (MyStudent);
                Db.Entry (MyStudent).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public bool DeleteStudent ( string StudentID )
        {
            using(var Db = new EF ())
            {              
                Db.STUDENT.Remove (Db.STUDENT.Find (StudentID));               
                return Db.SaveChanges () > 0;
            }
        }

        public Student GetStudentByID (string STudentID )
        {
            using(var Db = new EF ())
            {              
                var MyStudent = Db.STUDENT.Find (STudentID);
                return MyStudent;
            }    
        }

        public static Student GetStudentByFullName (string FirstANDLastName )
        {
            using(var Db = new EF ())
            {
                var MyStudent = Db.STUDENT.SingleOrDefault (S => S.FIRSTNAME + S.LASTNAME  == FirstANDLastName);

                return MyStudent;
            }
        }

        public List<Student> GetAllStudents ( )
        {
            using(var Db = new EF ())
            {
                IList<Student> DaraNatadjis = Db.STUDENT.ToList ();

                return (List<Student>)DaraNatadjis;
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
            using(var Db = new EF ())
            {

                try
                {
                    return Db.STUDENT.Find (StudentID) != null;
                }
                catch(Exception e)
                {
                    
                    MessageBox.Show(e.InnerException.ToString()) ;
                }

            }

            return false;
        }

        #endregion


        #region PERSON


        #region PERSON C-R-U-D


        public static bool AddPerson ( Person MyPerson )
        {
            using(var Db = new EF ())
            {
                Db.PERSON.Add (MyPerson);
                return Db.SaveChanges () > 0;
            }
        }

        public static bool UpdatePerson ( Person MyPerson )
        {
            using(var Db = new EF ())
            {
                Db.PERSON.Attach (MyPerson);
                Db.Entry (MyPerson).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public static bool DeletePerson ( string PersonID )
        {
            using(var Db = new EF ())
            {
                Db.PERSON.Remove (Db.PERSON.Find (PersonID));
                return Db.SaveChanges () > 0;
            }
        }





        #endregion


        #region GET PERSON BY

        public static List<Person> GetAllPersons ( )
        {
            using(var Db = new EF ())
            {
                var Persons = Db.PERSON.ToList ();

                return Persons;
            }
        }

        public Person GetPersonByID ( string PersonID )
        {
            using(var Db = new EF ())
            {
                //var MyStudent = Db.Student.Find (PersonID);
                return Db.PERSON.Find (PersonID);
            }
        }

        public static Person GetPersonByFullName ( string FirstANDLastName )
        {
            using(var Db = new EF ())
            {
                var MyPerson = Db.PERSON.SingleOrDefault (P => P.FIRSTNAME + P.LASTNAME  == FirstANDLastName);

                return MyPerson;
            }
        }

        #endregion


        #endregion






        #region STAFF C-R-U-D


        private readonly EF D = new EF();

        
        public bool AddStaff ( Staff MyStaff)
        {                      
            D.STAFF.Add (MyStaff);
            return D.SaveChanges () > 0;          
        }

        public bool UpdateStaff ( Staff MyStaff)
        {
            using (var Db = new EF())
            {
                Db.STAFF.Attach(MyStaff);
                Db.Entry(MyStaff).State = EntityState.Modified;
                return Db.SaveChanges() > 0;
            }
        }

        public bool DeleteStaff ( string StaffID )
        {          
            D.STAFF.Remove (D.STAFF.Find (StaffID));                    
            return D.SaveChanges () > 0;            
        }

        public Staff GetStaffByID ( string StaffID )
        {
        //    var StaffIDs = 
            //from S in Db.STAFF
        //select S.STAFF_ID; 
            return D.STAFF.Find (StaffID);            
        }

        public Staff GetStaffByFullName ( string FirstANDLastName )
        {
            var MyStaff = D.STAFF.SingleOrDefault (S => S.FULL_NAME ==  FirstANDLastName);

            return MyStaff;           
        }

        public List<Staff> GetAllStaffs ( )
        {
            //D.SaveChangesAsync();
            return D.STAFF.ToList ();                             
        }

        public List<string> GetAllStaffsID ( )
        {
            List<string> IDs = null;

            IDs.AddRange(D.STAFF.ToList().Select(S => S.STAFF_ID));

            return IDs;
        }

        public string GetStaffFullName ( string StaffID )
        {                         
            return D.STAFF.Find (StaffID).FULL_NAME;            
        }



        #endregion

        
    }
}
