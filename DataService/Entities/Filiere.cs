using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere
    {

        [Key]
        public Guid FILIERE_ID { get; set; }
        /// <summary>
        /// exp: Genie Informatique
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// exp: Licence -> Licence Professionelle -> Technicien -> Technicien Specialise -> Master -> Ingenieur -> Ingenieur Etat -> Doctorat
        /// </summary>
        public string NIVEAU { get; set; }

        //todo:Filiere Departement => Staff Departement
        //public string DEPARTEMENT { get; set; }

        /// <summary>
        /// Le niveau Minimal pour l'admission a cette filiere
        /// </summary>
        public string NIVEAU_ENTREE { get; set; }

        /// <summary>
        /// Le nombre de niveau
        /// </summary>
        public int N_ANNEE { get; set; }  // 5 ans

        /// <summary>
        /// Description
        /// </summary>
        public string DESCRIPTION { get; set; }
    }
}
