using System;
using CLib;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;

namespace DataService.ViewModel.Pedagogy {

    /// <summary>
    /// Informations d'une inscription 
    /// </summary>
    public class InscriptionCard {

        /// <summary>
        /// Informations d'une inscription 
        /// </summary>
        public InscriptionCard (Enrollement enrollement)
        {
            InscriptionGuid   = enrollement.EnrollementGuid;
            InscriptionNum    ="Numéro Inscription  " + enrollement.EnrollementNum;
            Status            = enrollement.EnrollementStatus.GetEnumDescription();
            EnrollementStatus = enrollement.EnrollementStatus;
            DateInscription   ="Inscrit le  "+ enrollement.DateAdded.GetValueOrDefault().ToShortDateString();
            StudentFullName   = enrollement.Student.Person.FullName;
            PhotoIdentity     = enrollement.Student.Person.PhotoIdentity;
            ClasseDescription = enrollement.Classe.Sigle + "  -  " + enrollement.Classe.Filiere.Name;
            SchoolYearName    = enrollement.SchoolYear.Name;
            Session           = "Session " + enrollement.Classe.Session;
            Description       = enrollement.DateAdded.GetValueOrDefault().ToShortDateString();

            if (enrollement.EnrollementStatus == EnrollementStatus.Canceled ||
                enrollement.EnrollementStatus == EnrollementStatus.Failed)
                StatusColor = "Red";
            else if (enrollement.EnrollementStatus == EnrollementStatus.NotCompleted &&
                     enrollement.SchoolYear.DateFin < DateTime.Today)
                StatusColor = "Red";
            else
                StatusColor = "Blue";           
        }


        /// <summary>
        /// InscriptionGuid
        /// </summary>
        public Guid InscriptionGuid { get; }

        /// <summary>
        /// Guid de l'Etudiant
        /// </summary>
        public string StudentFullName { get; }

        /// <summary>
        /// PhotoIdentity
        /// </summary>
        public byte[] PhotoIdentity { get; }

        /// <summary>
        /// Classe Description classe code + filiere
        /// </summary>
        public string ClasseDescription { get;}

        /// <summary>
        /// Le Numero d'Inscription
        /// </summary>
        public string InscriptionNum { get; }

        /// <summary>
        /// Le status de l'Inscription
        /// </summary>
        public string Status { get;}

        /// <summary>
        /// Le status de l'Inscription
        /// </summary>
        public EnrollementStatus EnrollementStatus { get; }

        /// <summary>
        /// Le StatusColor de l'Inscription
        /// </summary>
        public string StatusColor { get; }

        /// <summary>
        /// SchoolYear
        /// </summary>
        public string SchoolYearName { get; }

        /// <summary>
        /// Session
        /// </summary>
        public string Session { get; }

        /// <summary>
        /// DateInscription
        /// </summary>
        public string DateInscription { get; }

        /// <summary>
        /// Details de l'Inscription
        /// </summary>
        public string Description { get; }

    }
}
