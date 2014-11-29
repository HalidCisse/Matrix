using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    /// <summary>
    /// Qualification Deja Acquise
    /// Ex : BAC, BAC+ 1, BAC + 2
    /// </summary>
    
    public class Qualification
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid QUALIFICATION_ID { get; set; }

        /// <summary>
        /// Ex : Engenieur
        /// </summary>
        public string NIVEAU { get; set; } // 

        /// <summary>
        /// La Filiere
        /// </summary>
        public Guid FILIERE_ID { get; set; } 

        /// <summary>
        /// Etablissement
        /// </summary>
        public string ETABLISSEMENT { get; set; } 
                  
        /// <summary>
        /// Bac + ?
        /// </summary>
        public int BAC_PLUS { get; set; } 

        /// <summary>
        /// Description
        /// </summary>
        public string DESCRIPTION { get; set; }

    }
}
