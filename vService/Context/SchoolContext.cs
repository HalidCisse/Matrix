using System.Data.Entity;
using System.Diagnostics;
using Common.Comm.Entity;
using Common.Pedagogy.Entity;
using Common.Shared.Entity;

namespace DataService.Context
{
    /// <summary>
    /// School Context
    /// </summary>
    internal class SchoolContext : DbContext
    {
        // Your context has been configured to use a 'EF' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataService.EF' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'EF' 
        // connection string in the application configuration file.

        /// <summary>
        /// Context
        /// </summary>
        [DebuggerStepThrough]
        public SchoolContext() : base("name=conString")
        {
            //Configuration.LazyLoadingEnabled=false;

            //"name=conString"
            //Database.CreateIfNotExists();
            //Database.SetInitializer (new DropCreateDatabaseIfModelChanges<EF> ());
            //Configuration.ProxyCreationEnabled = false;
            //Database.SetInitializer (new DBInitializer ());
        }

        

        #region ESchool

        /// <summary>
        /// Enseignant, instructeur, Proff
        /// </summary>
        public virtual DbSet<Staff> Staffs { get; set; }

        /// <summary>
        /// Etudiant, Stagiaire, Eleve
        /// </summary>
        public virtual DbSet<Student> Students { get; set; }

        /// <summary>
        /// Message Privé, Email, Multicast ou Annonce
        /// </summary>
        public virtual DbSet<Conversation> Conversations { get; set; }



        ///// <summary>
        ///// Etudiant, Stagiaire, Eleve
        ///// </summary>
        //public virtual DbSet<Person> Peoples { get; set; }

        ///// <summary>
        ///// Les Parametres
        ///// </summary>
        //public virtual DbSet<UserSetting> UserSetting { get; set; }

        /// <summary>
        /// Les Parametres System
        /// </summary>
        public virtual DbSet<MatrixSetting> SystemSetting { get; set; }




        #endregion


        #region SECURITE

        //// <summary>
        //// Profile D'Utilisateurs
        //// </summary>
        //public virtual DbSet<UserProfile> UserProfile { get; set; }

        ///// <summary>
        ///// Role D'Utilisateurs
        ///// </summary>
        //public virtual DbSet<UserRoles> UserRole { get; set; }


        #endregion


        #region PEDAGOGY

        /// <summary>
        /// Represent une classe avec un groupe d'etudiant
        /// </summary>
        public virtual DbSet<Classe> Classes { get; set; }

        /// <summary>
        /// Represent une Filiere avec un groupe de Classe
        /// </summary>
        public virtual DbSet<Filiere> Filieres { get; set; }

        /// <summary>
        /// Represent une Matiere Enseigner dans une Seule Classe
        /// </summary>
        public virtual DbSet<Subject> Subjects { get; set; }

        /// <summary>
        /// Represente une Periode d'Examen Exp: 1 ere Semestre
        /// </summary>
        public virtual DbSet<SchoolPeriod> SchoolPeriods { get; set; }  

        /// <summary>
        /// Represente la Note obtenue par un Etudiant apres un control
        /// </summary>
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }

        /// <summary>
        /// Represente un Cours, Devoir, Control, ou Examen
        /// </summary>
        public virtual DbSet<Study> Studies { get; set; }

        /// <summary>
        /// Cours Exception
        /// </summary>
        public virtual DbSet<StudyException> StudyExceptions { get; set; }

        ///// <summary>
        ///// Les Etudiants d'une Classe
        ///// </summary>
        //public virtual DbSet<ClasseStudents> ClasseStudents { get; set; }

        /// <summary>
        /// Represente une Annee Scolaire
        /// </summary>
        public virtual DbSet<SchoolYear> SchoolYears { get; set; } 

        /// <summary>
        /// L'inscription d'un etudiant a une classe en une Annee Scolaire
        /// </summary>
        public virtual DbSet<Enrollement> Enrollements { get; set; }

        ///// <summary>
        ///// Les Conditions d'inscription
        ///// </summary>
        //public virtual DbSet<InscriptionRule> InscriptionRules { get; set; }

        ///// <summary>
        ///// Qualifications
        ///// </summary>
        //public virtual DbSet<Qualification> Qualifications { get; set; }

        ///// <summary>
        ///// Les Qualifications d'un etudiant
        ///// </summary>
        //public virtual DbSet<StudentQualification> StudentQualifications { get; set; }

        /// <summary>
        /// Presence d'une Personne a un Cours
        /// </summary>
        public virtual DbSet<AbsenceTicket> AbsenceTickets { get; set; }



        #endregion



        #region RELATIONS MAPPING

        protected override void OnModelCreating (DbModelBuilder modelBuilder) {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<KeyDiscoveryConvention>();

            //modelBuilder.Entity<Staff> ()
            //    .HasRequired (e => e.PERSON)
            //    .WithRequiredPrincipal ();

            //modelBuilder.Entity<Course> ()
            //    .HasMany (c => c.Instructors).WithMany (i => i.Courses)
            //    .Map (t => t.MapLeftKey ("CourseID")
            //        .MapRightKey ("InstructorID")
            //        .ToTable ("CourseInstructor"));


            modelBuilder.Entity<Student>()
               .HasOptional(c => c.Person)
               .WithOptionalDependent()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasOptional(s => s.Guardian)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Study>()
            //   .HasRequired(c => c.Proff)
            //   .WithOptional()
            //   .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Study>()
            //    .HasRequired(s => s.Grader)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Study>()
            //    .HasRequired(s => s.Supervisor)
            //    .WithOptional()
            //    .WillCascadeOnDelete(false);
        }


        //protected override void OnModelCreating (DbModelBuilder modelBuilder) {
        //    modelBuilder.Ignore<Salary>();
        //    modelBuilder.Ignore<Payroll>();
        //    modelBuilder.Ignore<SchoolFee>();
        //    //modelBuilder.Ignore<Payroll>();
        //    //modelBuilder.Configurations.Add(new ShippingAddressMap());
        //}


        #endregion



    }


}


//          Update-Database -ConfigurationTypeName DataService.Migrations.SchoolContext.Configuration
//          Update-Database -ConfigurationTypeName DataService.Migrations.EconomatContext.Configuration