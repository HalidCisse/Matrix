using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;

namespace DataService.ViewModel
{
    /// <summary>
    /// Model d'une filiere et une list de ses classes
    /// </summary>
    public class FiliereClassCard
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fil"></param>
        public FiliereClassCard ( Filiere fil )
        {           
            FiliereName = fil.Name.ToUpper();

            ClassList = new List<ClassCard> ();
            GetCLASS_LIST (fil.FiliereId);           
        }
      
        /// <summary>
        /// 
        /// </summary>
        public string FiliereName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ClassCard> ClassList { get; set; }      

        private void GetCLASS_LIST ( Guid filiereId )
        {
            using(var db = new Ef ())
            {
                Parallel.ForEach(db.Classe.Where(c => c.FiliereId == filiereId), cl =>
                {
                    ClassList.Add(new ClassCard(cl));
                });

                if (!ClassList.Any()) return;
                ClassList = ClassList.OrderBy(c => c.Level).ToList();
            }
        }
       
       

    }
}
