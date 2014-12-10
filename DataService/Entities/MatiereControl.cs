using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class MatiereControl
    {

        [Key]
        public string MatierecontrolId { get; set; }

        public string CoursId { get; set; }

        public DateTime? StartTime { get; set; }

        public int Duration { get; set; }


    }
}
