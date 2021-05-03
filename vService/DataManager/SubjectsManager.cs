using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Web.Security;
using Common.Pedagogy.Entity;
using DataService.Context;
using DataService.ViewModel.Pedagogy;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Matiere
    /// </summary>
    public class SubjectsManager {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mySubject"></param>
		/// <returns></returns>
		//[PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.MatiereWrite)]
		public bool AddMatiere(Subject mySubject)
        {
			var userKey = Membership.GetUser()?.ProviderUserKey;
			if (userKey == null)                      throw new SecurityException("CAN_NOT_VERIFY_USER_IDENTITY");
			if (string.IsNullOrEmpty(mySubject.Name)) throw new InvalidOperationException("SUBJECT_NAME_CAN_NOT_BE_EMPTY");
			
			using (var db = new SchoolContext())
            {
				if (db.Subjects.Any(m=> m.Sigle.Equals(mySubject.Sigle, StringComparison.CurrentCultureIgnoreCase))) throw new InvalidOperationException("SUBJECT_WITH_SAME_CODE_EXIST");
				if (mySubject.SubjectGuid == Guid.Empty) mySubject.SubjectGuid         = Guid.NewGuid();
				if (string.IsNullOrEmpty(mySubject.Description)) mySubject.Description = mySubject.Name;

				mySubject.LastEditDate     = DateTime.Now;
				mySubject.LastEditUserGuid = (Guid)userKey;
				mySubject.DateAdded        = DateTime.Now;
				mySubject.AddUserGuid      = (Guid)userKey;

				db.Subjects.Add(mySubject);
                return db.SaveChanges() > 0;
            }
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="mySubject"></param>
		/// <returns></returns>
		//[PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.MatiereRead)]
		public bool UpdateMatiere(Subject mySubject)
        {
			var userKey = Membership.GetUser()?.ProviderUserKey;
			if (userKey == null)                      throw new SecurityException("CAN_NOT_VERIFY_USER_IDENTITY");
			if (string.IsNullOrEmpty(mySubject.Name)) throw new InvalidOperationException("SUBJECT_NAME_CAN_NOT_BE_EMPTY");
			

			using (var db = new SchoolContext())
			{
				var modMatiere = db.Subjects.Find(mySubject.SubjectGuid);
				if (modMatiere==null) throw new InvalidOperationException("CAN_NOT_FIND_SUBJECT_REFERENCE");
				if (string.IsNullOrEmpty(mySubject.Description)) mySubject.Description = mySubject.Name;

				modMatiere.Name             = mySubject.Name;
				modMatiere.Module           = mySubject.Module;
				modMatiere.StudyLanguage    = mySubject.StudyLanguage;
				modMatiere.Discipline       = mySubject.Discipline;
				modMatiere.Description      = mySubject.Description;
				modMatiere.Couleur          = mySubject.Couleur;
				modMatiere.WeeklyHours      = mySubject.WeeklyHours;

				modMatiere.LastEditDate     = DateTime.Now;
				modMatiere.LastEditUserGuid = (Guid)userKey;

				db.Subjects.Attach(modMatiere);
                db.Entry(modMatiere).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="matiereId"></param>
		/// <returns></returns>
		//[PrincipalPermission(SecurityAction.Demand, Role = SecurityClearances.MatiereDelete)]
		public bool DeleteMatiere(Guid matiereId)
        {
            using (var db = new SchoolContext())
            {
                db.Subjects.Remove(db.Subjects.Find(matiereId));
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Renvoi les Matieres Cards Pour Une Classe
        /// </summary>
        /// <param name="classeGuid">ID de la Classe</param>
        /// <param name="currentDate">La Date Actuelle</param>
        /// <returns></returns>
        public List<MatiereCard> GetClassMatieresCards (Guid classeGuid, DateTime currentDate) {
            using (var db = new SchoolContext()) {
                currentDate=currentDate.Date;
                var firstDateOfWeek = currentDate.DayOfWeek==DayOfWeek.Sunday ? currentDate.AddDays(-6) : currentDate.AddDays(-((int)currentDate.DayOfWeek-1));
                var lastDateOfWeek = firstDateOfWeek.AddDays(6);

                return db.Studies.Where(c =>
                                                c.ClasseGuid==classeGuid&&!c.IsDeleted&&
                                                (
                                                    (
                                                        c.StartDate<=firstDateOfWeek&&
                                                        c.EndDate>=lastDateOfWeek
                                                    )
                                                    ||
                                                    (
                                                        c.EndDate>=firstDateOfWeek&&
                                                        c.EndDate<=lastDateOfWeek
                                                    )
                                                    ||
                                                    (
                                                        c.StartDate>=firstDateOfWeek&&
                                                        c.StartDate<=lastDateOfWeek
                                                    )
                                                )
                                             ).Select(c=> c.Subject)
                                              .Distinct()
                                              .OrderBy(s => s.Name)
                                              .ToList()
                                              .Select(s => new MatiereCard(s))                                         
                                              .ToList();    
            }
        }


        /// <summary>
        /// Return Toutes Les Matieres de cette Classe
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <returns></returns>
        public List<MatiereCard> GetClassMatieresCards (Guid classeGuid) {
            using (var db = new SchoolContext()) {               
                return db.Studies.Where(m => m.ClasseGuid == classeGuid && !m.IsDeleted)
                    .Select(c => c.Subject)
                    .Distinct()
                    .OrderBy(s => s.Name)
                    .ToList()
                    .Select(s => new MatiereCard(s))                   
                    .ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereGuid"></param>
        /// <returns></returns>
        public Subject GetMatiereById(Guid matiereGuid)
        {
            using (var db = new SchoolContext())
            {
                var myMatiere = db.Subjects.Find(matiereGuid);
                return myMatiere;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereName"></param>
        /// <returns></returns>
        public static Subject GetMatiereByName(string matiereName)
        {
            using (var db = new SchoolContext())
            {
                var myMatiere = db.Subjects.SingleOrDefault(s => s.Name == matiereName);

                return myMatiere;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Subject> GetAllMatieres()
        {
            using (var db = new SchoolContext())
                return db.Subjects.ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMatieresNames()
        {
            using (var db = new SchoolContext())
            {
                return db.Subjects.Select(m => m.Name).ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereId"></param>
        /// <returns></returns>
        public string GetMatiereName(Guid matiereId)
        {
            using (var db = new SchoolContext())
            {
                var myMatiereName = db.Subjects.Find(matiereId).Name;
                return myMatiereName;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereName"></param>
        /// <returns></returns>
        public string GetMatiereIdFromName(string matiereName)
        {
            using (var db = new SchoolContext())
            {
                var mat = db.Subjects.FirstOrDefault(m => m.Name == matiereName);
                return mat == null ? null : mat.SubjectGuid.ToString();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="matiereId"></param>
        /// <returns></returns>
        public bool MatiereExist(Guid matiereId)
        {
            using (var db = new SchoolContext())
            {
                return db.Subjects.Find(matiereId) != null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int MatiereSigleCont (string name) {
            using (var db = new SchoolContext()) {
                return db.Subjects.Count(s => s.Sigle.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
        }


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<string> GetModules()
		{
			using (var db = new SchoolContext())
			{
				var modules = db.Subjects.Where(m=> !string.IsNullOrEmpty(m.Module)).Select(m => m.Module ).ToList();

				//if (!modules.Any())
					modules.AddRange(new List<string> {"Module A", "Module B", "Module C"});
				return modules.Distinct().ToList();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<string> GetDisciplines()
		{
			using (var db = new SchoolContext())
			{
				var disciplines = db.Subjects.Where(m => !string.IsNullOrEmpty(m.Discipline)).Select(m => m.Discipline).ToList();

				//if (!disciplines.Any())
					disciplines.AddRange(new List<string> { "Mathématique", "Physique", "Arts", "Histoire ", "Géographie", "Arts", "Science et technologie", "Éducation physique ", "Droit" });
				return disciplines.Distinct().ToList();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<string> GetStudyLanguages()
		{
			using (var db = new SchoolContext())
			{
				var langs = db.Subjects.Where(m => !string.IsNullOrEmpty(m.StudyLanguage)).Select(m => m.StudyLanguage).ToList();

				//if (!langs.Any())
					langs.AddRange(new List<string> { "Français", "Anglais", "Arabe" });
				return langs.Distinct().ToList();
			}
		}


        /// <summary>
        /// Ajouter une speciality pour un staff
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <param name="subjectGuid"></param>
        /// <param name="specialty"></param>
        /// <returns></returns>
        public bool SaveSpecialty (Guid staffGuid, Guid subjectGuid, bool specialty) {           
            using (var db = new SchoolContext())
            {
                var subject = db.Subjects.Find(subjectGuid);
                if (subject == null)
                    throw new InvalidOperationException("CAN_NOT_FIND_SUBJECT");

                if(specialty)
                    db.Staffs.Find(staffGuid).Subjects.Add(subject);
                else
                    db.Staffs.Find(staffGuid).Subjects.Remove(subject);

                return db.SaveChanges() > 0;
            }                          
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetSpecialties () {
            using (var db = new SchoolContext())
                return db.Subjects.ToList().Select(s => new SubjectCard(s));
        }


        /// <summary>
        /// List des specialites d'un staff
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <returns></returns>
        public IEnumerable GetSpecialtyAs (Guid staffGuid) {
            using (var db = new SchoolContext())
                return db.Subjects.OrderBy(s=> s.Name).ToList().Select(s=> new SubjectCard(s, staffGuid));
        }


        /// <summary>
        /// List des specialites d'un staff
        /// </summary>
        /// <param name="staffGuid"></param>
        /// <returns></returns>
        public IEnumerable<SubjectCard> GetStaffSpecialty (Guid staffGuid) {
            using (var db = new SchoolContext())
                return db.Staffs.Find(staffGuid).Subjects.OrderBy(s => s.Name).ToList().Select(s => new SubjectCard(s, staffGuid));
        }


        /// <summary>
        /// les Instructeurs d'une Matiere
        /// </summary>
        /// <param name="subjectGuid"></param>
        /// <returns></returns>
        public IEnumerable GetSubjectStaffs (Guid subjectGuid) {
            using (var db = new SchoolContext())
                return db.Staffs.Where(s => s.Subjects.Any(m => m.SubjectGuid == subjectGuid)).Include(s=> s.Person).ToList();
        }



        #region Internal Static

        internal static Subject StaticGetMatiereById(Guid matiereId)
        {
            using (var db = new SchoolContext())
                return db.Subjects.Find(matiereId);
        }


        internal static bool StaticIsSpecialty (Guid staffGuid, Guid subjectGuid) {
            using (var db = new SchoolContext())
                return db.Staffs.Find(staffGuid).Subjects.Any(s => s.SubjectGuid == subjectGuid);
        }




        #endregion

       
    }
}
