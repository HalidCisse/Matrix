using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    public class ClasseStudents
    {

        [Key]
        public string ClasseStudentsId { get; set; }

        public string ClasseId { get; set; }

        public string StudentId { get; set; }

    }
}
