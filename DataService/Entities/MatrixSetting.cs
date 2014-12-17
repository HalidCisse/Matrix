using System;
using System.ComponentModel.DataAnnotations;
using DataService.Enum;

namespace DataService.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class MatrixSetting
    {
        /// <summary>
        /// ID Du System
        /// </summary> 
        [Key]
        public Guid SysGuid { get; set; } = MatrixConstants.SystemGuid();

        /// <summary>
        /// Le Guid de l'annee Scolaire Actuelle
        /// </summary>
        public Guid CurrentAnneeScolaireGuid { get; set; }

        /// <summary>
        /// Le Guid de de la Periode l'annee Scolaire Actuelle
        /// </summary>
        public Guid CurrentPeriodeScolaireGuid { get; set; }

        /// <summary>
        /// Le nom de L'Etablissement
        /// </summary>
        public string EtablissementName { get; set; }

        /// <summary>
        /// Le Telephone de L'Etablissement
        /// </summary>
        public string EtablissementTel { get; set; }

        /// <summary>
        /// Le Fax de L'Etablissement
        /// </summary>
        public string EtablissementFax { get; set; }

        /// <summary>
        /// Le Logo de L'Etablissement
        /// </summary>
        public byte[] EtablissementLogo { get; set; }

        /// <summary>
        /// Le pays de L'Etablissement
        /// </summary>
        public string EtablissementCountry { get; set; }

        /// <summary>
        /// La Ville de L'Etablissement
        /// </summary>
        public string EtablissementCity { get; set; }

        /// <summary>
        /// L'Adresse de L'Etablissement
        /// </summary>
        public string EtablissementAdress { get; set; }



    }
}
