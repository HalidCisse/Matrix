using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Cours Exceptions
    /// </summary>
    public class StudyException {
       
        /// <summary>
        /// Cours Exceptions ID
        /// </summary>
        [Key]
        public Guid StudyExceptionGuid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID du Cours
        /// </summary>
        public Guid StudyGuid { get; set; }

        /// <summary>
        /// Date de l'Exception
        /// </summary>
        public DateTime? ExceptionDate { get; set; } = DateTime.Today;


        /// <summary>
        /// Le groupe d'etudiant
        /// </summary>
        [ForeignKey("StudyGuid")]
        public virtual Study Study { get; set; }
    }
}
