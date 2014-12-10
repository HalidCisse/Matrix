using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Salle
    {

        [Key]
        public Guid SalleId { get; set; }

        public string Name { get; set; }

        public string Adresse { get; set; }


    }
}
