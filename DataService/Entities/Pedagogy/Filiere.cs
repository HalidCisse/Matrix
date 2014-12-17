using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// 
    /// </summary>
    public class Filiere
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid FiliereGuid { get; set; }

        /// <summary>
        /// exp: Genie Informatique
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// exp: Licence -> Licence Professionelle -> Technicien -> Technicien Specialise -> Master -> Ingenieur -> Ingenieur Etat -> Doctorat
        /// </summary>
        public string Niveau { get; set; }

        //todo:Filiere Departement => Staff Departement
        //public string DEPARTEMENT { get; set; }

        /// <summary>
        /// Le niveau Minimal pour l'admission a cette filiere
        /// </summary>
        public string NiveauEntree { get; set; }

        /// <summary>
        /// Le nombre de niveau
        /// </summary>
        public int NAnnee { get; set; }  // 5 ans

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }
}
