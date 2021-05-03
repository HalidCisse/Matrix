using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using Common.Shared.Entity;

namespace Common.Comm.Entity {

    /// <summary>
    /// Message, Talk
    /// </summary>
    public class Message:Tracable {

        /// <summary>
        /// MessageGuid
        /// </summary>
        [Key]
        public Guid MessageGuid { get; set; }

        /// <summary>
        /// Emitteur
        /// </summary>
        public Guid SenderGuid { get; set; }

        /// <summary>
        /// Le Chat
        /// </summary>
        public Guid ChatGuid { get; set; }
        
        /// <summary>
        /// Content du Message
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Attachement du Message
        /// </summary>
        public Guid? AttachementGuid { get; set; }
       
       


        /// <summary>
        /// La personne Expediteur
        /// </summary>
        [ForeignKey("SenderGuid")]
        public virtual Person Sender { get; set; }

        /// <summary>
        /// La personne Expediteur
        /// </summary>
        [ForeignKey("ChatGuid")]
        public virtual Chat Chat { get; set; }

        /// <summary>
        /// Attachement du Message
        /// </summary>
        [ForeignKey("AttachementGuid")]
        public virtual Document Attachement { get; set; }
       
        /// <summary>
        /// Les Personnes qui ont lit le message 
        /// </summary>
        public virtual ICollection<Person> Readers { get; set; }= new HashSet<Person>();

    }
}
