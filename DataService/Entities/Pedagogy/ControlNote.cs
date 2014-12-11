using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    public class ControlNote
    {
        [Key]
        public string ControlnoteId { get; set; }

        public string MatierecontrolId { get; set; }

        public string StudentId { get; set; }

        public int Note { get; set; }

    }
}
