using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Pedagogy.Entity;
using Common.Shared.Enums;

namespace Common.Shared.Entity
{
    /// <summary>
    /// Represente Un Employer de L'Ecole, Staff ou Instructeur
    /// </summary>
    public class Staff 
    {

        /// <summary>
        /// StaffGuid
        /// </summary>
        [Key]
        public Guid StaffGuid { get; set; }

        /// <summary>
        /// Guid de la personne Associer
        /// </summary>
        public Guid PersonGuid { get; set; }

        /// <summary>
        /// StaffId
        /// </summary>
        public string Matricule { get; set; }
       
        /// <summary>
        /// Position
        /// </summary>
        public string PositionPrincipale { get; set; }

        /// <summary>
        /// Departement
        /// </summary>
        public string DepartementPrincipale { get; set; }

        /// <summary>
        /// Division/Groupe
        /// </summary>
        public string Division { get; set; }

        /// <summary>
        /// Qualification
        /// </summary>
        public string Qualification { get; set; }

        /// <summary>
        /// Qualification
        /// </summary>
        public string Diploma { get; set; }

        /// <summary>
        /// NiveauDiplome
        /// </summary>
        public string DiplomaLevel { get; set; }

        /// <summary>
        /// Experience du Staff
        /// </summary>
        public int Experiences { get; set; }

        /// <summary>
        /// Ancien job
        /// </summary>
        public string FormerJob { get; set; }        

        /// <summary>
        /// Senior
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// HiredDate
        /// </summary>
        public DateTime? HiredDate { get; set; }

        /// <summary>
        /// Suspendu, Regulier, Licencier
        /// </summary>
        public StaffStatus Statut { get; set; }




        /// <summary>
        /// La personne Associer
        /// </summary>
        [ForeignKey("PersonGuid")]
        public virtual Person Person { get; set; }
            
        /// <summary>
        /// Les Specialites du Staff
        /// </summary>
        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();



        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<Study> Cours { get; set; } = new HashSet<Study>();  
         
        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<Employment> Employments { get; set; } = new HashSet<Employment>();

       

    }
}
