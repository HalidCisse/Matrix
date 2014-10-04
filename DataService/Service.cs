using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities;
namespace DataService
{
    public class Service: Interface
    {


        


        #region Student


        #region C R U D


        public bool AddStudent(Student MyStudent)
        {          
            using (var Db = new EF())
            {
                Db.Student.Add (MyStudent);               
                return Db.SaveChanges() > 0;                               
            }        
        }

        public bool UpdateStudent(Student MyStudent)
        {
            using(var Db = new EF ())
            {
                Db.Student.Attach (MyStudent);
                Db.Entry (MyStudent).State = EntityState.Modified;
                return Db.SaveChanges () > 0;
            }
        }

        public bool DeleteStudent ( string StudentID )
        {
            using(var Db = new EF ())
            {              
                Db.Student.Remove (Db.Student.Find (StudentID));               
                return Db.SaveChanges () > 0;
            }
        }

       



        #endregion



        #region Get Student BY




        public Student GetStudentByID (string STudentID )
        {
            using(var Db = new EF ())
            {
                //var MyStudent = Db.Student.SingleOrDefault (S => S.STUDENT_ID == STudentID);
                var MyStudent = Db.Student.Find (STudentID);
                return MyStudent;
            }    
        }

        public Student GetStudentByFullName (string FirstANDLastName )
        {
            using(var Db = new EF ())
            {
                var MyStudent = Db.Student.SingleOrDefault (S => S.FIRSTNAME + S.LASTNAME  == FirstANDLastName);

                return MyStudent;
            }
        }




        #endregion



        #region Get Student Info


        public List<Student> GetAllStudents ( )
        {
            using(var Db = new EF ())
            {
                IList<Student> DaraNatadjis = Db.Student.ToList ();

                return (List<Student>)DaraNatadjis;
            }
        }


        public string GetStudentName(string StudentID)
        {
            using (var Db = new EF())
            {
                var MyStudentName = Db.Student.Find(StudentID).FIRSTNAME + " " + Db.Student.Find(StudentID).LASTNAME;
                return MyStudentName;
            }           
        }

        

        #endregion



        #region Validations

        public bool StudentExist(string StudentID)
        {
            using(var Db = new EF ())
            {
                return Db.Student.Find(StudentID) != null;
            }  
        }



        #endregion




        #endregion









        #region Patternes DATA

        public List<string> GetNATIONALITIES ( )
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

        public List<string> GetBIRTH_PLACE()
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

        public List<string> GetTITLES()
        {         
            return new List<string> {"Mr", "Mme", "Mlle", "Inspecifie"};
        }

        public List<string> GetSTATUTS()
        {                  
             return new List<string> { "Regulier", "Irregulier", "Abandonner", "Radier" };
        }


        



        #endregion









    }
}
