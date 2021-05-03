using System;
using System.Linq;
using CLib;
using Common.Comm.Entity;

namespace DataService.ViewModel.Comm {

    /// <summary>
    /// Une Conversation avec une Groupe de Personnes
    /// </summary>
    public class ChatCard {

        /// <summary>
        /// default
        /// </summary>
        /// <param name="chat"></param>
        public ChatCard(Chat chat)
        {
            var lastChat = chat.Messages.OrderByDescending(c => c.DateAdded).FirstOrDefault();

            ChatGuid=chat.ChatGuid;
            LastSenderName   =lastChat?.Sender.FullName;
            LastSenderPhoto  =lastChat?.Sender.PhotoIdentity;
            LastBody         =lastChat?.Body;
            LastDateString   =(lastChat?.DateAdded.GetValueOrDefault()).Friendly();
        }

        /// <summary>
        /// La cle du Conversation
        /// </summary>
        public Guid ChatGuid { get; set; }

        /// <summary>
        /// La Nom du L'expediteur
        /// </summary>
        public string LastSenderName { get; set; }

        /// <summary>
        /// La photo du L'expediteur
        /// </summary>
        public byte[] LastSenderPhoto { get; set; }

        /// <summary>
        /// Le Message
        /// </summary>
        public string LastBody { get; set; }

        /// <summary>
        /// La date de l'envoi
        /// </summary>
        public string LastDateString { get; set; }



    }
}
