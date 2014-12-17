using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class ClasseStudents
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid ClasseStudentsGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ClasseGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid StudentGuid { get; set; }

    }
}
