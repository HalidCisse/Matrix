using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities.Pedagogy
{
    /// <summary>
    /// Annee Scolaire ex : 2013-2014
    /// </summary>
    public class AnneeScolaire
    {

        /// <summary>
        /// 
        /// </summary>
        public AnneeScolaire()
        {
            DateCreated = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid AnneeScolaireGuid { get; set; }

        /// <summary>
        /// Ex: Spring 2013/2014
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Debut de l'Annee Scolaire
        /// </summary>
        public DateTime? DateDebut { get; set; }

        /// <summary>
        /// Fin de l'Annee Scolaire
        /// </summary>
        public DateTime? DateFin { get; set; }
        
        /// <summary>
        /// Date Ou L'Inscription Est Ouvert
        /// </summary>
        public DateTime? DateDebutInscription { get; set; }

        /// <summary>
        /// Date de la Fermeture de l'Inscription
        /// </summary>
        public DateTime? DateFinInscription { get; set; }
         
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date de Creation
        /// </summary>
        public DateTime? DateCreated { get; }

    }

}
