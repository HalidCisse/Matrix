using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class StudentQualification
    {
        [Key]
        public Guid STUDENT_QUALIFICATION_ID { get; set; }

        public string STUDENT_ID { get; set; }
         
        public Guid QUALIFICATION_ID { get; set; }


    }
}
