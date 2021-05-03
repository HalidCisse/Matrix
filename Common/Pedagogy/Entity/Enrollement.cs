using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using Common.Economat.Enums;
using Common.Pedagogy.Enums;
using Common.Shared.Entity;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Inscription
    /// </summary>
    public class Enrollement: Tracable
    {

        /// <summary>
        /// Guid
        /// </summary>
        [Key]
        public Guid EnrollementGuid { get; set; } 

        /// <summary>
        /// Guid de l'Etudiant
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// ID de la Classe
        /// </summary>
        public Guid ClasseGuid { get; set; }

        /// <summary>
        /// L'Annee Scolaire de l'Inscription
        /// </summary>
        public Guid SchoolYearGuid { get; set; }

        /// <summary>
        /// Le Numero d'Inscription
        /// </summary>
        public string EnrollementNum { get; set; }

        /// <summary>
        /// Le status de l'Inscription
        /// </summary>
        public EnrollementStatus EnrollementStatus { get; set; }

        /// <summary>
        /// Details de l'Inscription
        /// </summary>
        public string Description { get; set; }


        #region ECONOMAT


        /// <summary>
        /// Determine si l'Etudiants est Boursier
        /// </summary>
        public bool IsScholar { get; set; }


        /// <summary>
        /// Type de Reglement des frais d'etudes
        /// </summary>
        public InstallmentRecurrence InstallmentRecurrence { get; set; }


        /// <summary>
        /// Somme a Payer A l'Inscription si est Boursier
        /// </summary>
        public double InscriptionAmount { get; set; }


        /// <summary>
        /// Somme a Payer par tranche si est boursier
        /// </summary>
        public double InstallementAmount { get; set; }


        #endregion



        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("StudentGuid")]
        public virtual Student Student { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("ClasseGuid")]
        public virtual Classe Classe { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("SchoolYearGuid")]
        public virtual SchoolYear SchoolYear { get; set; }
    }
}
