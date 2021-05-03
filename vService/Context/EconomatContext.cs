using System.Data.Entity;
using System.Diagnostics;
using Common.Economat.Entity;

namespace DataService.Context
{
    internal class EconomatContext : DbContext
    {

        [DebuggerStepThrough]
        public EconomatContext() : base("name=conString")
        {            
            //Configuration.ProxyCreationEnabled = false;           
        }

        //          Update-Database -ConfigurationTypeName DataService.Migrations.SchoolContext.Configuration
        //          Update-Database -ConfigurationTypeName DataService.Migrations.EconomatContext.Configuration






        /// <summary>
        /// Employements des Staffs
        /// </summary>
        public virtual DbSet<Employment> Employments { get; set; }

        /// <summary>
        /// Renumerations des Employers
        /// </summary>
        public virtual DbSet<Salary> Salaries { get; set; }

        /// <summary>
		/// Methode des Payements des salaires des Staffs et des Enseignants
		/// </summary>
		public virtual DbSet<Payroll> Payrolls { get; set; }


        /// <summary>
        /// Recue de payement des frais d'etudes
        /// </summary>
        public virtual DbSet<SchoolFee> SchoolFees { get; set; }


        /// <summary>
        /// Transactions Caisse
        /// </summary>
        public virtual DbSet<Transaction> Transactions { get; set; }





        //protected override void OnModelCreating (DbModelBuilder modelBuilder) {
        //    modelBuilder.Ignore<Employments>();
        //    modelBuilder.Ignore<Payment>();
        //    //modelBuilder.Configurations.Add(new ShippingAddressMap());
        //}


    }
}
