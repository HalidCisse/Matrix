using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
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
        public Guid StudentQualificationId { get; set; }

        /// <summary>
        /// ID de l'Etudiant
        /// </summary>
        public string StudentId { get; set; }
         
        /// <summary>
        /// ID de la Qualification
        /// </summary>
        public Guid QualificationId { get; set; }


    }
}
