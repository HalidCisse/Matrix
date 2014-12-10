using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class ClasseStudents
    {

        [Key]
        public string ClasseStudentsId { get; set; }

        public string ClasseId { get; set; }

        public string StudentId { get; set; }

    }
}
