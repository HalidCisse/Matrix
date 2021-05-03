using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Shared.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Salle
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid SalleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Adresse { get; set; }


    }
}
