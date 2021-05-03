using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CLib;
using Common.Shared.Entity;
using DataService.Context;
using DataService.ViewModel;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion d'Etudiants
    /// </summary>
    public class StudentsManager
    {

        #region CRUD


        /// <summary>
        /// Ajouter Un Nouveau Etudiant
        /// </summary>
        /// <param name="myStudent"></param>
        /// <returns></returns>
        public bool AddStudent(Student myStudent)
        {
            //todo full validation

            using (var db = new SchoolContext())
            {
                if (myStudent.StudentGuid == Guid.Empty)
                    myStudent.StudentGuid= Guid.NewGuid();

                if(myStudent.Person.PersonGuid==Guid.Empty)
                    myStudent.Person.PersonGuid=Guid.NewGuid();

                if(myStudent.Guardian?.PersonGuid==Guid.Empty)
                    myStudent.Guardian.PersonGuid=Guid.NewGuid();

                myStudent.Person.DateAdded        =DateTime.Now;
                myStudent.Person.AddUserGuid      =Guid.Empty;
                myStudent.Person.LastEditUserGuid =Guid.Empty;
                myStudent.Person.LastEditDate     =DateTime.Now;

                if (myStudent.Guardian != null)
                {
                    myStudent.Guardian.DateAdded        =DateTime.Now;
                    myStudent.Guardian.AddUserGuid      =Guid.Empty;
                    myStudent.Guardian.LastEditUserGuid =Guid.Empty;
                    myStudent.Guardian.LastEditDate     =DateTime.Now;
                                        
                    db.Set<Person>().Add(myStudent.Guardian);
                }

                db.Set<Person>().Add(myStudent.Person);
                db.Students.Add(myStudent);              
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Modifier Un Etudiant
        /// </summary>
        /// <param name="myStudent"></param>
        /// <returns></returns>
        public bool UpdateStudent(Student myStudent)
        {
            using (var db = new SchoolContext())
            {
                db.Set<Person>().Attach(myStudent.Person);
                db.Entry(myStudent.Person).State=EntityState.Modified;

                db.Set<Person>().Attach(myStudent.Guardian);
                db.Entry(myStudent.Guardian).State=EntityState.Modified;

                db.Students.Attach(myStudent);
                db.Entry(myStudent).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Supprimer Un Etudiant
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        public bool DeleteStudent(Guid studentGuid)
        {
            using (var db = new SchoolContext()) {
                var theMan = db.Students.Find(studentGuid);

                if(theMan==null)
                    throw new InvalidOperationException("CAN_NOT_FIND_STUDENT_REFERENCE");

                theMan.Person.DeleteDate=DateTime.Now;
                theMan.Person.IsDeleted=true;
                theMan.Person.DeleteUserGuid=Guid.Empty;

                db.Students.Attach(theMan);
                db.Entry(theMan).State=EntityState.Modified;
                return db.SaveChanges()>0;
            }
        }


        #endregion




        #region Helpers



        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Student GetStudentById(string studentId)
        {
            using (var db = new SchoolContext())
            {
                return db.Students.First(s => s.Matricule == studentId);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        public Student GetStudentByGuid(Guid studentGuid)
        {
            using (var db = new SchoolContext())
                return db.Students.Include(s => s.Person).Include(s => s.Guardian).FirstOrDefault(s => s.StudentGuid == studentGuid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstAndLastName"></param>
        /// <returns></returns>
        public static Student GetStudentByFullName(string firstAndLastName)
        {
            using (var db = new SchoolContext())
            {             
                return db.Students.SingleOrDefault(s => s.Person.FirstName + s.Person.LastName == firstAndLastName);
            }
        }

        /// <summary>
        /// Nouveau Numero de matricule Unique
        /// </summary>       
        /// <returns>Renvoi un nouveau Numéro de matricule Unique</returns>
        public string NewMatricule () {
            string newId;

            do
                newId="E"+RandomHelper.GetRandLetters(1)+"-"+DateTime.Today.Month+DateTime.Today.Year.ToString().Substring(2)+"-"+RandomHelper.GetRandNum(4);
            while(
                     StudentExist(newId)
                  );

            return newId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public string GetStudentName(string studentId)
        {
            using (var db = new SchoolContext())
            {
                var myStudentName = db.Students.First(s => s.Matricule == studentId).Person.FirstName;
                return myStudentName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matricule"></param>
        /// <returns></returns>
        public bool StudentExist(string matricule)
        {
            using (var db = new SchoolContext())
                return db.Students.Any(s => s.Matricule.Equals(matricule, StringComparison.CurrentCultureIgnoreCase));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <returns></returns>
        public Guid GetStudentCurrentClasseGuid(Guid studentGuid)
        {
            using (var db = new SchoolContext())
            {
                var currentInscription = db.Enrollements.FirstOrDefault(i => i.SchoolYearGuid == PedagogyManager.StaticGetDefaultAnneeScolaireGuid && i.StudentGuid == studentGuid);

                return currentInscription?.ClasseGuid ?? Guid.Empty;
            }
        }


        #endregion





        #region Views



        /// <summary>
        /// Renvoi la list de Cards des etudiants de l'annee Scolaire Specifier
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentCard> GetStudentCards () {
            using (var db = new SchoolContext()) {
                //if(currentAnneeScolaireGuid==Guid.Empty)
                //    currentAnneeScolaireGuid=PedagogyManager.StaticGetDefaultAnneeScolaireGuid;
                //Guid currentAnneeScolaireGuid

                return db.Students.Where(s => !s.Person.IsDeleted)
                                  .OrderBy(s => s.Person.FirstName)
                                  .Include(s=> s.Person)
                                  .ToList()
                                  .Select(student => new StudentCard(student));
            }
        }


        /// <summary>
        /// Renvoi la list des Etudiants 
        /// </summary>
        /// <param name="searchString">Parametre de Recherche</param>
        /// <param name="maxResult">Nombre max de Resultat</param>
        /// <returns></returns>        
        public IEnumerable<SearchCard> Search(string searchString, int maxResult)
        {
                searchString = searchString.Trim();
                List<Guid> searchResult;

                if (!string.IsNullOrEmpty(searchString))

                using (var db = new SchoolContext())
                {
                    searchResult = db.Students.Where(s => (s.Person.FirstName + " " + s.Person.LastName).Contains(searchString) ||
                                                          (s.Person.LastName + " " + s.Person.FirstName).Contains(searchString) ||
                                                           s.Person.EmailAdress.Contains(searchString) ||
                                                           s.Matricule.Equals(searchString) 
                        ).Take(maxResult).Select(s => s.StudentGuid).ToList();
                }
               else
                {
                    using (var mc = new EconomatContext())
                        searchResult = mc.SchoolFees.Where(s => s.DueDate <= DateTime.Today && !s.IsPaid).Select(f => f.StudentGuid).Distinct().Take(30).ToList();
                    if (!searchResult.Any())
                        using (var db = new SchoolContext())
                            searchResult = db.Students.Take(maxResult).Select(s => s.StudentGuid).ToList();
                }

                var results = new HashSet<SearchCard>();

                foreach (var result in searchResult)
                    results.Add(new SearchCard(result));

                return results;
        }


        /// <summary>
        /// Renvoi la list des Etudiants 
        /// </summary>
        /// <param name="searchString">Parametre de Recherche</param>
        /// <returns></returns>        
        public IEnumerable<StudentCard> Search (string searchString) {
            searchString=searchString?.Trim();
           
            if(!string.IsNullOrEmpty(searchString))
                using (var db = new SchoolContext()) {
                    return db.Students.Where(s => (s.Person.FirstName+" "+s.Person.LastName).Contains(searchString)||
                                                        (s.Person.LastName+" "+s.Person.FirstName).Contains(searchString)||
                                                         s.Person.EmailAdress.Contains(searchString)||
                                                         s.Matricule.Equals(searchString)
                                     ).Take(7)
                                     .Include(s => s.Person)
                                     .ToList()
                                     .Select(s => new StudentCard(s));
                }
            using (var db = new SchoolContext())
                return db.Students.Include(s => s.Person)
                                  .ToList()
                                  .Select(s => new StudentCard(s));
        }


        #endregion






    }
}




//.Select(s=> new SearchCard(s.StudentGuid, thisYear));
//).Select(s => new {
//    s.StudentGuid,
//    s.PhotoIdentity,
//    s.FullName
//});
//new HashSet<SearchCard>(searchResult.Select(s => new SearchCard(s, thisYear)));