using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using CLib;
using CLib.Validation;
using Common.Comm.Entity;
using Common.Comm.Enums;
using Common.Shared.Entity;
using DataService.Context;
using DataService.ViewModel.Comm;
using DataService.ViewModel.Economat;

namespace DataService.DataManager {

    /// <summary>
    /// Manager des Messages, Annonces, Emails
    /// </summary>
    public sealed class CommManager {


        #region CRUD Message


        /// <summary>
        /// Envoyer Un Chat
        /// </summary>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        public bool PushChat (Message newMessage) {
            using (var db = new SchoolContext()) {
                if(db.Set<Person>().Find(newMessage.SenderGuid)==null)
                    throw new InvalidOperationException("SENDER_REFERENCE_NOT_FOUND");
                if(newMessage.MessageGuid==Guid.Empty)
                    newMessage.MessageGuid=Guid.NewGuid();
                if(db.Set<Chat>().Find(newMessage.ChatGuid)==null)
                    throw new InvalidOperationException("CONVERSATION_REFERENCE_NOT_FOUND");

                newMessage.DateAdded        =DateTime.Now;
                newMessage.AddUserGuid      = Guid.Empty;
                newMessage.LastEditUserGuid = Guid.Empty;
                newMessage.LastEditDate     =DateTime.Now;
                
                if (newMessage.Attachement == null) return db.SaveChanges() > 0;

                if(newMessage.Attachement.DocumentGuid==Guid.Empty)
                    newMessage.Attachement.DocumentGuid=Guid.NewGuid();
                db.Set<Document>().Add(newMessage.Attachement);
                db.Set<Message>().Add(newMessage);

                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Creer Nouvelle Conversation
        /// </summary>
        /// <param name="newChat"></param>
        /// <returns></returns>
        public bool SaveChat (Chat newChat) {
            using (var db = new SchoolContext()) {

                if(newChat.Persons.Count < 2)
                    throw new InvalidOperationException("CONVERSATION_MUST_HAVE_AT_LEAST_TWO_PERSONS");

                if(newChat.ChatGuid==Guid.Empty)
                    newChat.ChatGuid=Guid.NewGuid();

                newChat.DateAdded        =DateTime.Now;
                newChat.AddUserGuid      =Guid.Empty;
                newChat.LastEditUserGuid =Guid.Empty;
                newChat.LastEditDate     =DateTime.Now;

                //foreach (var talker in newConversation.Persons)
                //{
                //    if(talker.PersonGuid==Guid.Empty)
                //        talker.PersonGuid=Guid.NewGuid();
                //    db.Set<Person>().Add(talker);
                //}

                foreach(var talk in newChat.Messages.Where(talk => talk.MessageGuid==Guid.Empty))
                    talk.MessageGuid=Guid.NewGuid();
                db.Set<Chat>().Add(newChat);      
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Supprimer un chat
        /// </summary>
        /// <param name="chatGuid"></param>
        /// <returns></returns>
        public bool DeleteChat (Guid chatGuid) {
            using (var db = new SchoolContext()) {
                var oldConversation = db.Set<Chat>().Find(chatGuid);
                oldConversation.IsDeleted=true;
                oldConversation.DeleteUserGuid=Guid.Empty;
                db.Set<Chat>().Attach(oldConversation);
                db.Entry(oldConversation).State=EntityState.Modified;
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Charger un chat avec ses messages
        /// </summary>
        /// <param name="chatGuid"></param>
        /// <param name="toDate"></param>
        /// <param name="maxChats"></param>
        /// <param name="markRead"></param>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public IEnumerable<MessageCard> GetMessages (Guid chatGuid, DateTime? fromDate = null, DateTime? toDate = null, int maxChats = 20, bool markRead = true) {
            //todo markRead
            using (var db = new SchoolContext()) {
                if(fromDate==null||toDate==null) {
                    fromDate=DateTime.Today;
                    toDate=DateTime.Today.AddDays(1);
                }

            return
                    db.Set<Chat>().Find(chatGuid).Messages
                                          .Where(c => c.IsDeleted&&(c.DateAdded>=fromDate&&c.DateAdded<=toDate))
                                          .OrderByDescending(c => c.DateAdded)
                                          .Take(maxChats)                       
                                          .ToList()
                                          .Select(c => new MessageCard(c));
            }
        }


        /// <summary>
        /// La liste des messages d'une personne
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="maxResult"></param>
        /// <returns></returns>
        public IEnumerable<ChatCard> GetChats (Guid personGuid, DateTime? fromDate = null, DateTime? toDate = null, int maxResult = 20) {
            using (var db = new SchoolContext()) {
                if(fromDate==null||toDate==null) {
                    fromDate=DateTime.Today;
                    toDate=DateTime.Today.AddDays(1);
                }
               
                return db.Set<Person>().Find(personGuid).Chats
                                       .Where(c=>c.IsDeleted&&(c.DateAdded>=fromDate&&c.DateAdded<=toDate))
                                       .OrderByDescending(m => m.DateAdded)
                                       .Take(maxResult)
                                       .ToList()
                                       .Select(c => new ChatCard(c))
                                       .ToList();
            }
        }


        /// <summary>
        /// Telecharger Attachement associer au message
        /// </summary>
        /// <param name="messageGuid"></param>
        /// <returns></returns>
        public Document DownloadAttachement (Guid messageGuid) {
            using (var db = new SchoolContext())
                return db.Set<Message>().Find(messageGuid).Attachement;
        }


        #endregion




        #region CRUD Message

        /// <summary>
        /// Envoyer Un Message
        /// </summary>
        /// <param name="conversation"></param>
        /// <returns></returns>
        public bool SendMessage(Conversation conversation)
        {
            using (var db = new SchoolContext()) {
                if(db.Set<Person>().Find(conversation.SenderGuid)==null)
                    throw new InvalidOperationException("SENDER_REFERENCE_NOT_FOUND");
                if (conversation.MessageGuid == Guid.Empty) conversation.MessageGuid = Guid.NewGuid();
                if(conversation.RecipientGuid==conversation.SenderGuid)
                    throw new InvalidOperationException("MESSAGE_SENDER_CAN_NOT_EGUAL_RECIPIENT");

                switch (conversation.MessageType)
                {
                    case MessageType.Personal:                      
                        if(db.Set<Person>().Find(conversation.RecipientGuid)==null)
                            throw new InvalidOperationException("RECIPIENT_REFERENCE_NOT_FOUND");                      
                        break;

                    case MessageType.Email:
                        if(string.IsNullOrEmpty(conversation.RecipientEmail))
                            conversation.RecipientEmail=db.Set<Person>().Find(conversation.RecipientGuid).EmailAdress;
                        if( !InputHelper.IsValidEmail(conversation.RecipientEmail))
                            throw new InvalidOperationException("RECIPIENT_EMAIL_INVALID");
                        


                        var newMail = new MailMessage();                        
                        newMail.To.Add(new MailAddress(conversation.RecipientEmail));
                        newMail.Subject  = conversation.Subject;
                        newMail.Body     = conversation.Content;

                        if (EmailHelper.SendMailByGmail(newMail))
                        {
                            conversation.DateAdded=DateTime.Now;
                            conversation.LastEditDate=DateTime.Now;
                            db.Conversations.Add(conversation);
                            return db.SaveChanges()>0;
                        }
                        return false;
                                                                  
                    case MessageType.ToClasse:                       
                        if(db.Classes.Find(conversation.RecipientGuid)==null)
                            throw new InvalidOperationException("CLASSE_REFERENCE_NOT_FOUND");
                        break;
                }
                
                conversation.DateAdded    =DateTime.Now;
                conversation.LastEditDate =DateTime.Now;

                db.Conversations.Add(conversation);
                if (conversation.Attachement != null)                
                   db.Set<Document>().Add(conversation.Attachement);
                return db.SaveChanges()>0;
            }
        }


        /// <summary>
        /// Supprimer un Message
        /// </summary>
        /// <param name="messageGuid"></param>
        /// <returns></returns>
        public bool DeleteMessage (Guid messageGuid) {
            using (var db = new SchoolContext())
            {
                var oldMessage = db.Conversations.Find(messageGuid);
                oldMessage.IsDeleted = true;
                oldMessage.DeleteUserGuid = Guid.Empty;
                db.Conversations.Attach(oldMessage);
                db.Entry(oldMessage).State=EntityState.Modified;
                return db.SaveChanges()>0;               
            }
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageGuid"></param>
        /// <param name="markRead"></param>
        /// <returns></returns>
        public Conversation GetMessage(Guid messageGuid, bool markRead = true)
        {
            using (var db = new SchoolContext())
            {              
                var oldMessage = db.Conversations.Find(messageGuid);
                if (oldMessage == null) return null;
               
                oldMessage.IsRead=true;
                oldMessage.LastEditDate=DateTime.Now;
                db.Conversations.Attach(oldMessage);
                db.Entry(oldMessage).State=EntityState.Modified;
                db.SaveChanges();
                return oldMessage;
            }
        }


        /// <summary>
        /// La liste des messages d'une personne
        /// </summary>
        /// <param name="personGuid"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="maxResult"></param>
        /// <returns></returns>
        public List<DataCard> GetPersonMessages(Guid personGuid, DateTime? fromDate = null, DateTime? toDate = null, int maxResult = 20){          
            using (var db = new SchoolContext())
            {
                if (fromDate == null || toDate == null)
                {
                    fromDate=DateTime.Today;
                    toDate=DateTime.Today.AddDays(1);
                }
                    
                var mesMessages = new List<Conversation>();

                mesMessages.AddRange(db.Conversations.Include(m => m.Sender).Where(m => (m.RecipientGuid==personGuid||m.SenderGuid==personGuid)&&!m.IsDeleted&&(
                                                    m.DateAdded>=fromDate&&
                                                    m.DateAdded<=toDate
                                                )));
                var isStaff = db.Staffs.Any(s => s.Person.PersonGuid == personGuid);

                if (isStaff)
                    mesMessages.AddRange(db.Conversations.Include(m => m.Sender).Where(m => m.MessageType == MessageType.ToStaffs && !m.IsDeleted &&(
                                                                    m.DateAdded >= fromDate &&
                                                                    m.DateAdded <= toDate
                                                                    )));
                else
                {
                    mesMessages.AddRange(
                        db.Conversations.Include(m => m.Sender).Where(m => m.MessageType==MessageType.ToStudents&&!m.IsDeleted&&(
                                                   m.DateAdded>=fromDate&&
                                                   m.DateAdded<=toDate
                                                   )));

                    var student = db.Students.FirstOrDefault(s => s.Person.PersonGuid == personGuid);
                    if (student == null)
                        return mesMessages.Distinct()
                                          .OrderByDescending(m => m.DateAdded)
                                          .Take(maxResult)
                                          .ToList()
                                          .Select(m => new DataCard(m, personGuid))
                                          .ToList();
                    var mesClasses =
                        EnrollementManager.GetStudentInscriptions(
                            student.StudentGuid, fromDate, toDate).Select(i=> i.ClasseGuid);
                    foreach (var classeGuid in mesClasses)
                        mesMessages.AddRange(
                            db.Conversations.Include(m=>m.Sender).Where(
                                m =>
                                    m.MessageType == MessageType.ToClasse && m.RecipientGuid == classeGuid &&
                                    !m.IsDeleted && (
                                        m.DateAdded >= fromDate &&
                                        m.DateAdded <= toDate
                                        )));
                }
                return mesMessages.Distinct()
                                  .OrderByDescending(m=> m.DateAdded)
                                  .Take(maxResult)
                                  .ToList()
                                  .Select(m=> new DataCard(m, personGuid))                                  
                                  .ToList();
            }
        }


        #endregion




        #region Helpers




        #endregion




        #region Static Internal Static




        #endregion



    }
}
