﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Pedagogy.Entity
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
        public Guid QualificationGuid { get; set; }

        /// <summary>
        /// Ex : Engenieur
        /// </summary>
        public string Niveau { get; set; } // 

        /// <summary>
        /// La Filiere
        /// </summary>
        public Guid FiliereGuid { get; set; } 

        /// <summary>
        /// Etablissement
        /// </summary>
        public string Etablissement { get; set; } 
                  
        /// <summary>
        /// Bac + ?
        /// </summary>
        public int BacPlus { get; set; } 

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

    }
}
