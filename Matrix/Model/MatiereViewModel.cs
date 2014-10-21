using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entities;

namespace Matrix.Model
{
    public class MatiereViewModel
    {

        public MatiereViewModel ( )
        {
            MATIERES_LIST = new List<Matiere> ();
        }
        public string ANNEE_NAME { get; set; }

        public List<Matiere> MATIERES_LIST { get; set; }

        public int MATIERES_COUNT { get; set; }
        

    }   
}
