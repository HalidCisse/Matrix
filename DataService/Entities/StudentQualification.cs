using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
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
        public Guid STUDENT_QUALIFICATION_ID { get; set; }

        /// <summary>
        /// ID de l'Etudiant
        /// </summary>
        public string STUDENT_ID { get; set; }
         
        /// <summary>
        /// ID de la Qualification
        /// </summary>
        public Guid QUALIFICATION_ID { get; set; }


    }
}
