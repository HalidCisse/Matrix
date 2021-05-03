using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using Common.Pedagogy.Enums;
using Common.Shared.Entity;

namespace Common.Pedagogy.Entity
{
    /// <summary>
    /// Represente un cours
    /// </summary>
    public class Study:  Tracable
    {
        /// <summary>
        /// ID du Cours
        /// </summary>
        [Key]
        public Guid StudyGuid { get; set; }

        /// <summary>
        /// ID de la classe
        /// </summary>       
        public Guid ClasseGuid { get; set; }

        /// <summary>
        /// ID du staff qui va dispenser le cours
        /// </summary>      
        public Guid ProffGuid { get; set; }

        /// <summary>
        /// ID du staff qui va corriger le cours
        /// </summary>      
        public Guid? GraderGuid { get; set; }

        /// <summary>
        /// Supervisor
        /// </summary>      
        public Guid? SupervisorGuid { get; set; }

        /// <summary>
        /// ID du matiere qui sera enseigner
        /// </summary>       
        public Guid SubjectGuid { get; set; }

        /// <summary>
        /// La salle ou le cours sera dispenser
        /// </summary>
        public string Room { get; set; }
           
        /// <summary>
        /// La recurrence du cours
        /// </summary>
        public string RecurrenceDays { get; set; }

        /// <summary>
        /// Heure de debut du cours
        /// </summary>
        public TimeSpan StartTime { get; set; }
         
        /// <summary>
        /// Heure de fin du cours
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Debut de la periode du cours
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Fin de la periode du cours
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Control, Cours, Examen, Test, Revision, Travaux Pratiques, Travaux Dirigés
        /// </summary>
        public CoursTypes Type { get; set; }              

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }



        /// <summary>
        /// Enseignant
        /// </summary>
        [ForeignKey("ProffGuid")]
        public virtual Staff Proff { get; set; }
        /// <summary>
        /// Correcteur
        /// </summary>
        [ForeignKey("GraderGuid")]
        public virtual Staff Grader { get; set; }
        /// <summary>
        /// Surveillant
        /// </summary>
        [ForeignKey("SupervisorGuid")]
        public virtual Staff Supervisor { get; set; }
        /// <summary>
        /// La matiere Enseigner
        /// </summary>
        [ForeignKey("SubjectGuid")]
        public virtual Subject Subject { get; set; }       
        /// <summary>
        /// Le groupe d'etudiant
        /// </summary>
        [ForeignKey("ClasseGuid")]
        public virtual Classe Classe { get; set; }
        /// <summary>
        /// Les corrections si c'est un Control
        /// </summary>
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }=new HashSet<StudentGrade>();
        /// <summary>
        /// Les jours d'Exception de ce cours
        /// </summary>
        public virtual ICollection<StudyException> Exceptions { get; set; }=new HashSet<StudyException>();
    }
}
