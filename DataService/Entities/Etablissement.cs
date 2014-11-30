using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
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
        public Guid ETABLISSEMENT_ID { get; set; }

        /// <summary>
        /// Nom de l'ecole
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public byte[] LOGO { get; set; } 

        /// <summary>
        /// Pays
        /// </summary>
        public string COUNTRY { get; set; }

        /// <summary>
        /// Adresse Domicile
        /// </summary>
        public string ADRESSE { get; set; }

        /// <summary>
        /// Numero de Telephone
        /// </summary>
        public string PHONE { get; set; }

        /// <summary>
        /// Numero de fax
        /// </summary>
        public string FAX { get; set; }
         
        /// <summary>
        /// Description
        /// </summary>
        public string DESCRIPTION { get; set; }

    }
}
