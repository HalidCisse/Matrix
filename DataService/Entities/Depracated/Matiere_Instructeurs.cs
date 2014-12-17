using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    public class MatiereInstructeurs
    {

        [Key]
        public Guid MatiereInstructeursId { get; set; } // MATIERE_ID + STAFF_ID

        public Guid MatiereId { get; set; }

        public String StaffId { get; set; }

    }
}
