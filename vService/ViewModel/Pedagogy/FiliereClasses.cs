using System.Collections.Generic;
using System.Linq;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy {

    /// <summary>
    /// 
    /// </summary>
    public class FiliereClasses {


        internal FiliereClasses(Filiere filiere)
        {
            FiliereName=filiere.Name.ToUpper();

            using (var db = new SchoolContext())
                ClassesList =
                    db.Classes.Where(c => c.FiliereGuid == filiere.FiliereGuid && !c.IsDeleted)
                        .OrderBy(c => c.ClassGrade).ToList();
        }


        /// <summary>
        /// Classes List
        /// </summary>
        public List<Classe> ClassesList { get; }


        /// <summary>
        /// FiliereName
        /// </summary>
        public string FiliereName { get; set; }


    }
}
