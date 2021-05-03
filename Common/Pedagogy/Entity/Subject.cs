using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CLib.Database;
using Common.Shared.Entity;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    ///  Matiere/Module
    /// </summary>
    public class Subject: Tracable
    {
        
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid SubjectGuid { get; set; }

        /// <summary>
        /// Nommination de la matiere
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sigle/Code de la matiere exp: Math 101
        /// </summary>
        public string Sigle { get; set; }

        /// <summary>
        /// Discipline de la matiere exp: Mathematique/LV1 / Attelier
        /// </summary>
        public string Discipline { get; set; }

        /// <summary>
        /// Primaire/Secondaire
        /// </summary>
        public string TypeMatiere { get; set; }

        /// <summary>
        /// Module
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Langue d'Enseignement
        /// </summary>
        public string StudyLanguage { get; set; }

        /// <summary>
        /// Masse Horaire
        /// </summary>
        public TimeSpan WeeklyHours { get; set; }

        /// <summary>
        /// TempsPrescrit
        /// </summary>
        public TimeSpan TempsPrescrit { get; set; }

        /// <summary>
        /// Salaire par heure pour un instructeur
        /// </summary>
        public double HourlyPay { get; set; }

        /// <summary>
        /// Coeffiecient de cette Matiere
        /// </summary>
        public int Coefficient { get; set; }

        /// <summary>
        /// La Couleur 
        /// </summary>
        public string Couleur { get; set; }

        /// <summary>
        /// DESCRIPTION
        /// </summary>
        public string Description { get; set; }



        /// <summary>
        /// List des staffs qui enseigne cette matiere
        /// </summary>
        public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Study> Cours { get; set; }= new HashSet<Study>();
        ///// <summary>
        ///// 
        ///// </summary>
        //public virtual ICollection<Classe> Classes { get; set; }= new HashSet<Classe>();
    }
}
