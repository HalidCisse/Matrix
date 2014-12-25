using System.Data.Entity;
using DataService.Entities;
using DataService.Entities.Pedagogy;
using DataService.Entities.Security;

namespace DataService.Context
{
    /// <summary>
    /// School Context
    /// </summary>
    public class Ef : DbContext
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
        public Ef ( ) : base ("name=conString")
        {
            //Database.CreateIfNotExists();
            //Database.SetInitializer (new DropCreateDatabaseIfModelChanges<EF> ());
            Configuration.ProxyCreationEnabled = false;           
            //Database.SetInitializer (new DBInitializer ());
        }


        #region 

        /// <summary>
        /// Enseignant, instructeur, Proff
        /// </summary>
        public virtual DbSet<Staff> Staff { get; set; }

        /// <summary>
        /// Etudiant, Stagiaire, Eleve
        /// </summary>
        public virtual DbSet<Student> Student { get; set; }

        /// <summary>
        /// Informations Ecoles
        /// </summary>
        //public virtual DbSet<Etablissement> Etablissement { get; set; }

        /// <summary>
        /// Salle Ou on Instruit un Cours
        /// </summary>
        //public virtual DbSet<Salle> Salle { get; set; }

        /// <summary>
        /// Les Parametres
        /// </summary>
        public virtual DbSet<UserSetting> UserSetting { get; set; }

        /// <summary>
        /// Les Parametres System
        /// </summary>
        public virtual DbSet<MatrixSetting> MatrixSetting { get; set; }

        


        #endregion


        #region SECURITE

        /// <summary>
        /// Profile D'Utilisateurs
        /// </summary>
        public virtual DbSet<UserProfile> UserProfile { get; set; }

        /// <summary>
        /// Role D'Utilisateurs
        /// </summary>
        public virtual DbSet<UserRoles> UserRole { get; set; }


        #endregion


        #region PEDAGOGY

        /// <summary>
        /// Represent une classe avec un groupe d'etudiant
        /// </summary>
        public virtual DbSet<Classe> Classe { get; set; }
        /// <summary>
        /// Represent une Filiere avec un groupe de Classe
        /// </summary>
        public virtual DbSet<Filiere> Filiere { get; set; }
        /// <summary>
        /// Represent une Matiere Enseigner dans une Seule Classe
        /// </summary>
        public virtual DbSet<Matiere> Matiere { get; set; }
        /// <summary>
        /// Represente une Periode d'Examen Exp: 1 ere Semestre
        /// </summary>
        public virtual DbSet<PeriodeScolaire> PeriodeScolaire { get; set; }
        /// <summary>
        /// Represente la Note obtenue par un Etudiant apres un control
        /// </summary>
        public virtual DbSet<ControlNote> ControlNote { get; set; }
        /// <summary>
        /// Represente un Cours
        /// </summary>
        public virtual DbSet<Cours> Cours { get; set; }

        /// <summary>
        /// Devoir, Control, ou Examen
        /// </summary>
        public virtual DbSet<MatiereControl> MatiereControl { get; set; }    
                   
        /// <summary>
        /// Les Etudiants d'une Classe
        /// </summary>
        public virtual DbSet<ClasseStudents> ClasseStudents { get; set; } 

        /// <summary>
        /// Les Instructeurs d'une Matiere
        /// </summary>
        public virtual DbSet<MatiereInstructeurs> MatieresInstructeurs { get; set; }

        /// <summary>
        /// Represente une Annee Scolaire
        /// </summary>
        public virtual DbSet<AnneeScolaire> AnneeScolaire { get; set; }   
             
        /// <summary>
        /// L'inscription d'un etudiant a une classe en une Annee Scolaire
        /// </summary>
        public virtual DbSet<Inscription> Inscription { get; set; }

        /// <summary>
        /// Les Conditions d'inscription
        /// </summary>
        public virtual DbSet<InscriptionRule> InscriptionRule { get; set; }

        /// <summary>
        /// Qualifications
        /// </summary>
        public virtual DbSet<Qualification> Qualification { get; set; }

        /// <summary>
        /// Les Qualifications d'un etudiant
        /// </summary>
        public virtual DbSet<StudentQualification> StudentQualification { get; set; }

        /// <summary>
        /// Presence d'une Personne a un Cours
        /// </summary>
        public virtual DbSet<AbsenceTicket> AbsenceTicket { get; set; }

        

        #endregion



        #region RELATIONS MAPPING


        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        ///             before the model has been locked down and used to initialize the context.  The default
        ///             implementation of this method does nothing, but it can be overridden in a derived class
        ///             such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        ///             is created.  The model for that context is then cached and is for all further instances of
        ///             the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///             property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///             More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///             classes directly.
        /// </remarks>
        /// <param name="modelBuilder">The builder that defines the model for the context being created. </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

        }


        #endregion



    }

    
}