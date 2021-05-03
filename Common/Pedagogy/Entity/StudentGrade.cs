using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using Common.Shared.Entity;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// StudentNote
    /// </summary>
    public class StudentGrade: Tracable
    {
        /// <summary>
        /// StudentNoteGuid
        /// </summary>
        [Key]     
        public Guid StudentGradeGuid { get; set; }

        /// <summary>
        /// CoursGuid
        /// </summary>
        public Guid CoursGuid { get; set; }

        /// <summary>
        /// StudentGuid
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// DateTaken
        /// </summary>
        public DateTime? DateTaken { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        public double Mark { get; set; }

        /// <summary>
        /// Coefficient
        /// </summary>
        public int Coefficient { get; set; }

        /// <summary>
        /// Barem
        /// </summary>
        public double Barem { get; set; }

        /// <summary>
        /// Appreciation
        /// </summary>
        public string Appreciation { get; set; }

        /// <summary>
        /// Note coefficier
        /// </summary>
        [NotMapped]
        public double GradeCoeff => (Mark * Coefficient);


        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("StudentGuid")]
        public virtual Student Student { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("CoursGuid")]
        public virtual Study Study { get; set; }

    }
}
