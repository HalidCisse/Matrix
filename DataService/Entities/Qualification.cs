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
        [Key]
        public Guid QUALIFICATION_ID { get; set; }

        public string NIVEAU { get; set; } // Technicien Sup

        public string FILIERE_ID { get; set; } // -> Developpement Informatique

        public string ETABLISSEMENT { get; set; } // Miage Rabat
                  
        public int BAC_PLUS { get; set; } // +2

        public string DESCRIPTION { get; set; }

    }
}
