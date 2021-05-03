using System;
using System.Linq;
using Common.Shared.Entity;
using DataService.Context;

namespace DataService.ViewModel
{
    /// <summary>
    /// Represent un Model de Donnée d'un Etudiant
    /// </summary>
    public class StudentCard
    {        

        /// <summary>
        /// Represent un Model de Donnée d'un Etudiant
        /// </summary>
        public StudentCard( Student currentStudent, Guid currentAnneeScolaireGuid )
        {
            StudentGuid   = currentStudent.StudentGuid;
            Title         = currentStudent.Person.Title.ToString();
            FirstName     = currentStudent.Person.FirstName;
            LastName      = currentStudent.Person.LastName;
            PhotoIdentity = currentStudent.Person.PhotoIdentity;
            PhoneNumber   = currentStudent.Person.PhoneNumber;
            EmailAdress   = currentStudent.Person.EmailAdress;
            HomeAdress    = currentStudent.Person.HomeAdress;
            PersonGuid    = currentStudent.Person.PersonGuid;

            using (var db = new SchoolContext())
            {                
                var currentInscription = db.Enrollements.FirstOrDefault(i => i.SchoolYearGuid == currentAnneeScolaireGuid && i.StudentGuid == currentStudent.StudentGuid);

                if (currentInscription != null)
                {
                    var currentClasse  = db.Classes.Find(currentInscription.ClasseGuid);

                    CurrentClasseLevel = currentClasse.Name;
                    CurrentFiliere     = db.Filieres.Find(currentClasse.FiliereGuid).Name;
                }
                else
                {
                    CurrentClasseLevel = "Non Inscrit";
                    CurrentFiliere     = "Non Inscrit";
                }                              
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentStudent"></param>
        public StudentCard (Student currentStudent) {
            StudentGuid   =currentStudent.StudentGuid;
            Title         =currentStudent.Person.Title.ToString();
            FirstName     =currentStudent.Person.FirstName;
            LastName      =currentStudent.Person.LastName;
            PhotoIdentity =currentStudent.Person.PhotoIdentity;
            PhoneNumber   =currentStudent.Person.PhoneNumber;
            EmailAdress   =currentStudent.Person.EmailAdress;
            HomeAdress    =currentStudent.Person.HomeAdress;
            PersonGuid    =currentStudent.Person.PersonGuid;

            using (var db = new SchoolContext()) {
                var currentInscription = db.Enrollements.Where(e=> e.StudentGuid ==currentStudent.StudentGuid).OrderByDescending(e=> e.DateAdded).FirstOrDefault();

                if(currentInscription!=null) {
                    //var currentClasse = db.Classes.Find(currentInscription.ClasseGuid);

                    CurrentClasseLevel=currentInscription.Classe.Sigle;
                    CurrentFiliere = currentInscription.Classe.Filiere.Sigle;
                        // db.Filieres.Find(currentClasse.FiliereGuid).Name;
                }
                else {
                    CurrentClasseLevel="Non Inscrit";
                    CurrentFiliere="Non Inscrit";
                }
            }
        }



        /// <summary>
        /// StudentGuid
        /// </summary>
        public Guid StudentGuid { get; }

        /// <summary>
        /// PersonGuid
        /// </summary>
        public Guid PersonGuid { get; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get;  }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get;  }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get;  }

        /// <summary>
        /// PhotoIdentity
        /// </summary>
        public byte[] PhotoIdentity { get;  }

        /// <summary>
        /// PhoneNumber
        /// </summary>
        public string PhoneNumber { get;  }

        /// <summary>
        /// EmailAdress
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// HomeAdress
        /// </summary>
        public string HomeAdress { get;  }       
      
        /// <summary>
        /// Renvoi le nom complet exp: Halid Cisse
        /// </summary>
        public string FullName => FirstName + " " + LastName;

        /// <summary>
        /// Renvoi le La Filiere Du l'étudiant
        /// </summary>
        public string CurrentFiliere { get; }

        /// <summary>
        /// Renvoi la Niveau de sa Classe Actuelle
        /// </summary>
        public string CurrentClasseLevel { get; }


    }
}










