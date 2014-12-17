using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Represente Un Employer de L'Ecole, Staff ou Instructeur
    /// </summary>
    public class Staff 
    {

        /// <summary>
        /// 
        /// </summary>
        public Staff ()
        {
            RegistrationDate = DateTime.Now;
            StaffGuid = Guid.NewGuid();
        }
       
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid StaffGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Position { get; set; }  
          
        /// <summary>
        /// 
        /// </summary>
        public string Departement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? HiredDate { get; set; }

        /// <summary>
        /// Suspendu, Regulier, Licencier
        /// </summary>
        public string Statut { get; set; }  



        #region Human

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] PhotoIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HomeAdress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName => FirstName + " " + LastName;

        #endregion


       
    }
}
