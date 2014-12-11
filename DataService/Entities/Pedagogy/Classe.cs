using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    public class Classe
    {
        [Key]
        public Guid ClasseId { get; set; }

        public string Name { get; set; }

        public Guid FiliereId { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public Guid AnneeScolaireId { get; set; }

        
    }
}
