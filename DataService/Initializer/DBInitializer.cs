using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using DataService.Context;
using DataService.Entities;

namespace DataService.Initializer
{
    class DBInitializer : DropCreateDatabaseIfModelChanges<EF>
    {

        protected override void Seed ( EF EF )
        {

            AddStudentSeed (EF);



        }



        #region SEEDS

        private static void AddStudentSeed ( EF EF )
        {
            var MyStudents = new List<Student>{
                new Student {STUDENT_ID = "DI100", FIRSTNAME = "Halid", LASTNAME = "Cisse", PHONE_NUMBER = "00122445545"},
                new Student {STUDENT_ID = "DI102", FIRSTNAME = "Mat", LASTNAME = "Pearson", PHONE_NUMBER = "0012244545545"},
                new Student {STUDENT_ID = "DI103", FIRSTNAME = "Dave" , LASTNAME = "Wood", PHONE_NUMBER = "001544545578"},
                new Student {STUDENT_ID = "DI104", FIRSTNAME = "Adam", LASTNAME = "Nolan", PHONE_NUMBER = "0012445454545"}
            };

            MyStudents.ForEach (Student => EF.STUDENT.Add (Student));
        }




        #endregion



        #region Helpers

        private static byte[] BitmapArrayFromFile ( string ImageFilePath )
        {
            if(!File.Exists (ImageFilePath)) return null;

            var fs = new FileStream (ImageFilePath, FileMode.Open, FileAccess.Read);
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
