using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Matiere
    {

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid MATIERE_ID { get; set; }

        /// <summary>
        /// Nommination de la matiere
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Sigle de la matiere exp: Math 101
        /// </summary>
        public string SIGLE { get; set; }
         
        /// <summary>
        /// La classe de cette Matiere
        /// </summary>
        public Guid CLASSE_ID { get; set; }
         
        //public Guid FILIERE_ID { get; set; }

        //public int FILIERE_LEVEL { get; set; }

        //public string HEURE_PAR_SEMAINE { get; set; }

        /// <summary>
        /// Coeffiecient de cette Matiere
        /// </summary>
        public int COEFFICIENT { get; set; }
         
        /// <summary>
        /// La Couleur pour identifier rapide
        /// </summary>
        public string COULEUR { get; set; }

        /// <summary>
        /// DESCRIPTION
        /// </summary>
        public string DESCRIPTION { get; set; }

        
         
    }
}
