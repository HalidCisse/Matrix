using System.Data.Entity;
using DataService.Entities;

namespace DataService.Context
{
    public class EF : DbContext
    {
        // Your context has been configured to use a 'EF' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataService.EF' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EF' 
        // connection string in the application configuration file.
        public EF ( ) : base ("name=EF")
        {
            Database.SetInitializer (new DropCreateDatabaseIfModelChanges<EF> ());
            //Database.SetInitializer (new DBInitializer ());
        }


        public virtual DbSet<Student> Student { get; set; }



        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }


    }

    
}