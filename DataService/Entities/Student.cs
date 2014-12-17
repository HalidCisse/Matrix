using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Represente un Etudiant, Stagiaire, Eleve
    /// </summary>
    public class Student 
    {
        /// <summary>
        /// 
        /// </summary>
        public Student()
        {
            RegistrationDate = DateTime.Now;
            StudentGuid = Guid.NewGuid();
        }

        /// <summary>
        /// 
        /// </summary>
        [Key]        
        public Guid StudentGuid { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(20)]       
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

        //[Column ("BIRTH_DATE", TypeName="DateTime2")]
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BirthDate { get; set; }

       // [Column(TypeName = "date")]
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
        public string Statut { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? RegistrationDate { get; set; }

    }
}
















//#region Trans

///// <summary>
///// Renvoi le nom complet exp: Halid Cisse
///// </summary>
//public string FullName => FirstName + " " + LastName;

///// <summary>
///// Renvoi le La Filiere Du l'étudiant
///// </summary>
//public string Course => "Software Developper ";

///// <summary>
///// Renvoi la Niveau de sa Classe Actuelle
///// </summary>
//public string Level => "1 ere Annnee";

//#endregion