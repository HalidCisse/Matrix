using System;
using Common.Pedagogy.Entity;
using DataService.DataManager;

namespace DataService.ViewModel.Pedagogy {

    /// <summary>
    /// Matiere informations
    /// </summary>
    public class SubjectCard {


        /// <summary>
        /// Matiere informations
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="staffGuid"></param>
        public SubjectCard (Subject subject, Guid staffGuid)
        {
            SubjectGuid = subject.SubjectGuid;
            FullName = subject.Name + " (" + subject.Sigle + ")";
            HourlyPay = subject.HourlyPay.ToString("0.##");
            Coefficient = subject.Coefficient;
            Color= subject.Couleur;
            Module= subject.Module + " - " + subject.StudyLanguage;
            Description = "Coeff " + subject.Coefficient + "  -  TH " + subject.HourlyPay.ToString("0.##") + " dhs";

            IsSpecialty = SubjectsManager.StaticIsSpecialty(staffGuid, subject.SubjectGuid);
        }

        /// <summary>
        /// Matiere informations
        /// </summary>
        /// <param name="subject"></param>
        public SubjectCard (Subject subject) {
            SubjectGuid=subject.SubjectGuid;
            FullName=subject.Name+" ("+subject.Sigle+")";
            HourlyPay=subject.HourlyPay.ToString("0.##");
            Coefficient=subject.Coefficient;
            Color=subject.Couleur;
            Module=subject.Module+" - "+subject.StudyLanguage;
            Description="Coeff "+subject.Coefficient+"  -  TH "+subject.HourlyPay.ToString("0.##")+" dhs";
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid SubjectGuid { get; }

        /// <summary>
        /// Nommination de la matiere
        /// </summary>
        public string FullName { get; }               
        
        /// <summary>
        /// Salaire par heure pour un instructeur
        /// </summary>
        public string HourlyPay { get;}

        /// <summary>
        /// Coeffiecient de cette Matiere
        /// </summary>
        public int Coefficient { get;  }

        /// <summary>
        /// La Couleur 
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// DESCRIPTION
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Module
        /// </summary>
        public string Module { get; }

        /// <summary>
        /// IsSpecialty
        /// </summary>
        public bool IsSpecialty { get; }

    }
}
