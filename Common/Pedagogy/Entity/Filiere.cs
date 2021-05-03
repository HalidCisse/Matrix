using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Pedagogy.Enums;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Filiere
    /// </summary>
    public class Filiere : Tracable   //todo add Type formation to AddFiliere
    {

        /// <summary>
        /// FiliereGuid
        /// </summary>
        [Key]
        public Guid FiliereGuid { get; set; }

        /// <summary>
        /// exp: Genie Informatique
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sigle de la Filiere exp: GI, TGC, TI, TE, SE
        /// </summary>
        public string Sigle { get; set; }

        /// <summary>
        /// Elementaire, fondamentale, lycee, college, professionnel, universitaire, continu
        /// </summary>
        public string TypeFormation { get; set; }

        /// <summary>
        /// primaire, secondaire, licence, master, doctorat, deug, TS
        /// </summary>
        public string Cycle { get; set; }

        /// <summary>
        /// exp: Licence -> Licence Professionelle -> Technicien -> Technicien Specialise -> Master -> Ingenieur -> Ingenieur Etat -> Doctorat
        /// </summary>
        public string Diplome { get; set; }

        /// <summary>
        /// Departement/Division de la Filiere
        /// </summary>
        public string Departement { get; set; }

        /// <summary>
        /// Le niveau Minimal pour l'admission a cette filiere
        /// </summary>
        public string Admission { get; set; }

        /// <summary>
        /// Le nombre de niveau
        /// </summary>
        public FiliereScolarite Scolarite { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Classe> Classes { get; set; }= new HashSet<Classe>();
    }
}
