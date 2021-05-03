using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Shared.Entity
{
    /// <summary>
    /// Etablissement
    /// </summary>
    public class Etablissement
    {
        /// <summary>
        /// Etablissement
        /// </summary>
        [Key]
        public Guid EtablissementId { get; set; }

        /// <summary>
        /// Nom de l'ecole
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public byte[] Logo { get; set; } 

        /// <summary>
        /// Pays
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Adresse Domicile
        /// </summary>
        public string Adresse { get; set; }

        /// <summary>
        /// Numero de Telephone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Numero de fax
        /// </summary>
        public string Fax { get; set; }
         
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

    }
}
