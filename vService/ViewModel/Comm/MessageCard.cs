using System;
using CLib;
using Common.Comm.Entity;

namespace DataService.ViewModel.Comm {

    /// <summary>
    /// Chat, Message
    /// </summary>
    public class MessageCard {

        /// <summary>
        /// Chat
        /// </summary>
        /// <param name="message"></param>
        public MessageCard(Message message)
        {
            MessageGuid= message.MessageGuid;
            SenderName  = message.Sender.FullName;
            SenderPhoto =message.Sender.PhotoIdentity;
            Body        =message.Body;
            DateString  = DateTimeHelper.Friendly(message.DateAdded.GetValueOrDefault()) ;
        }


        /// <summary>
        /// La cle du Chat
        /// </summary>
        public Guid MessageGuid { get; set; }

        /// <summary>
        /// La Nom du L'expediteur
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// La photo du L'expediteur
        /// </summary>
        public byte[] SenderPhoto { get; set; }

        /// <summary>
        /// Le Message
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// La date de l'envoi
        /// </summary>
        public string DateString { get; set; }

    }
}
