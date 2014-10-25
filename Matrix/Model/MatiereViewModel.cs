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
            MATIERES_MODEL_LIST = new List<MatiereModel>();
        }
        public string ANNEE_NAME { get; set; }       

        public List<MatiereModel> MATIERES_MODEL_LIST { get; set; }

        //public int INSTRUCTOR_COUNT { get; set; }
        
        //public int MATIERES_COUNT { get; set; }
        

    }   
}
