using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class ControlNote
    {
        [Key]
        public string CONTROLNOTE_ID { get; set; }

        public string MATIERECONTROL_ID { get; set; }

        public string STUDENT_ID { get; set; }

        public int NOTE { get; set; }

    }
}
