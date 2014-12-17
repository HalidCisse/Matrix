using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class Classe
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid ClasseGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid FiliereGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid AnneeScolaireGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        

        
    }
}
