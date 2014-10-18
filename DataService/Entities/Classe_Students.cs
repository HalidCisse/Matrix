using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Classe_Students
    {

        [Key]
        public string CLASSE_STUDENTS_ID { get; set; }

        public string CLASSE_ID { get; set; }

        public string STUDENT_ID { get; set; }

    }
}
