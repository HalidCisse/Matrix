using System;
using System.Globalization;
using CLib;
using Common.Comm.Entity;
using Common.Comm.Enums;
using Common.Economat.Entity;
using Common.Pedagogy.Entity;
using Common.Shared.Entity;
using DataService.Context;

namespace DataService.ViewModel.Economat {

    /// <summary>
    /// 
    /// </summary>
    public class DataCard {        


        /// <summary>
        /// 
        /// </summary>
        public DataCard () {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="salary"></param>
        public DataCard(Salary salary)
        {
            Info1 = salary.Designation;
            Info3 = salary.Remuneration.ToString("0.##", CultureInfo.CurrentCulture) + " dhs";
        }


        /// <summary>
        /// study
        /// </summary>
        public DataCard (Study study)
        {
            Guid  = study.StudyGuid;
            Info1 = study.Subject.Name + " (" + study.Subject.Sigle + ")";
            Info2 = study.Classe.Name  + " (" + study.Classe.ClassGrade.GetEnumDescription() + ")";
            Info3 = study.StartDate.GetValueOrDefault().ToShortDateString() + " (" + study.StartTime.ProperTimeSpan() + "-" +
                    study.EndTime.ProperTimeSpan() + ") - Salle " +study.Room;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="coursEvent"></param>
        public DataCard (CoursEvent coursEvent) {
            using (var db = new SchoolContext()) {
                var currentCous = db.Studies.Find(coursEvent.CoursGuid);

                Guid  =coursEvent.CoursGuid;
                Date1 =coursEvent.EventDate;
                Info1 =currentCous.Subject.Name+" ("+currentCous.Subject.Sigle+")";
                Info2 =currentCous.Classe.Name+" ("+currentCous.Classe.Filiere.Name+")";
                Info3 =coursEvent.EventDate.ToShortDateString()+" ("+coursEvent.StartTime.ProperTimeSpan()+"-"+
                        coursEvent.EndTime.ProperTimeSpan()+") - Salle "+currentCous.Room;
            }
        }


        /// <summary>
        /// message
        /// </summary>        
        /// <param name="conversation"></param>
        /// <param name="readerGuid"></param>
        public DataCard (Conversation conversation, Guid readerGuid)
        {
            using (var db = new SchoolContext())
            {
                if (readerGuid == conversation.SenderGuid)
                {
                    switch (conversation.MessageType)
                    {
                        case MessageType.Personal:
                            Info1 = "A : " + db.Set<Person>().Find(conversation.RecipientGuid).FullName;
                            break;
                        case MessageType.Email:
                            Info1 = "A : " + conversation.RecipientEmail;
                            break;
                        case MessageType.ToClasse:
                            Info1 = "A : " + db.Classes.Find(conversation.RecipientGuid).Name;
                            break;
                        case MessageType.ToStudents:
                            Info1 = "Aux Etudiants ";
                            break;
                        case MessageType.ToStaffs:
                            Info1 = "Aux Personnels";
                            break;
                    }
                    Info4 = "Blue";
                }
                else
                {
                    Info1="De : "+conversation.Sender.FullName;
                    Info4 = conversation.IsRead ? "Beige" : "Red";
                }


                Info2 = "Sujet : " + conversation.Subject;
                Info3 = conversation.DateAdded.GetValueOrDefault().ToShortDateString() + " - " +
                        conversation.DateAdded.GetValueOrDefault().TimeOfDay.ProperTimeSpan();
                Guid = conversation.MessageGuid;
                Date1 = conversation.DateAdded.GetValueOrDefault();
               
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="classe"></param>
        public DataCard (Classe classe) {
            Guid=classe.ClasseGuid;
            Info1=classe.Name+" ("+classe.Filiere.Name+")";            
        }



        /// <summary>
        /// Guid
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Info1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Info2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Info3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Info4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Bool1 { get; set; }


    }
}
