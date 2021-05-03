using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CLib;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using Common.Shared.Entity;
using DataService.Context;
using DataService.Helpers;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion Des Inscriptions
    /// </summary>
    public sealed class EnrollementManager {

        /// <summary>
        /// Inscrire Un Etudiant A une Classe
        /// </summary>      
        /// <param name="myEnrollement">Objet Inscription</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>True si succes</returns>
        public bool EnrollStudent(Enrollement myEnrollement)
        {
            using (var db = new SchoolContext())
            {
                var classe = db.Classes.FirstOrDefault(c=> c.ClasseGuid==myEnrollement.ClasseGuid && !c.IsDeleted);
                if (classe == null)                                             throw new InvalidOperationException("CLASSE_REFERENCE_NOT_FOUND");

                var anneeScolaireGuid                                           = PedagogyManager.StaticGetSessionAnneeScolaireGuid(classe.Session);

                if (anneeScolaireGuid != null) myEnrollement.SchoolYearGuid  = (Guid) anneeScolaireGuid;
                else                                                            throw new InvalidOperationException("ANNEE_SCOLAIRE_SESSION_NOT_ACTIVE");

                if (db.Students.Find(myEnrollement.StudentGuid) == null)         throw new InvalidOperationException("STUDENT_REFERENCE_NOT_FOUND");
                if (InscExist(myEnrollement.EnrollementNum))                     throw new InvalidOperationException("INSCRIPTION_ID_ALREADY_EXIST");
                if (myEnrollement.InscriptionAmount < 0)                        throw new InvalidOperationException("INSCRIPTION_AMOUNT_CAN_NOT_BE_NEGATIF");
                if (myEnrollement.InstallementAmount < 0)                       throw new InvalidOperationException("INSTALLEMENT_AMOUNT_CAN_NOT_BE_NEGATIF");


                if (myEnrollement.EnrollementGuid == Guid.Empty) myEnrollement.EnrollementGuid = Guid.NewGuid();

                myEnrollement.DateAdded        = DateTime.Now;
                myEnrollement.LastEditDate     = DateTime.Now;
                myEnrollement.LastEditUserGuid = Guid.Empty;
                myEnrollement.AddUserGuid      = Guid.Empty;

                db.Enrollements.Add(myEnrollement);

                if (db.SaveChanges() <= 0)
                    return false;

                foreach (var recue in SchoolFeeHelper.GenerateInscriptionReceipts(myEnrollement))
                    StudentsFinanceManager.StaticAddFeeReceipt(recue);
                return true; 
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myEnrollement"></param>
        /// <returns></returns>
        public bool UpdateEnrollement (Enrollement myEnrollement)
        {
            //if (!PedagogyManager.AnneeScolaireExist(myInscription.AnneeScolaireGuid)) throw new NotValidDataException("ANNEE_SCOLAIRE_N_EXIST_PAS");

            using (var db = new SchoolContext())
            {
                db.Enrollements.Attach(myEnrollement);
                db.Entry(myEnrollement).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Modifier le Statur d'une Inscription
        /// </summary>
        /// <param name="inscriptionGuid"></param>
        /// <param name="newStatut"></param>
        /// <returns></returns>
        public bool ChangeStatut (Guid inscriptionGuid, EnrollementStatus newStatut) {           
            using (var db = new SchoolContext())
            {
                var oldIns = db.Enrollements.Find(inscriptionGuid);
                if (oldIns==null) throw new InvalidOperationException("ENROLLEMENT_REFERENCE_NOT_FOUND");

                oldIns.EnrollementStatus = newStatut;

                oldIns.LastEditDate = DateTime.UtcNow;
                oldIns.LastEditUserGuid = Guid.Empty;

                db.Enrollements.Attach(oldIns);
                db.Entry(oldIns).State=EntityState.Modified;
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteInscription(Guid inscriptionGuid)
        {
            using (var db = new SchoolContext())
            {
                db.Enrollements.Remove(db.Enrollements.Find(inscriptionGuid));
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inscriptionGuid"></param>
        /// <returns></returns>
        public Enrollement GetInscriptionByGuid(Guid inscriptionGuid)
        {
            using (var db = new SchoolContext())
            {
                return db.Enrollements.Find(inscriptionGuid);
            }
        }


        /// <summary>
        /// Renvoi l'inscription de ce numero
        /// </summary>
        /// <param name="inscriptionId"></param>
        /// <returns></returns>
        public Enrollement GetInscriptionById(string inscriptionId)
        {
            using (var db = new SchoolContext())
            {
                return db.Enrollements.FirstOrDefault(i=> i.EnrollementNum.Equals(inscriptionId, StringComparison.CurrentCultureIgnoreCase) );
            }
        }


        /// <summary>
        /// Renvoi l'Etudiant de cette Inscription
        /// </summary>
        /// <param name="enrollementNum"></param>
        /// <returns></returns>
        public Student GetInscriptionStudent(string enrollementNum)
        {
            using (var db = new SchoolContext())
            {
                //var stdGuid = GetInscriptionById(inscriptionId)?.StudentGuid;

                //return db.Students.FirstOrDefault(s=> s.StudentGuid == stdGuid);

                return
                    db.Enrollements.Include(i=> i.Student.Person).FirstOrDefault(
                        i => i.EnrollementNum.Equals(enrollementNum, StringComparison.CurrentCultureIgnoreCase))?.Student;                
            }
        }


        /// <summary>
        /// Renvoi l'Etudiant de cette Inscription
        /// </summary>
        /// <param name="inscriptionGuid"></param>
        /// <returns></returns>
        public Student GetInscriptionStudent(Guid inscriptionGuid)
        {
            using (var db = new SchoolContext())
            {
                var stdGuid = GetInscriptionByGuid(inscriptionGuid)?.StudentGuid;

                return db.Students.Find(stdGuid);
            }
        }


        /// <summary>
        /// Return true si l'Etudiant est Inscrit
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsStudentInsc( Guid studentGuid, Guid currentAnneeScolaireGuid)
        {
            using (var db = new SchoolContext())
            {
                return db.Enrollements.Any(i => i.SchoolYearGuid == currentAnneeScolaireGuid && i.StudentGuid == studentGuid);
            }            
        }


        /// <summary>
        /// Renvoi La List des Etudiants Non Inscrit
        /// </summary>
        /// <returns></returns>
        public HashSet<Student> GetStudentsNotIns()
        {
            //using (var db = new SchoolContext())
            //{
            //    var students = new HashSet<Student>();

            //    foreach (var std in db.Student)
            //    {
            //        if (!IsStudentInsc(std.StudentGuid, currentAnneeScolaireGuid)) students.Add(std);                    
            //    }

            //    return students;
            //}

            using (var db = new SchoolContext())
            {
                var students = new HashSet<Student>(db.Students.Include(s=> s.Person));
                var ans = db.SchoolYears.Where(a => a.DateDebut <= DateTime.Today && a.DateFin >= DateTime.Today).Select(a=> a.SchoolYearGuid).ToList();

                foreach (
                    var std in from std in students.ToList() from an in ans where IsStudentInsc(std.StudentGuid, an) select std)
                    students.Remove(std);

                return students;
            }
        }


        /// <summary>
        /// Verifier si ce ID est Deja Utilisee
        /// </summary>
        /// <param name="insId"></param>
        /// <returns></returns>
        public bool InscExist(string insId)
        {
            using (var db = new SchoolContext())
            {
                return db.Enrollements.Any(i => i.EnrollementNum == insId);
            }
        }


        /// <summary>
        /// Nouveau Numero d'Inscription Unique
        /// </summary>       
        /// <returns>Renvoi un nouveau Numéro d'Inscription Unique</returns>
        public string GetNewInscriptionId()
        {           
            string newId;

            do newId = "I" + RandomHelper.GetRandLetters(1) + "-" + DateTime.Today.Month + DateTime.Today.Year.ToString().Substring(2)  + "-" + RandomHelper.GetRandNum(4);
            while (
                     InscExist(newId)
                  );

            return newId;
        }


        /// <summary>
        /// Les Inscriptions d'une Classe
        /// </summary>
        /// <returns></returns>
        public List<InscriptionCard> GetInscriptions (Guid classGuid, Guid anneeScolaireGuid) {          
            using (var db = new SchoolContext()) {               
                return db.Enrollements.Where(i=> i.SchoolYear.SchoolYearGuid == anneeScolaireGuid && 
                !i.IsDeleted && i.Classe.ClasseGuid == classGuid)
                .Include(i=> i.Student)
                .Include(i=> i.Student.Person)
                .Include(i=> i.Classe)
                .Include(i=> i.SchoolYear)
                .OrderByDescending(i=> i.SchoolYear.DateDebut)
                .ToList()
                .Select(i=> new InscriptionCard(i))
                .ToList();
            }
        }


        /// <summary>
        /// Les Inscriptions d'un etudiant
        /// </summary>
        /// <returns></returns>
        public List<InscriptionCard> GetInscriptions (Guid studentGuid) => GetStudentInscriptions(studentGuid).Select(i => new InscriptionCard(i)).ToList();



        #region Internal Static



        internal static Enrollement StaticGetInscriptionByGuid(Guid insGuid)
        {
            using (var db = new SchoolContext())
                return db.Enrollements.Find(insGuid);
        }


        /// <summary>
        /// GetStudentInscriptions
        /// </summary>
        /// <param name="studentGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal static List<Enrollement> GetStudentInscriptions (Guid studentGuid, DateTime? startDate=null, DateTime? endDate=null) {
            using (var db = new SchoolContext()) {
                if(startDate==null||endDate==null)
                    return db.Enrollements.Where(i => i.StudentGuid==studentGuid&&!i.IsDeleted)
                                 .OrderByDescending(c => c.SchoolYear.DateDebut)
                                 .Include(i => i.Student)
                                 .Include(i => i.Student.Person)
                                 .Include(i => i.Classe)
                                 .Include(i => i.SchoolYear)
                                 .Include(i=> i.Classe.Filiere)                                
                                 .ToList();

                return db.Enrollements.Where(i => i.StudentGuid==studentGuid&&!i.IsDeleted &&
                                            (
                                                (
                                                    i.SchoolYear.DateDebut<=startDate&&
                                                    i.SchoolYear.DateFin>=startDate
                                                )
                                                ||
                                                (
                                                    i.SchoolYear.DateDebut>=startDate&&
                                                    i.SchoolYear.DateDebut<=endDate
                                                )
                                            )
                                           )                    
                                 .OrderByDescending(c => c.SchoolYear.DateDebut)
                                 .Include(i => i.Student)
                                 .Include(i => i.Student.Person)
                                 .Include(i => i.Classe)
                                 .Include(i => i.SchoolYear)
                                 .Include(i => i.Classe.Filiere)
                                 .ToList();

                //var ans =  db.AnneeScolaire.Where(i =>
                //        !i.IsDeleted&&
                //        ((
                //            i.DateDebut<=startDate&&
                //            i.DateFin>=startDate
                //        )
                //        ||
                //        (
                //            i.DateDebut>=startDate&&
                //            i.DateDebut<=endDate
                //        ))).ToList();

                //var mesIns = new List<Inscription>();

                //foreach (var an in ans)
                //{
                //    var an1 = an;
                //    mesIns.AddRange(
                //        db.Inscription.Where(
                //            i => i.StudentGuid == studentGuid && 
                //                 i.AnneeScolaireGuid == an1.AnneeScolaireGuid &&
                //                 !i.IsDeleted
                //            )
                //            .Include(i => i.Student)
                //            .Include(i => i.Classe)
                //            .Include(i => i.Classe.Filiere)
                //            .Include(i => i.AnneeScolaire)
                //        );
                //}

                //return mesIns.OrderByDescending(i => i.AnneeScolaire.DateDebut).ToList();
            }
        }



       #endregion








        }
}
