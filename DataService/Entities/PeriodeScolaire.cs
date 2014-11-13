using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Entities
{
    /// <summary>
    /// Ex : 1 ere Trimestre
    /// </summary>
    public class PeriodeScolaire
    {
        [Key]
        public Guid PERIODE_SCOLAIRE_ID { get; set; }

        public string NAME { get; set; }

        public Guid ANNEE_SCOLAIRE_ID { get; set; }

        public DateTime START_DATE { get; set; }



    }
}
