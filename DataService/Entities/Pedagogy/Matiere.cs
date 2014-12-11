using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    public class Matiere
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid MatiereId { get; set; }

        /// <summary>
        /// Nommination de la matiere
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sigle de la matiere exp: Math 101
        /// </summary>
        public string Sigle { get; set; }
         
        /// <summary>
        /// La classe de cette Matiere
        /// </summary>
        public Guid ClasseId { get; set; }

        //public Guid FILIERE_ID { get; set; }

        //public int FILIERE_LEVEL { get; set; }

        //todo: Mass Horaire Matiere

        //public string HEURE_PAR_SEMAINE { get; set; }

        /// <summary>
        /// Coeffiecient de cette Matiere
        /// </summary>
        public int Coefficient { get; set; }
         
        /// <summary>
        /// La Couleur pour identifier rapide
        /// </summary>
        public string Couleur { get; set; }

        /// <summary>
        /// DESCRIPTION
        /// </summary>
        public string Description { get; set; }

        
         
    }
}
