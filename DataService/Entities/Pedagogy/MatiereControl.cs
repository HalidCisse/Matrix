using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class MatiereControl
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid MatierecontrolGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CoursGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Duration { get; set; }


    }
}
