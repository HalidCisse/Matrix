using System;
using System.ComponentModel.DataAnnotations;



namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentNote
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]     
        public Guid StudentNoteGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CoursGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Note { get; set; } = 0.0;

        /// <summary>
        /// Appreciation
        /// </summary>
        public string Appreciation { get; set; } = "";



    }
}
