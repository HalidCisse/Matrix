using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataService.Entities;
using DataService.Initializer;

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
            //Database.SetInitializer (new DropCreateDatabaseIfModelChanges<EF> ());
            Configuration.ProxyCreationEnabled = false;           
            //Database.SetInitializer (new DBInitializer ());
        }


        //public virtual DbSet<Person> PERSON { get; set; }
        public virtual DbSet<Staff> STAFF { get; set; }
        public virtual DbSet<Student> STUDENT { get; set; }


        //protected override void OnModelCreating ( DbModelBuilder modelBuilder )
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention> ();
        //    modelBuilder.Conventions.Remove<KeyDiscoveryConvention>();
        //    //modelBuilder.Entity<Staff> ()
        //    //    .HasRequired (e => e.PERSON)
        //    //    .WithRequiredPrincipal ();

        //    //modelBuilder.Entity<Course> ()
        //    //    .HasMany (c => c.Instructors).WithMany (i => i.Courses)
        //    //    .Map (t => t.MapLeftKey ("CourseID")
        //    //        .MapRightKey ("InstructorID")
        //    //        .ToTable ("CourseInstructor"));



        //}

        


    }

    
}