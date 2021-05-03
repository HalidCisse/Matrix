using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLib.Database;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Annee Scolaire ex : 2013-2014
    /// </summary>
    public class SchoolYear: Tracable
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid SchoolYearGuid { get; set; }


        /// <summary>
        /// Ex: Année Scolaire 2013/2014 - Session Soir
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Ex: Session Spring, Session Printemps , Session Jour, Session Soir
        /// </summary>
        public string Session { get; set; }


        /// <summary>
        /// Barem pour la Generation des Notes Ex: 20, sur 100
        /// </summary>
        public double BaremDesNotes { get; set; }


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
        /// 
        /// </summary>
        public virtual ICollection<Enrollement> Inscriptions { get; set; } = new HashSet<Enrollement>();

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<SchoolPeriod> PeriodeScolaires { get; set; } = new HashSet<SchoolPeriod>();

    }

}
