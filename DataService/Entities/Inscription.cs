using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
