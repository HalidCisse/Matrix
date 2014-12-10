using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
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
