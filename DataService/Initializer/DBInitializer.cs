using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Reflection;
using DataService.Context;
using DataService.Entities;

namespace DataService.Initializer
{
    class DBInitializer : CreateDatabaseIfNotExists<EF> //DropCreateDatabaseIfModelChanges<EF> //CreateDatabaseIfNotExists<EF> //DropCreateDatabaseIfModelChanges<EF>
    {

        protected override void Seed ( EF EF )
        {
           
            //AddPersonSeed(EF);
            //AddStudentSeed (EF);
            //AddStaffSeed (EF);

        }



        //#region SEEDS

        //private static void AddStudentSeed ( EF EF )
        //{
        //    var MyStudents = new List<Student>{
        //        new Student {STUDENT_ID = "DI100", FIRSTNAME = "Halid", LASTNAME = "Cisse", PHONE_NUMBER = "00122445545"},
        //        new Student {STUDENT_ID = "DI102", FIRSTNAME = "Mat", LASTNAME = "Pearson", PHONE_NUMBER = "0012244545545"},
        //        new Student {STUDENT_ID = "DI103", FIRSTNAME = "Dave" , LASTNAME = "Wood", PHONE_NUMBER = "001544545578"},
        //        new Student {STUDENT_ID = "DI104", FIRSTNAME = "Adam", LASTNAME = "Nolan", PHONE_NUMBER = "0012445454545"}
        //    };

        //    MyStudents.ForEach (Student => EF.STUDENT.Add (Student));
        //}

        //private static void AddPersonSeed ( EF EF )
        //{
        //    var MyPersons = new List<Person> {
        //        //new Person {PERSON_ID = "STAFF_IDDI100", FIRSTNAME = "Halid", LASTNAME = "Cisse", PHONE_NUMBER = "00122445545"},
        //        //new Person {PERSON_ID = "STAFF_IDDI102", FIRSTNAME = "Mat", LASTNAME = "Pearson", PHONE_NUMBER = "0012244545545"},
        //        //new Person {PERSON_ID = "STAFF_IDDI103", FIRSTNAME = "Dave" , LASTNAME = "Wood", PHONE_NUMBER = "001544545578"},
        //        //new Person {PERSON_ID = "STAFF_IDDI104", FIRSTNAME = "Adam", LASTNAME = "Nolan", PHONE_NUMBER = "0012445454545"}
        //    };

        //    //MyPersons.ForEach (Person => EF.PERSON.Add (Person));
        //}

        //private static void AddStaffSeed ( EF EF )
        //{
        //    var MyStaffs = new List<Staff> {
        //        new Staff {STAFF_ID = "DI100", DEPARTEMENT = "Info", POSITION = "Chef de Departement"},
        //        new Staff {STAFF_ID = "DI102", DEPARTEMENT = "Math", POSITION = "Chef de Departement"},
        //        new Staff {STAFF_ID = "DI103", DEPARTEMENT = "Bio", POSITION = "Chef de Departement"},
        //        new Staff {STAFF_ID = "DI104", DEPARTEMENT = "Chimie", POSITION = "Chef de Departement"}
        //    };

        //    MyStaffs.ForEach (Staff => EF.STAFF.Add (Staff));
        //}


        //#endregion

        

        //#region Helpers

        //private static byte[] BitmapArrayFromFile ( string ImageFilePath )
        //{
        //    if(!File.Exists (ImageFilePath)) return null;

        //    var fs = new FileStream (ImageFilePath, FileMode.Open, FileAccess.Read);
        //    var imgByteArr = new byte[fs.Length];
        //    fs.Read (imgByteArr, 0, Convert.ToInt32 (fs.Length));
        //    fs.Close ();
        //    return imgByteArr;
        //}
        //private static string GetRes ( string pathInApplication, Assembly assembly = null )
        //{
        //    if(assembly == null)
        //    {
        //        assembly = Assembly.GetCallingAssembly ();
        //    }

        //    if(pathInApplication[0] == '/')
        //    {
        //        pathInApplication = pathInApplication.Substring (1);
        //    }
        //    return (new Uri (@"pack://application:,,,/" + assembly.GetName ().Name + ";component/" + pathInApplication, UriKind.Absolute).ToString ());
        //}

        //#endregion



    }
}
