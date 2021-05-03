using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using Common.Pedagogy.Enums;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Classe : Tracable
    {
        /// <summary>
        /// ClasseGuid
        /// </summary>
        [Key]
        public Guid ClasseGuid { get; set; }


        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Sigle
        /// </summary>
        public string Sigle { get; set; }


        /// <summary>
        /// Session de cette Classe
        /// </summary>
        public string Session { get; set; }


        /// <summary>
        /// Effectif Maximal d'Inscription Par Session
        /// </summary>
        public int MaxEffectif { get; set; }


        /// <summary>
        /// Guid de la Filiere 
        /// </summary>
        public Guid FiliereGuid { get; set; }


        /// <summary>
        /// Level
        /// </summary>
        public ClassGrades ClassGrade { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }


        #region ECONOMAT


        /// <summary>
        /// Somme a Payer A l'Inscription
        /// </summary>
        public double InscriptionAmount { get; set; }


        /// <summary>
        /// Somme a Payer Par Tranche pour un Installement Mensuel
        /// </summary>
        public double MonthlyAmount { get; set; }


        /// <summary>
        /// Somme a Payer Par Tranche pour un Installement Trimestriel
        /// </summary>
        public double QuarterlyAmount { get; set; }


        /// <summary>
        /// Somme a Payer Par Tranche pour un Installement Semestriel
        /// </summary>
        public double HalfYearlyAmount { get; set; }


        /// <summary>
        /// Somme a Payer Par Tranche pour un Installement Annuel
        /// </summary>
        public double YearlyAmount { get; set; }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("FiliereGuid")]
        public virtual Filiere Filiere { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Study> Cours { get; set; }=new HashSet<Study>();
        
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Enrollement> Inscriptions { get; set; }=new HashSet<Enrollement>();

        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<Subject> Matieres { get; set; }=new HashSet<Subject>();

    }
}
