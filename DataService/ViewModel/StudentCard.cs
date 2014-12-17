using System;
using System.Linq;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;

namespace DataService.ViewModel
{
    /// <summary>
    /// Represent un Model de Donnée d'un Etudiant
    /// </summary>
    public class StudentCard
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public StudentCard( Student currentStudent, AnneeScolaire currentAnneeScolaire )
        {
            StudentGuid = currentStudent.StudentGuid;
            Title = currentStudent.Title;
            Firstname = currentStudent.FirstName;
            Lastname = currentStudent.LastName;
            PhotoIdentity = currentStudent.PhotoIdentity;
            PhoneNumber = currentStudent.PhoneNumber;
            EmailAdress = currentStudent.EmailAdress;
            HomeAdress = currentStudent.HomeAdress;

            using (var db = new Ef())
            {                
                var currentInscription = db.Inscription.First(i => i.AnneeScolaireGuid == currentAnneeScolaire.AnneeScolaireGuid);

                if (currentInscription != null)
                {
                    var currentClasse = db.Classe.Find(currentInscription.ClasseGuid);

                    CurrentClasseLevel = currentClasse.Name;
                    CurrentFiliere = db.Filiere.Find(currentClasse.FiliereGuid).Name;
                }
                else
                {
                    CurrentClasseLevel = "Non Inscrit";
                    CurrentFiliere = "Non Inscrit";
                }                              
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid StudentGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] PhotoIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HomeAdress { get; set; }       
      
        /// <summary>
        /// Renvoi le nom complet exp: Halid Cisse
        /// </summary>
        public string FullName => Firstname + " " + Lastname;

        /// <summary>
        /// Renvoi le La Filiere Du l'étudiant
        /// </summary>
        public string CurrentFiliere;

        /// <summary>
        /// Renvoi la Niveau de sa Classe Actuelle
        /// </summary>
        public string CurrentClasseLevel;

        

    }
}












//Nationality = currentStudent.Nationality;

//IdentityNumber = currentStudent.IdentityNumber;
//BirthDate = currentStudent.BirthDate;

//RegistrationDate = currentStudent.RegistrationDate;
//Statut = currentStudent.Statut;



// public DateTime? RegistrationDate { get; set; }

//  public string Statut { get; set; }

// public string Nationality { get; set; }

// public string IdentityNumber { get; set; }

// public DateTime? BirthDate { get; set; }

// public string BirthPlace { get; set; }