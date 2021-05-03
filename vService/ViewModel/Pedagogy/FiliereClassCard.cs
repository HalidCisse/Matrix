using System.Collections.Generic;
using System.Linq;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
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
            
            using (var db = new SchoolContext())
            {
                //Parallel.ForEach(db.Classe.Where(c => c.FiliereGuid == fil.FiliereGuid), cl =>
                //{
                //    ClassList.Add(new ClassCard(cl));                    
                //});


                foreach (var cl in db.Classes.Where(c => c.FiliereGuid == fil.FiliereGuid).OrderBy(c => c.ClassGrade))
                    ClassList.Add(new ClassCard(cl));

                if (!ClassList.Any()) return;
                ClassList = ClassList.OrderBy(c => c.Level).ToList();
            }
           
        }
      
        /// <summary>
        /// 
        /// </summary>
        public string FiliereName { get;}

        /// <summary>
        /// 
        /// </summary>
        public List<ClassCard> ClassList { get; set; } = new List<ClassCard> ();

       
       
       

    }
}
