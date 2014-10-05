using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities
{
    public class Staff
    {
        [Key]
        public string STAFF_ID { get; set; }

        [ForeignKey ("PERSON")]
        public string PERSON_ID { get; set; }

        public string POSITION { get; set; }    
        public string DEPARTEMENT { get; set; }
      
        public string HIRED_DATE { get; set; }       
      
        public string STATUT { get; set; }  // suspended, regulier, Licencier

        [Required]
        public virtual Person PERSON { get; set; }
    }
}
