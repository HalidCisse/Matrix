using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using CLib;
using Common.Pedagogy.Entity;
using Common.Pedagogy.Enums;
using Common.Security.Enums;
using DataService.Context;
using DataService.Helpers;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion Des Cours
    /// </summary>
    public class StudyManager {

        #region CRUD

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myStudy"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool AddCours(Study myStudy)
        {    
            using (var db = new SchoolContext())
            {
                if(db.Classes.Find(myStudy.ClasseGuid) == null)
                    throw new InvalidOperationException("CLASSE_NOT_FOUND");

                if(db.Staffs.Find(myStudy.ProffGuid) == null)
                    throw new InvalidOperationException("STAFF_NOT_FOUND");

                var recDays = CoursHelper.DecodeRecurrence(myStudy.RecurrenceDays);

                if(!StaticIsClasseFree(myStudy.ClasseGuid, recDays, myStudy.StartDate.GetValueOrDefault(), myStudy.EndDate.GetValueOrDefault(), myStudy.StartTime, myStudy.EndTime))
                    throw new InvalidOperationException("CLASSE_IS_NOT_FREE");

                if(!StaticIsStaffFree(myStudy.ProffGuid, recDays, myStudy.StartDate.GetValueOrDefault(), myStudy.EndDate.GetValueOrDefault(), myStudy.StartTime, myStudy.EndTime))
                    throw new InvalidOperationException("STAFF_IS_NOT_FREE");

                if(!StaticIsSalleFree(myStudy.Room, recDays, myStudy.StartDate.GetValueOrDefault(), myStudy.EndDate.GetValueOrDefault(), myStudy.StartTime, myStudy.EndTime))
                    throw new InvalidOperationException("CLASSROOM_IS_NOT_FREE");

                if(!StaticGetCoursEvents(myStudy, null, null).Any())
                    throw new InvalidOperationException("COURS_WITH_NO_OCCURRENCE");

                if(CoursHelper.IsGraded(myStudy.Type))
                {
                    myStudy.EndDate=myStudy.StartDate;
                    myStudy.RecurrenceDays = ((int)myStudy.StartDate.GetValueOrDefault().DayOfWeek).ToString();
                }
                    
                if (myStudy.StudyGuid==Guid.Empty)
                    myStudy.StudyGuid           = Guid.NewGuid();
                if(myStudy.GraderGuid==null || myStudy.GraderGuid== Guid.Empty)
                    myStudy.GraderGuid          = myStudy.ProffGuid ;
                if(myStudy.SupervisorGuid==null || myStudy.SupervisorGuid == Guid.Empty)
                    myStudy.SupervisorGuid      = myStudy.ProffGuid;
                if(!string.IsNullOrEmpty(myStudy.Room)) myStudy.Room = myStudy.Room.Trim();
                

                db.Studies.Add(myStudy);
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myStudy"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool UpdateCours(Study myStudy)
        {
            //todo if control add StudentGrades

            using (var db = new SchoolContext())
            {
                db.Studies.Attach(myStudy);
                db.Entry(myStudy).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Supprimer un Cours
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.SuperUser)]
        public bool HardDeleteCours(Guid coursId)
        {
            using (var db = new SchoolContext())
            {
                db.Studies.Remove(db.Studies.Find(coursId));
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Soft Delete un Cours
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool DeleteCours(Guid coursId)
        {
            using (var db = new SchoolContext())
            {
                db.Studies.Find(coursId).IsDeleted = true;
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Supprimer un Cours Pour Une Seule Journee dans sa Reccurence
        /// </summary>
        /// <param name="coursId"></param>
        /// <param name="exceptionDay"></param>
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.StudyWrite)]
        public bool AddCoursExeption(Guid coursId, DateTime exceptionDay)
        {
            using (var db = new SchoolContext())
            {
                var coursException = new StudyException {StudyGuid = coursId, ExceptionDate = exceptionDay};

                db.StudyExceptions.Add(coursException);
                return db.SaveChanges() > 0;         
            }
        }




        #endregion








        #region Helpers

        /// <summary>
        /// Determine si le Staff a aucune Tache Active
        /// </summary>
        /// <param name="profileGuid"></param>
        /// <returns></returns>
        public bool HasAnyJob (Guid profileGuid)
        {
            if (
                GetStaffSupervisionBetween(profileGuid, DateTime.Today, DateTime.Today)
                    .Any(study => StaticGetCoursEvents(study.StudyGuid, DateTime.Today, DateTime.Today).Any()))
                return true;

            using (var db = new SchoolContext())
                return db.Studies.Where(c => c.GraderGuid==profileGuid&&!c.IsDeleted&&
                                           ((
                                               c.StartDate<=DateTime.Today&&
                                               c.EndDate>=DateTime.Today
                                               )
                                            ||
                                            (
                                                c.StartDate>=DateTime.Today&&
                                                c.StartDate<=DateTime.Today
                                                ))).ToList().Any(s => CoursHelper.IsGraded(s.Type));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        public Study GetCoursById(Guid coursId)
        {
            using (var db = new SchoolContext())
                return db.Studies.Find(coursId);
        }


        /// <summary>
        /// Emploi du Temps d'une Classe
        /// </summary>
        /// <param name="classGuid"></param>
        /// <param name="currentDate"></param>
        /// <param name="scheduleType"></param>
        /// <returns></returns>
        public IEnumerable GetClasseSchedule (Guid classGuid, DateTime? currentDate, ScheduleType? scheduleType = ScheduleType.Weekly) {

            var scheduleDate = currentDate?.Date??DateTime.Today;

            var scheduleData = new ConcurrentBag<DayCoursCards>();

            var days = new HashSet<DateTime>();

            if(scheduleType==ScheduleType.Weekly) {
                var firstDateOfWeek = scheduleDate.DayOfWeek==DayOfWeek.Sunday
                    ? scheduleDate.AddDays(-6)
                    : scheduleDate.AddDays(-((int)scheduleDate.DayOfWeek-1));
                for(var i = 0; i<=6; i++)
                    days.Add(firstDateOfWeek.AddDays(i));
            }
            else
                days.Add(scheduleDate);

            Parallel.ForEach(days, d => {
                var dayCard = new DayCoursCards(classGuid, d);

                if(dayCard.DayDate.Equals(scheduleDate)&&scheduleDate!=DateTime.Today)
                    dayCard.DayColor="#894769";

                if(dayCard.DayCours.Any())
                    scheduleData.Add(dayCard);
            });

            return scheduleData.OrderBy(d => d.DayDate);
        }


        /// <summary>
        /// Emploi du Temps d un Staff
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="currentDate"></param>
        /// <param name="scheduleType"></param>
        /// <returns></returns>
        public IEnumerable GetStaffSchedule (Guid staffGuid, DateTime? currentDate, ScheduleType? scheduleType = ScheduleType.Weekly)
        {
            var scheduleDate = currentDate?.Date ?? DateTime.Today;
          
            var scheduleData = new ConcurrentBag<DayCoursCards>();

            var days = new HashSet<DateTime>();

            if (scheduleType == ScheduleType.Weekly)
            {
                var firstDateOfWeek = scheduleDate.DayOfWeek == DayOfWeek.Sunday
                    ? scheduleDate.AddDays(-6)
                    : scheduleDate.AddDays(-((int) scheduleDate.DayOfWeek - 1));
                for (var i = 0; i <= 6; i++)
                    days.Add(firstDateOfWeek.AddDays(i));
            }
            else
                days.Add(scheduleDate);

            Parallel.ForEach(days, d => {
                var dayCard = new DayCoursCards(staffGuid, d, true);

                if(dayCard.DayDate.Equals(scheduleDate)&&scheduleDate!=DateTime.Today)
                    dayCard.DayColor="#894769";

                if(dayCard.DayCours.Any())
                    scheduleData.Add(dayCard);
            });

            return scheduleData.OrderBy(d => d.DayDate);
        }

        
        /// <summary>
        /// Determine si la salle est libre durant ce moment
        /// </summary>
        /// <param name="sallename"></param>
        /// <param name="weekDays"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool IsSalleFree(string sallename, List<DayOfWeek> weekDays, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime) 
            => StaticIsSalleFree(sallename, weekDays, startDate, endDate, startTime, endTime);


        /// <summary>
        /// Determine si le prof est libre durant ce moment
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="weekDays"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool IsStaffFree (Guid staffGuid, List<DayOfWeek> weekDays, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime) 
            => StaticIsStaffFree(staffGuid, weekDays, startDate, endDate, startTime, endTime);


        /// <summary>
        /// Determine si la Classe est libre durant ce moment
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <param name="weekDays"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool IsClasseFree (Guid classeGuid, List<DayOfWeek> weekDays, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime) 
            => StaticIsClasseFree(classeGuid, weekDays ,startDate, endDate, startTime, endTime);

        #endregion







        #region Protected Internal Static

        /// <summary>
        /// Determine si la salle est libre durant ce moment
        /// </summary>
        /// <param name="sallename"></param>
        /// <param name="weekDays"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        protected internal static bool StaticIsSalleFree (string sallename, List<DayOfWeek> weekDays, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            if (string.IsNullOrEmpty(sallename)) return true;
           
            using (var db = new SchoolContext()) {
                var periodeEvents = db.Studies.Where(c =>
                                    c.Room.Equals(sallename.Trim(), StringComparison.CurrentCultureIgnoreCase) &&!c.IsDeleted&&
                                    (
                                        (
                                            c.StartDate<=startDate&&
                                            c.EndDate>=startDate
                                        )
                                        ||
                                        (
                                            c.StartDate>=startDate&&
                                            c.StartDate<=endDate
                                        )
                                    )
                                    &&
                                    (
                                        (
                                            c.StartTime<=startTime&&
                                            c.EndTime>=startTime
                                        )
                                        ||
                                        (
                                            c.StartTime>=startTime&&
                                            c.StartTime<=endTime
                                        )
                                    )
                                   );

                return
                    DateTimeHelper.EachDay(startDate, endDate)
                        .Where(d => weekDays.Contains(d.DayOfWeek))
                        .All(day => !periodeEvents
                            .Any(e =>
                                    e.RecurrenceDays.Contains(((int)day.DayOfWeek).ToString())&&
                                    e.Exceptions.All(exc => exc.ExceptionDate!=day)));

                //return DateTimeHelper.EachDay(startDate, endDate).Where(d => weekDays.Contains(d.DayOfWeek)).All(day => !periodeEvents.Any(e => e.RecurrenceDays.Contains(((int)day.DayOfWeek).ToString())));
                //return DateTimeHelper.EachDay(startDate, endDate).All(day => !periodeEvents.Any(e => e.RecurrenceDays.Contains(((int)day.DayOfWeek).ToString())));
            }
        }

        /// <summary>
        /// Determine si l'enseignant est libre durant ce moment
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="weekDays"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        protected internal static bool StaticIsStaffFree (Guid staffGuid, List<DayOfWeek> weekDays, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime) {
            using (var db = new SchoolContext()) {
                var periodeEvents = db.Studies.Where(c =>
                                    c.Proff.StaffGuid==staffGuid&&!c.IsDeleted&&
                                    (
                                        (
                                            c.StartDate<=startDate&&
                                            c.EndDate>=startDate
                                        )
                                        ||
                                        (
                                            c.StartDate>=startDate&&
                                            c.StartDate<=endDate
                                        )
                                    )
                                    &&
                                    (
                                        (
                                            c.StartTime<=startTime&&
                                            c.EndTime>=startTime
                                        )
                                        ||
                                        (
                                            c.StartTime>=startTime&&
                                            c.StartTime<=endTime
                                        )
                                    )
                                   );

                return
                    DateTimeHelper.EachDay(startDate, endDate)
                        .Where(d => weekDays.Contains(d.DayOfWeek))
                        .All(day => !periodeEvents
                            .Any(e =>
                                    e.RecurrenceDays.Contains(((int)day.DayOfWeek).ToString())&&
                                    e.Exceptions.All(exc => exc.ExceptionDate!=day)));

                //return DateTimeHelper.EachDay(startDate, endDate).Where(d => weekDays.Contains(d.DayOfWeek)).All(day => !periodeEvents.Any(e => e.RecurrenceDays.Contains(((int)day.DayOfWeek).ToString())));
                //return DateTimeHelper.EachDay(startDate, endDate).All(day => !periodeEvents.Any(e => e.RecurrenceDays.Contains(((int)day.DayOfWeek).ToString())));
            }
        }

        /// <summary>
        /// Determine si la classe est libre durant ce moment
        /// </summary>
        /// <param name="classGuid"></param>
        /// <param name="weekDays"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        protected internal static bool StaticIsClasseFree (Guid classGuid, List<DayOfWeek> weekDays, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime)
        {
            using (var db = new SchoolContext())
            {
                var periodeEvents = db.Studies.Where(c =>
                                    c.ClasseGuid==classGuid&&!c.IsDeleted&&
                                    (
                                        (
                                            c.StartDate<=startDate&&
                                            c.EndDate>=startDate
                                        )
                                        ||
                                        (
                                            c.StartDate>=startDate&&
                                            c.StartDate<=endDate
                                        )
                                    )
                                    &&
                                    (
                                        (
                                            c.StartTime<=startTime&&
                                            c.EndTime>=startTime
                                        )
                                        ||
                                        (
                                            c.StartTime>=startTime&&
                                            c.StartTime<=endTime
                                        )
                                    )
                                   );

                //foreach (var e in from e in periodeEvents
                //    from dayOfWeek in weekDays
                //    where !e.RecurrenceDays.Contains(((int) dayOfWeek).ToString())
                //    select e)
                //    periodeEvents.Remove(e);

                return
                    DateTimeHelper.EachDay(startDate, endDate)
                        .Where(d => weekDays.Contains(d.DayOfWeek))
                        .All(day => !periodeEvents
                            .Any(e =>
                                    e.RecurrenceDays.Contains(((int) day.DayOfWeek).ToString()) &&
                                    e.Exceptions.All(exc => exc.ExceptionDate != day)));

                //return DateTimeHelper.EachDay(startDate, endDate).Where(d => weekDays.Contains(d.DayOfWeek)).All(day => !periodeEvents.Any(e => e.RecurrenceDays.Contains(((int) day.DayOfWeek).ToString())));
            }
        }

        /// <summary>
        /// Le nombre d'heure dont ce cours a ete enseigner
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="coursGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="removeAbsencesAndRetards"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected internal static TimeSpan StaticGetHoursTaught (Guid staffGuid, Guid coursGuid, DateTime? startDate, DateTime? endDate, bool removeAbsencesAndRetards = false) {
            var hoursTaught = new TimeSpan();

            if(!removeAbsencesAndRetards) {
                foreach(var cEvent in StaticGetCoursEvents(coursGuid, startDate, endDate))
                    hoursTaught+=(cEvent.EndTime-cEvent.StartTime).Duration();
                return hoursTaught;
            }

            foreach(var cEvent in StaticGetCoursEvents(coursGuid, startDate, endDate))
                hoursTaught+=(cEvent.EndTime-cEvent.StartTime).Duration();
            return hoursTaught-AbsenceManager.GetTotalAbsences(staffGuid, coursGuid, startDate, endDate);
        }

        /// <summary>
        /// Le nombre D'heure que ce Staff a enseigner
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="removeAbsencesAndRetards"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected internal static TimeSpan StaticGetHoursTaught (Guid staffGuid, DateTime? startDate, DateTime? endDate, bool removeAbsencesAndRetards = false)
        {
            var hoursTaught = new TimeSpan();

            if (!removeAbsencesAndRetards)
            {
                return GetStaffCoursBetween(staffGuid, startDate, endDate)
                    .Aggregate(hoursTaught, (current1, cours) => StaticGetCoursEvents(cours.StudyGuid, startDate, endDate)
                    .Aggregate(current1, (current, cEvent) => current+(cEvent.EndTime-cEvent.StartTime).Duration()));               
            }
            foreach(var cours in GetStaffCoursBetween(staffGuid, startDate, endDate))
                foreach(var cEvent in StaticGetCoursEvents(cours.StudyGuid, startDate, endDate))
                    hoursTaught+=(cEvent.EndTime-cEvent.StartTime);
            return hoursTaught - AbsenceManager.GetTotalAbsences(staffGuid, startDate, endDate);
        }

        /// <summary>
        /// Le nombre d'evenement pour ce cours
        /// </summary>
        /// <param name="coursGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected internal static List<CoursEvent> StaticGetCoursEvents (Guid coursGuid, DateTime? startDate, DateTime? endDate) {
            using (var db = new SchoolContext())
            {                
                var coursEvents = new List<CoursEvent>();
                var myCours     = db.Studies.Find(coursGuid);

                if (myCours == null) return null;               

                var fromDate    = myCours.StartDate.GetValueOrDefault();
                var toDate      = myCours.EndDate.GetValueOrDefault();

                if (startDate != null)
                    fromDate    =(DateTime) startDate;                

                if(endDate!=null)
                    toDate      =(DateTime)endDate;

                if (myCours.StartDate > fromDate)
                    fromDate = myCours.StartDate.GetValueOrDefault();

                if(myCours.EndDate < toDate)
                    toDate=myCours.EndDate.GetValueOrDefault();              

                Parallel.ForEach(new ConcurrentBag<DateTime>(DateTimeHelper.EachDay(fromDate, toDate)), date =>
                {
                    var dayNum = ((int)date.DayOfWeek).ToString();

                    if(myCours.RecurrenceDays.Contains(dayNum))
                        coursEvents.Add(new CoursEvent(myCours, date));
                });

                foreach (var ex in db.StudyExceptions.Where(ex => ex.StudyGuid == myCours.StudyGuid).ToList())
                    coursEvents.Remove(coursEvents.FirstOrDefault(e => e.EventDate == ex.ExceptionDate));               
                return coursEvents.OrderBy(e => e.EventDate).ThenBy(e => e.StartTime).ToList();
            }
        }

        /// <summary>
        /// Le nombre d'evenement pour ce cours
        /// </summary>
        /// <param name="myStudy"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected internal static List<CoursEvent> StaticGetCoursEvents (Study myStudy, DateTime? startDate, DateTime? endDate) {
            
                var coursEvents = new List<CoursEvent>();

                var fromDate = myStudy.StartDate.GetValueOrDefault();
                var toDate = myStudy.EndDate.GetValueOrDefault();

                if(startDate!=null)
                    fromDate=(DateTime)startDate;

                if(endDate!=null)
                    toDate=(DateTime)endDate;

                if(myStudy.StartDate>fromDate)
                    fromDate=myStudy.StartDate.GetValueOrDefault();

                if(myStudy.EndDate<toDate)
                    toDate=myStudy.EndDate.GetValueOrDefault();

            Parallel.ForEach(new ConcurrentBag<DateTime>(DateTimeHelper.EachDay(fromDate, toDate)), date =>
            {
                var dayNum = ((int)date.DayOfWeek).ToString();

                if(myStudy.RecurrenceDays.Contains(dayNum))
                    coursEvents.Add(new CoursEvent(myStudy, date));
            });

            return coursEvents.OrderBy(e=> e.EventDate).ThenBy(e=> e.StartTime).ToList();           
        }

        /// <summary>
        /// Liste des cours que le staff a enseigner 
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static List<Study> GetStaffSupervisionBetween (Guid staffGuid, DateTime? startDate = null, DateTime? endDate = null) {
            using (var db = new SchoolContext()) {
                if(startDate==null||endDate==null)
                    return db.Studies.Where(c => c.SupervisorGuid==staffGuid&&!c.IsDeleted)
                                 .OrderBy(c => c.StartDate)
                                 .ThenBy(c => c.StartTime)
                                 .ToList();

                return db.Studies.Where(c =>
                        c.SupervisorGuid==staffGuid&&!c.IsDeleted&&
                        ((
                            c.StartDate<=startDate&&
                            c.EndDate>=startDate
                        )
                        ||
                        (
                            c.StartDate>=startDate&&
                            c.StartDate<=endDate
                        ))).OrderBy(c => c.StartDate)
                           .ThenBy(c => c.StartTime).ToList();
            }
        }

        /// <summary>
        /// Liste des cours que le staff a enseigner 
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal static List<Study> GetStaffCoursBetween(Guid staffGuid, DateTime? startDate = null, DateTime? endDate = null) {
            using (var db = new SchoolContext())
            {
                if (startDate == null || endDate == null)
                    return db.Studies.Where(c => c.ProffGuid==staffGuid&&!c.IsDeleted)
                                 .OrderBy(c => c.StartDate)
                                 .ThenBy(c => c.StartTime)
                                 .ToList();

                return db.Studies.Where(c =>
                        c.ProffGuid==staffGuid && !c.IsDeleted &&
                        ((
                            c.StartDate<=startDate&&
                            c.EndDate>=startDate
                        )
                        ||
                        (
                            c.StartDate>=startDate&&
                            c.StartDate<=endDate
                        ))).OrderBy(c => c.StartDate)
                           .ThenBy(c => c.StartTime).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        protected internal HashSet<Study> GetClassCoursBetween (Guid classeGuid, DateTime startDate, DateTime endDate) {
            using (var db = new SchoolContext()) {
                return new HashSet<Study>(db.Studies.Where(c =>
                    c.ClasseGuid==classeGuid && !c.IsDeleted &&
                    (
                         (
                             c.StartDate<=startDate&&
                             c.EndDate>=startDate
                         )
                         ||
                         (
                             c.StartDate>=startDate&&
                             c.StartDate<=endDate
                         )
                   )).OrderBy(c => c.StartTime));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursId"></param>
        /// <returns></returns>
        protected internal static Study StaticGetCoursById(Guid coursId)
        {
            using (var db = new SchoolContext())
                return db.Studies.Find(coursId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursGuid"></param>
        /// <returns></returns>
        protected internal static Guid StaticGetCoursClasseGuid(Guid coursGuid)
        {
            using (var db = new SchoolContext())
                return db.Studies.Find(coursGuid).ClasseGuid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursGuid"></param>
        /// <returns></returns>
        protected internal static Guid StaticGetCoursStaffGuid(Guid coursGuid)
        {
            using (var db = new SchoolContext())
                return db.Studies.Find(coursGuid).Proff.StaffGuid;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="coursGuid"></param>
        ///// <returns></returns>
        //protected internal static Guid StaticGetCoursAnneeScolaireGuid(Guid coursGuid)
        //{
        //    using (var db = new SchoolContext())
        //        return db.SchoolPeriods.Find(db.Cours.Find(coursGuid).PeriodeScolaireGuid)?.SchoolYearGuid ??
        //               PedagogyManager.StaticGetDefaultAnneeScolaireGuid;
        //}


        #endregion

        
    }
}
