using System;
using System.Linq;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;

namespace DataService.ViewModel
{
    /// <summary>
    /// Model de Filiere
    /// </summary>
    public class FiliereCard
    {
        /// <summary>
        /// Model de Filiere
        /// </summary>
        public FiliereCard (Filiere fl)
        {
            FiliereId = fl.FiliereId;
            Name = fl.Name.ToUpper();
            Niveau = fl.Niveau;
            NiveauEntree = fl.NiveauEntree;
            NAnnee = fl.NAnnee;
            GetCLASSES_COUNT();
        }

        public Guid FiliereId { get; set; } 

        public string Name { get; set; }

        public string Niveau { get; set; }

        public string NiveauEntree { get; set; }

        public int NAnnee { get; set; }

        public int StaffsCount { get; set; }

        public int StudentsCount { get; set; }

        public int ClassesCount { get; set; }
       
        public int MatieresCount { get; set; }



        #region HELPERS

        private void GetCLASSES_COUNT()
        {
            using (var db = new Ef())
            {
                ClassesCount = db.Classe.Count(c => c.FiliereId == FiliereId);        
            }
        }

        #endregion

    }
}
