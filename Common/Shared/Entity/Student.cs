using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Pedagogy.Entity;
using Common.Shared.Enums;

namespace Common.Shared.Entity
{
    /// <summary>
    /// Represente un Etudiant, Stagiaire, Eleve
    /// </summary>
    public class Student 
    {
        
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// Matricule
        /// </summary>
        public string Matricule { get; set; }

        /// <summary>
        /// Statut
        /// </summary>
        public StudentStatus Statut { get; set; }

        /// <summary>
        /// La personne Associer
        /// </summary>       
       
        public virtual Person Person { get; set; }
        /// <summary>
        /// Tuteur
        /// </summary>
        
        public virtual Person Guardian { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Enrollement> Inscriptions { get; set; } = new HashSet<Enrollement>();
        
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new HashSet<StudentGrade>();



        //[ForeignKey("PersonGuid")]
        //[ForeignKey("PersonGuid")]

        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<SchoolFee> SchoolFees { get; set; } = new HashSet<SchoolFee>();

        ///// <summary>
        ///// Guid de la personne Associer
        ///// </summary>
        //public Guid PersonGuid { get; set; }

        ///// <summary>
        ///// Guid de la personne Tuteur
        ///// </summary>
        //public Guid GuardianGuid { get; set; }


        ///// <summary>
        ///// Title
        ///// </summary>
        //public StudentTitles Title { get; set; } 

    }
}















