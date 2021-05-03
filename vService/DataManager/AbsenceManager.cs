using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using Common.Security.Enums;
using DataService.Context;
using DataService.ViewModel.Economat;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Absences et Retards
    /// </summary>
    public class AbsenceManager
    {

        #region CRUD

        /// <summary>
        /// Ajouter ou Modifier un Ticket s'il Exist
        /// </summary>
        /// <param name="myTicket">L'objet Ticket</param>
        /// <returns>True pour Success</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Superviseur)]
        public bool AddOrUpdateAbsenceTicket(AbsenceTicket myTicket)
        {            
            if (!myTicket.IsPresent || myTicket.RetardTime != new TimeSpan(0))
                return AbsenceTicketExist(myTicket) ? UpdateAbsenceTicket(myTicket) : AddAbsenceTicket(myTicket);

            return DeleteAbsenceTicket(myTicket.AbsenceTicketGuid);
        }


        /// <summary>
        /// Ajouter un Ticket 
        /// </summary>
        /// <param name="myTicket"></param>
        /// <returns>True pour Success</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Superviseur)]
        private static bool AddAbsenceTicket(AbsenceTicket myTicket)
        {
            if (!myTicket.IsPresent) myTicket.RetardTime = new TimeSpan(0, 0, 0, 0);

            using (var db = new SchoolContext())
            {
                if(myTicket.AbsenceTicketGuid==Guid.Empty)
                    myTicket.AbsenceTicketGuid=Guid.NewGuid();
                
                db.AbsenceTickets.Add(myTicket);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Modifier un Ticket 
        /// </summary>
        /// <param name="myTicket"></param>
        /// <returns>True pour Success</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Superviseur)]
        private static bool UpdateAbsenceTicket(AbsenceTicket myTicket)
        {
            if (!myTicket.IsPresent) myTicket.RetardTime = new TimeSpan(0, 0, 0, 0);

            using (var db = new SchoolContext())
            {                
                db.AbsenceTickets.Attach(myTicket);
                db.Entry(myTicket).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Suprime Un Ticket
        /// </summary>
        /// <param name="myTicketGuid">Guid du Ticket</param>
        /// <returns>True pour Success</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.Superviseur)]
        private static bool DeleteAbsenceTicket(Guid myTicketGuid)
        {
            using (var db = new SchoolContext())
            {
                db.AbsenceTickets.Remove(db.AbsenceTickets.Find(myTicketGuid));
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Verifie L'existence d'un Ticket
        /// </summary>
        /// <param name="absenceTicket">Objet Ticket</param>        
        /// <returns>True pour oui</returns>
        private static bool AbsenceTicketExist(AbsenceTicket absenceTicket)
        {           
            using (var db = new SchoolContext())
            {               
                if (db.AbsenceTickets.Find(absenceTicket.AbsenceTicketGuid) != null) return true;
                
                return db.AbsenceTickets.Any(t => t.CoursGuid == absenceTicket.CoursGuid && 
                                                 t.PersonGuid == absenceTicket.PersonGuid && 
                                                 t.CoursDate == absenceTicket.CoursDate);
            }
        }


        #endregion




        #region Helpers




        /// <summary>
        /// Cours de supervision
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable GetStaffSupervisions (Guid staffGuid, DateTime? startDate = null, DateTime? endDate = null) {
            var data = new List<DataCard>();

            if(startDate==null||endDate==null)
                startDate=endDate=DateTime.Today;

            foreach(var study in StudyManager.GetStaffSupervisionBetween(staffGuid, startDate, endDate))
                data.AddRange(
                    StudyManager.StaticGetCoursEvents(study.StudyGuid, startDate, endDate).Select(evt => new DataCard(evt)));
            return data;
        }

        /// <summary>
        /// Verifie Si un Etudiant ou Staff est Present a un cours donnee
        /// </summary>       
        /// <param name="personGuid">Guid de la Personne</param>
        /// <param name="coursGuid">Guid du Cours</param>
        /// <param name="coursDate">Date du Cours</param>
        /// <returns>True pour Oui</returns>
        public static bool EstPresent(Guid personGuid, Guid coursGuid, DateTime coursDate)
        {           
            using (var db = new SchoolContext())
            {
                var ticket = db.AbsenceTickets.FirstOrDefault(t => t.CoursGuid == coursGuid &&
                                                                          t.PersonGuid == personGuid &&
                                                                          t.CoursDate == coursDate);
                return ticket != null && ticket.IsPresent;
            }
        }

        /// <summary>
        /// Nombre Absent
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        public int GetNumberAbsences (Guid personGuid, DateTime fromDate, DateTime toDate) {
            using (var db = new SchoolContext()) {

                return db.AbsenceTickets.Count(tk =>
                                                 tk.PersonGuid==personGuid && !tk.IsPresent &&
                                                (
                                                    tk.CoursDate>=fromDate &&
                                                    tk.CoursDate<=toDate
                                                )
                                             );
            }
        }

        /// <summary>
        /// Changer de Superviseur
        /// </summary>
        /// <returns></returns>
        public bool SetSupervisor (Guid studyGuid, Guid newSupervisorGuid) {
            using (var db = new SchoolContext()) {

                var thestudy = db.Studies.Find(studyGuid);

                if(thestudy==null)
                    throw new InvalidOperationException("CAN_NOT_FIND_REFERENCED_STUDY");
                if(db.Staffs.Find(newSupervisorGuid)==null)
                    throw new InvalidOperationException("CAN_NOT_FIND_REFERENCED_STAFF");

                thestudy.SupervisorGuid=newSupervisorGuid;

                db.Studies.Attach(thestudy);
                db.Entry(thestudy).State=EntityState.Modified;
                return db.SaveChanges()>0;
            }
        }

        /// <summary>
        /// Le nombre d'Absence
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public int GetNumberRetards (Guid personGuid, DateTime fromDate, DateTime toDate) {
            using (var db = new SchoolContext())
            {               
                return db.AbsenceTickets.Count(tk => tk.PersonGuid==personGuid && tk.RetardTime>TimeSpan.Zero && tk.IsPresent &&
                                                (
                                                    tk.CoursDate>=fromDate&&
                                                    tk.CoursDate<=toDate
                                                )
                                             );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public TimeSpan GetTotalTimeAbsent(Guid personGuid, DateTime startDate, DateTime endDate) => GetTotalAbsences(personGuid, startDate, endDate);

        /// <summary>
        /// List de Tickets Absences/Retards
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable GetPersonTicketCards (Guid personGuid, DateTime startDate, DateTime endDate) {
            using (var db = new SchoolContext())
            {
                return db.AbsenceTickets.Where(tk =>
                    tk.PersonGuid == personGuid &&
                    (
                        tk.CoursDate >=startDate&&
                        tk.CoursDate <=endDate
                        )
                    ).OrderBy(tk => tk.CoursDate).ToList().Select(t=> new AssiduiteCard(t));
            }
        }

        /// <summary>
        /// Model des Presences
        /// </summary>
        /// <param name="currentCoursGuid"></param>
        /// <param name="coursDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable GetPresenceCards (Guid currentCoursGuid, DateTime coursDate) {
            //var tiketList = new HashSet<AbsenceTicketCard> {new AbsenceTicketCard(CoursManager.StaticGetCoursStaffGuid(currentCoursGuid), currentCoursGuid, coursDate) };

            //var tiketListB = new ConcurrentBag<AbsenceTicketCard>();

            //var stdsGuids = ClassesManager.StaticGetClassStudentsGuids(CoursManager.StaticGetCoursClasseGuid(currentCoursGuid), CoursManager.StaticGetCoursAnneeScolaireGuid(currentCoursGuid));


            using (var db = new SchoolContext()) {
                var theCours = db.Studies.Find(currentCoursGuid);

                var tiketList = new HashSet<AbsenceTicketCard> { new AbsenceTicketCard(theCours.Proff.PersonGuid, currentCoursGuid, coursDate) };
                var tiketListB = new ConcurrentBag<AbsenceTicketCard>();

                var stdsGuids =
                    theCours.Classe.Inscriptions.Where(
                        i => !i.IsDeleted&&i.EnrollementStatus!=EnrollementStatus.Canceled&&i.SchoolYear.DateDebut<=coursDate&&i.SchoolYear.DateFin>=coursDate)
                        .Select(i => i.Student.Person.PersonGuid);

                Parallel.ForEach(stdsGuids, std => {
                    tiketListB.Add(new AbsenceTicketCard(std, currentCoursGuid, coursDate));
                });

                return tiketList.Union(tiketListB.OrderBy(s => s.FullName));
            }
        }

        /// <summary>
        /// Renvoi la Liste des Absences d'un Staff ou Etudiants
        /// </summary>
        /// <param name="personGuid"></param>        
        /// <returns></returns>
        public HashSet<PeriodeAttendance> GetYearAttendancesCard (Guid personGuid) {
            using (var db = new SchoolContext()) {
                var anneeScolaireGuid = PedagogyManager.StaticGetDefaultAnneeScolaireGuid;

                var periodesAttendanceCard = new HashSet<PeriodeAttendance>();

                var anneePeriodesList = db.SchoolPeriods.Where(p => p.SchoolYearGuid==anneeScolaireGuid&&p.StartDate<=DateTime.Today).OrderBy(p => p.StartDate).ToList();

                foreach(var anneePeriode in anneePeriodesList)
                    periodesAttendanceCard.Add(new PeriodeAttendance(personGuid, anneePeriode));

                return periodesAttendanceCard;
            }
        }

        #endregion



        #region Internal Static



        /// <summary>
        /// 
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="coursGuid"></param>
        /// <param name="coursDate"></param>
        /// <returns></returns>
        protected internal static bool StaticEstPresent(Guid personGuid, Guid coursGuid, DateTime coursDate)
        {
            coursDate = coursDate.Date;

            using (var db = new SchoolContext())
            {
                var ticket = db.AbsenceTickets.FirstOrDefault(t => t.CoursGuid == coursGuid &&
                                                                  t.PersonGuid == personGuid &&
                                                                  t.CoursDate == coursDate);
                return ticket == null || ticket.IsPresent;
            }
        }


        /// <summary>
        /// Le Temps d'absence d'une Personne aux cours
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static TimeSpan GetTotalAbsences(Guid personGuid, DateTime? startDate, DateTime? endDate)
        {
            using (var db = new SchoolContext()) {
                var totalAbs = new TimeSpan();
        
                if(startDate==null||endDate==null)               
                    foreach (var abs in db.AbsenceTickets.Where(t => t.PersonGuid == personGuid))
                        if (abs.IsPresent)
                            totalAbs += abs.RetardTime;
                        else{
                            var cours = db.Studies.Find(abs.CoursGuid);
                            totalAbs += ((cours.EndTime - cours.StartTime));
                        }                
                else
                {
                    foreach(var abs in db.AbsenceTickets.Where(t => t.PersonGuid==personGuid && (t.CoursDate >= startDate && t.CoursDate <= endDate)))
                        if(abs.IsPresent)
                            totalAbs+=abs.RetardTime;
                        else {
                            var cours = db.Studies.Find(abs.CoursGuid);
                            totalAbs+=((cours.EndTime-cours.StartTime));
                        }
                }
                return totalAbs;
            }
        }


        /// <summary>
        /// Le Temps d'absence d'une personne A un cours
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="coursGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static TimeSpan GetTotalAbsences (Guid personGuid, Guid coursGuid, DateTime? startDate, DateTime? endDate) {
            using (var db = new SchoolContext()) {
                var totalAbs = new TimeSpan();

                if(startDate==null||endDate==null)
                    foreach(var abs in db.AbsenceTickets.Where(t => t.PersonGuid==personGuid && t.CoursGuid ==coursGuid))
                        if(abs.IsPresent)
                            totalAbs+=abs.RetardTime;
                        else {
                            var cours = db.Studies.Find(abs.CoursGuid);
                            totalAbs+=((cours.EndTime-cours.StartTime));
                        }
                else {
                    foreach(var abs in db.AbsenceTickets.Where(t => t.PersonGuid==personGuid && t.CoursGuid==coursGuid && (t.CoursDate>=startDate&&t.CoursDate<=endDate)))
                        if(abs.IsPresent)
                            totalAbs+=abs.RetardTime;
                        else {
                            var cours = db.Studies.Find(abs.CoursGuid);
                            totalAbs+=((cours.EndTime-cours.StartTime));
                        }
                }
                return totalAbs;
            }
        }






        #endregion

       
    }
}
