using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Model;

namespace DataService.ViewModel
{
    public class FiliereClassCard
    {

        public FiliereClassCard ( Filiere Fil )
        {           
            FILIERE_NAME = Fil.NAME.ToUpper();

            CLASS_LIST = new List<ClassCard> ();
            GetCLASS_LIST (Fil.FILIERE_ID);           
        }
      
        public string FILIERE_NAME { get; set; }

        public List<ClassCard> CLASS_LIST { get; set; }      

        private void GetCLASS_LIST ( Guid FILIERE_ID )
        {
            using(var Db = new EF ())
            {
                Parallel.ForEach(Db.CLASSE.Where(C => C.FILIERE_ID == FILIERE_ID), CL =>
                {
                    CLASS_LIST.Add(new ClassCard(CL));
                });
                
                CLASS_LIST = CLASS_LIST.OrderBy(C => C.LEVEL).ToList();
            }
        }
       
       

    }
}
