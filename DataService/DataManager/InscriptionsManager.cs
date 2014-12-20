using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataService.Context;
using DataService.Entities;
using DataService.Entities.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion Des Inscriptions
    /// </summary>
    public class InscriptionsManager
    {
        /// <summary>
        /// 
        /// </summary>      
        /// <param name="myInscription"></param>
        /// <returns></returns>
        public bool AddInscription(Inscription myInscription)
        {
            using (var db = new Ef())
            {
                db.Inscription.Add(myInscription);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myInscription"></param>
        /// <returns></returns>
        public bool UpdateInscription(Inscription myInscription)
        {
            using (var db = new Ef())
            {
                db.Inscription.Attach(myInscription);
                db.Entry(myInscription).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteInscription(Guid inscriptionGuid)
        {
            using (var db = new Ef())
            {
                db.Inscription.Remove(db.Inscription.Find(inscriptionGuid));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inscriptionGuid"></param>
        /// <returns></returns>
        public Inscription GetInscriptionById(Guid inscriptionGuid)
        {
            using (var db = new Ef())
            {
                return db.Inscription.Find(inscriptionGuid);
            }
        }

        /// <summary>
        /// Return true si l'Etudiant est Inscrit
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsStudentInsc( Guid studentGuid, Guid currentAnneeScolaireGuid)
        {
            using (var db = new Ef())
            {
                return db.Inscription.Any(i => i.AnneeScolaireGuid == currentAnneeScolaireGuid && i.StudentGuid == studentGuid);
            }            
        }

        /// <summary>
        /// Renvoi La List des Etudiants Non Inscrit
        /// </summary>
        /// <returns></returns>
        public HashSet<Student> GetStudentsNotIns(Guid currentAnneeScolaireGuid)
        {
            using (var db = new Ef())
            {
                var students = new HashSet<Student>();

                foreach (var std in db.Student)
                {
                    if (!IsStudentInsc(std.StudentGuid, currentAnneeScolaireGuid)) students.Add(std);                    
                }

                return students;

                //return new HashSet<Student>(db.Student.Where(s => IsStudentInsc(s.StudentGuid, currentAnneeScolaireGuid) == false));
            }
        }

        /// <summary>
        /// Verifier si ce ID est Deja Utilisee
        /// </summary>
        /// <param name="insId"></param>
        /// <returns></returns>
        public bool InscExist(string insId)
        {
            using (var db = new Ef())
            {
                return db.Inscription.Any(i => i.InscriptionId == insId);
            }
        }
    }
}
