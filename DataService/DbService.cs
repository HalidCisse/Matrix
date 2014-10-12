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
            using (var Db = new EF())
            {
                List<string> IDs = null;

                IDs.AddRange(Db.STAFF.ToList().Select(S => S.STAFF_ID));

                return IDs;
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
