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
            Database.CreateIfNotExists();
            //Database.SetInitializer (new DropCreateDatabaseIfModelChanges<EF> ());
            Configuration.ProxyCreationEnabled = false;           
            //Database.SetInitializer (new DBInitializer ());
        }
        


        public virtual DbSet<Staff> STAFF { get; set; }
        public virtual DbSet<Student> STUDENT { get; set; }

        //_______________________________________________________________________________//
        public virtual DbSet<Classe> CLASSE { get; set; }
        public virtual DbSet<Filiere> FILIERE { get; set; }
        public virtual DbSet<Matiere> MATIERE { get; set; }
        public virtual DbSet<ControlNote> CONTROL_NOTE { get; set; }
        public virtual DbSet<Cours> COURS { get; set; }
        public virtual DbSet<MatiereControl> MATIERE_CONTROL { get; set; }

        //_______________________________________________________________________________//
        public virtual DbSet<Filiere_Classes> FILIERE_CLASSES { get; set; }
        public virtual DbSet<Classe_Students> CLASSE_STUDENTS { get; set; }       
        public virtual DbSet<Filiere_Matieres> FILIERE_MATIERE { get; set; }
        public virtual DbSet<Matiere_Instructeurs> MATIERES_INSTRUCTEURS { get; set; }

        













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