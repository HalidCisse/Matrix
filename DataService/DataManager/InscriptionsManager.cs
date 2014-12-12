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
        public bool IsStudentInsc( string studentId, Guid currentAnneeScolaireGuid)
        {
            using (var db = new Ef())
            {
                return db.Inscription.Any(i => i.AnneeScolaireId == currentAnneeScolaireGuid && i.StudentId == studentId);
            }            
        }

        /// <summary>
        /// Renvoi La List des Etudiants Non Inscrit
        /// </summary>
        /// <returns></returns>
        public List<Student> GetStudentsNotIns(Guid currentAnneeScolaireGuid)
        {
            using (var db = new Ef())
            {
                return new List<Student>(db.Student.Where(s => IsStudentInsc(s.StudentId, currentAnneeScolaireGuid) == false));
            }
        }

    }
}
