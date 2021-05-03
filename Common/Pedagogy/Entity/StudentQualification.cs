using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Student Qualification
    /// </summary>
    public class StudentQualification
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid StudentQualificationGuid { get; set; }

        /// <summary>
        /// ID de l'Etudiant
        /// </summary>
        public Guid StudentGuid { get; set; }
         
        /// <summary>
        /// ID de la Qualification
        /// </summary>
        public Guid QualificationGuid { get; set; }


    }
}
