using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Inscription
    {
        [Key]
        public Guid INSCRIPTION_ID { get; set; }

        public string STUDENT_ID { get; set; }

        public Guid CLASSE_ID { get; set; }

    }
}
