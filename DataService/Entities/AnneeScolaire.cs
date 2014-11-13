using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities
{
    class AnneeScolaire
    {
        [Key]
        public Guid ANNEE_SCOLAIRE_ID { get; set; }

        public string NAME { get; set; }

        /// <summary>
        /// Debut de l annee Scolaire
        /// </summary>
        public DateTime DATE_DEBUT { get; set; }

        /// <summary>
        /// Fin de l annee Scolaire
        /// </summary>
        public DateTime DATE_FIN { get; set; }
        
        public DateTime DATE_DEBUT_INSCRIPTION { get; set; }

        public DateTime DATE_FIN_INSCRIPTION { get; set; }
         
        public string DESCRIPTION { get; set; }

    }
}
