using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DataService.Context;
using DataService.Entities;

namespace DataService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EF>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            
        }

        protected override void Seed(EF EF)
        {           
            //AddPersonSeed (EF);
            AddStudentSeed (EF);
            AddStaffSeed (EF);
        }




        #region SEEDS

        private static void AddStudentSeed ( EF EF )
        {
            ImageBuff = BitmapArrayFromFile ("defaultStudent.png");

            var MyStudents = new List<Student>{
                new Student {STUDENT_ID = "DI100", FIRSTNAME = "Halid", LASTNAME = "Cisse", PHONE_NUMBER = "00122445545", PHOTO_IDENTITY = ImageBuff},
                new Student {STUDENT_ID = "DI102", FIRSTNAME = "Mat", LASTNAME = "Pearson", PHONE_NUMBER = "0012244545545", PHOTO_IDENTITY = ImageBuff},
                new Student {STUDENT_ID = "DI103", FIRSTNAME = "Dave" , LASTNAME = "Wood", PHONE_NUMBER = "001544545578", PHOTO_IDENTITY = ImageBuff},
                new Student {STUDENT_ID = "DI104", FIRSTNAME = "Adam", LASTNAME = "Nolan", PHONE_NUMBER = "0012445454545", PHOTO_IDENTITY = ImageBuff}
            };

            MyStudents.ForEach (Student => EF.STUDENT.AddOrUpdate (Student));
        }

        private static void AddPersonSeed ( EF EF )
        {
            ImageBuff = BitmapArrayFromFile ("defaultStudent.png");

            var MyPersons = new List<Person> {
                new Person {PERSON_ID = "STAFF_IDDI100", FIRSTNAME = "Halid", LASTNAME = "Cisse", PHONE_NUMBER = "00122445545", PHOTO_IDENTITY = ImageBuff},
                new Person {PERSON_ID = "STAFF_IDDI102", FIRSTNAME = "Mat", LASTNAME = "Pearson", PHONE_NUMBER = "0012244545545", PHOTO_IDENTITY = ImageBuff},
                new Person {PERSON_ID = "STAFF_IDDI103", FIRSTNAME = "Dave" , LASTNAME = "Wood", PHONE_NUMBER = "001544545578"},
                new Person {PERSON_ID = "STAFF_IDDI104", FIRSTNAME = "Adam", LASTNAME = "Nolan", PHONE_NUMBER = "0012445454545"}
            };

            MyPersons.ForEach (Person => EF.PERSON.AddOrUpdate (Person));
        }

        private static void AddStaffSeed ( EF EF )
        {
            ImageBuff = BitmapArrayFromFile ("defaultStaff.png");

            var MyStaffs = new List<Staff> {
                new Staff {STAFF_ID = "DI100", DEPARTEMENT = "Info", POSITION = "Chef de Departement Info"},
                new Staff {STAFF_ID = "DI102", DEPARTEMENT = "Math", POSITION = "Chef de Departement"},
                new Staff {STAFF_ID = "DI103", DEPARTEMENT = "Bio", POSITION = "Chef de Departement"},
                new Staff {STAFF_ID = "DI104", DEPARTEMENT = "Chimie", POSITION = "Chef de Departement"}
            };

            MyStaffs.ForEach (Staff => EF.STAFF.AddOrUpdate (Staff));
        }


        #endregion


        #region Helpers

        private static byte[] ImageBuff;      
        private static byte[] BitmapArrayFromFile ( string FilePath )
        {
            //FilePath = GetRes (FilePath);
            FilePath = @"C:\Users\Halid\Documents\Visual Studio 2013\Projects\Matrix\Matrix\Portrait\" + FilePath;
            if(!File.Exists (FilePath)) return null;

            var fs = new FileStream (FilePath, FileMode.Open, FileAccess.Read);
            var imgByteArr = new byte[fs.Length];
            fs.Read (imgByteArr, 0, Convert.ToInt32 (fs.Length));
            fs.Close ();
            return imgByteArr;
        }
        private static string GetRes ( string pathInApplication, Assembly assembly = null )
        {
            if(assembly == null)
            {
                assembly = Assembly.GetCallingAssembly ();
            }

            if(pathInApplication[0] == '/')
            {
                pathInApplication = pathInApplication.Substring (1);
            }
            return (new Uri (@"pack://application:,,,/" + assembly.GetName ().Name + ";component/" + pathInApplication, UriKind.Absolute).ToString ());
        }

        #endregion

    }
}
