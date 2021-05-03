using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows;
using CLib;
using Common.Shared.Entity;
using Common.Shared.Enums;
using DataService.Properties;

namespace DataService.Migrations.SchoolContext
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.SchoolContext>
    {
        public Configuration()
        {
            //Database.CreateIfNotExists();
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations\SchoolContext";

            //          Update-Database -ConfigurationTypeName DataService.Migrations.SchoolContext.Configuration
            //          Update-Database -ConfigurationTypeName DataService.Migrations.EconomatContext.Configuration
        }

        protected override void Seed(Context.SchoolContext ef)
        {
           
            ef.Database.CreateIfNotExists();

            //ProfileSeed(context);
            //Staff_SeedFromSql(context);
            //Student_SeedFromSql(context);


            var sqlStudentPeople = Resources.dbo_People_data;
            var sqlStudents      = Resources.Students_data;
            var sqlStaffs        = Resources.dbo_Staffs_data;
            var sqlSubjects      = Resources.dbo_Subjects_data;
            var sqlFilieres      = Resources.dbo_Filieres_data;
            var sqlClasses       = Resources.dbo_Classes_data;

            ef.Database.ExecuteSqlCommand("DELETE FROM Classes");
            ef.Database.ExecuteSqlCommand("DELETE FROM Filieres");
            ef.Database.ExecuteSqlCommand("DELETE FROM Subjects");
            ef.Database.ExecuteSqlCommand("DELETE FROM Staffs");
            ef.Database.ExecuteSqlCommand("DELETE FROM Students");
            ef.Database.ExecuteSqlCommand("DELETE FROM People");

           
            ef.Database.ExecuteSqlCommand(sqlStudentPeople);
            ef.Database.ExecuteSqlCommand(sqlStudents);
            ef.Database.ExecuteSqlCommand(sqlStaffs);
            ef.Database.ExecuteSqlCommand(sqlSubjects);
            ef.Database.ExecuteSqlCommand(sqlFilieres);
            ef.Database.ExecuteSqlCommand(sqlClasses);

            var x = ef.Set<Person>().ToList();
            x.ForEach(s => s.PhotoIdentity=GetRandomImg());

            var admin = new Staff {
                StaffGuid = new Guid("53f258a3-f931-4975-b6ec-17d26aa95848"),
                Matricule="SS-124-3652",
                PositionPrincipale="Chef de Departement Info",
                DepartementPrincipale="Informatique",
                HiredDate=DateTime.Today.AddDays(-500),
                Statut=StaffStatus.Actif,

                Person=new Person {
                    Title=PersonTitles.Mr,
                    PersonGuid = new Guid("53f258a3-f931-4975-b6ec-17d26aa95848"),
                    FirstName="Halid",
                    LastName="cisse",
                    Nationality="Mali",
                    IdentityNumber="",
                    BirthDate=DateTime.Today.AddDays(-5000),
                    BirthPlace="Tayba",
                    PhoneNumber="0012547874",
                    EmailAdress="halid@gmail.com",
                    HomeAdress="Mabella",
                    RegistrationDate=DateTime.Today.AddDays(-100)
                }
            };

            ef.Staffs.AddOrUpdate(admin);

            MessageBox.Show("Seed Done");
        }

        private static void ProfileSeed(Context.SchoolContext ef)
        {
            //var adminUserprofiles = new UserProfile
            //{
            //    UserProfileGuid = MatrixConstants.SystemGuid(),
            //    UserSpace = UserSpace.AdminSpace
            //};

            //var adminUserRoles = new UserRoles
            //{
            //    UserProfileGuid = MatrixConstants.SystemGuid(),
            //    CanAddStudent = true,
            //    CanDeleteStudent = true
            //};

            //var adminSettings = new UserSetting
            //{
            //    UserProfileGuid = MatrixConstants.SystemGuid()
            //};

            //ef.UserProfile.AddOrUpdate(adminUserprofiles);

            //ef.UserRole.AddOrUpdate(adminUserRoles);

            //ef.UserSetting.AddOrUpdate(adminSettings);

        }

        private static void Staff_SeedFromSql(Context.SchoolContext ef)
        {            
            var sqlString = Resources.Staffs; // RessourcesHelper.GetEmbeddedResource("DataService.Migrations.InitData.Staffs.sql");
          
            //ef.Database.ExecuteSqlCommand("DELETE FROM Staffs");
            //ef.Database.ExecuteSqlCommand(sqlString);

            var admin = new Staff
            {
                StaffGuid = MatrixConstants.SystemGuid(),
                Matricule = "SS-124-3652",
                PositionPrincipale = "Chef de Departement Info",
                DepartementPrincipale= "Informatique",
                HiredDate = DateTime.Today.AddDays(-500),
                Statut = StaffStatus.Actif,
                
                Person = new Person 
                {
                    Title=PersonTitles.Mr,
                    PersonGuid = Guid.NewGuid(),
                    FirstName = "Halid",
                    LastName = "cisse",
                    Nationality = "Mali",
                    IdentityNumber = "",
                    BirthDate = DateTime.Today.AddDays(-5000),
                    BirthPlace = "Tayba",
                    PhoneNumber = "0012547874",
                    EmailAdress = "halid@gmail.com",
                    HomeAdress = "Mabella",
                    RegistrationDate = DateTime.Today.AddDays(-100)
                }
            };

            ef.Staffs.AddOrUpdate(admin);

            var x = ef.Staffs.ToList();
            x.ForEach(s => s.Person.PhotoIdentity = GetRandomImg());                              
        }

        private static void Student_SeedFromSql(DbContext ef)
        {
            

            //var sqlStudentPeople = Resources.dbo_People_data;
            //var sqlStudents      = Resources.Students_data;

            //ef.Database.ExecuteSqlCommand("DELETE FROM Students");
            //ef.Database.ExecuteSqlCommand("DELETE FROM People");
            

            //ef.Database.ExecuteSqlCommand(sqlStudentPeople);
            //ef.Database.ExecuteSqlCommand(sqlStudents);

            //var x = ef.Set<Person>().ToList();
            //x.ForEach(s => s.PhotoIdentity=GetRandomImg());


            //var sqlString = Resources.Students;
            //ef.Database.ExecuteSqlCommand("DELETE FROM Students");
            //ef.Database.ExecuteSqlCommand(sqlString);            

            //var x = ef.Students.ToList();

            //x.ForEach(s => s.Person.PhotoIdentity = GetRandomImg());            
        }

        #region Helpers

     

        private static byte[] GetRandomImg()
        {                        
            var x = RandomHelper.Random(1, 22);
            
            var imgName = "portrait" + x;

            var img = (Image)Resources.ResourceManager.GetObject(imgName, CultureInfo.InvariantCulture);      
           
            return img == null ? null : ImagesHelper.ImageToByteArray(img);
        }

       

        #endregion







    }
}
