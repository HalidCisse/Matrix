using System;
using System.Linq;
using CLib;
using Common.Pedagogy.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
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
            Niveau = fl.Diplome;
            NiveauEntree = fl.Admission;
            Scolarite = fl.Scolarite.GetEnumDescription();
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
        public string Scolarite { get; set; }

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
            using (var db = new SchoolContext())
            {
                ClassesCount = db.Classes.Count(c => c.FiliereGuid == FiliereGuid);        
            }
        }

        #endregion

    }
}
