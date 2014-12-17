using System;
using System.Linq;
using DataService.Context;
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
            FiliereGuid = fl.FiliereGuid;
            Name = fl.Name.ToUpper();
            Niveau = fl.Niveau;
            NiveauEntree = fl.NiveauEntree;
            NAnnee = fl.NAnnee;
            GetCLASSES_COUNT();
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid FiliereGuid { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Niveau { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NiveauEntree { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NAnnee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StaffsCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StudentsCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ClassesCount { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public int MatieresCount { get; set; }



        #region HELPERS

        private void GetCLASSES_COUNT()
        {
            using (var db = new Ef())
            {
                ClassesCount = db.Classe.Count(c => c.FiliereGuid == FiliereGuid);        
            }
        }

        #endregion

    }
}
