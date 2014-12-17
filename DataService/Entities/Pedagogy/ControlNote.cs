using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class ControlNote
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid ControlNoteGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid MatiereControlGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Note { get; set; }

    }
}
