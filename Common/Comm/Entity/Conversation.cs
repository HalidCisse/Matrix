using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using Common.Comm.Enums;
using Common.Shared.Entity;

namespace Common.Comm.Entity {


    /// <summary>
    /// Message Privé, Email, Multicast ou Annonce
    /// </summary>
    public class Conversation:Tracable {


        /// <summary>
        /// MessageGuid
        /// </summary>
        [Key]
        public Guid MessageGuid { get; set; }

        /// <summary>
        /// Emitteur
        /// </summary>
        public Guid? SenderGuid { get; set; }

        /// <summary>
        /// Destinateur
        /// </summary>
        public Guid? RecipientGuid { get; set; }

        /// <summary>
        /// Email Destinateur
        /// </summary>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Suject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Content du Message
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Attachement du Message
        /// </summary>
        public Guid? AttachementGuid { get; set; }

        /// <summary>
        /// Personal, ToClasse, ToStudents, ToStaffs
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Est Lue
        /// </summary>
        public bool IsRead { get; set; }




        /// <summary>
        /// La personne Expediteur
        /// </summary>
        [ForeignKey("SenderGuid")]
        public virtual Person Sender { get; set; }
        
        /// <summary>
        /// Attachement du Message
        /// </summary>
        [ForeignKey("AttachementGuid")]
        public virtual Document Attachement { get; set; }


        ///// <summary>
        ///// La personne Destinateur
        ///// </summary>
        //[ForeignKey("RecipientGuid")]
        //public virtual Person Recipient { get; set; }

        ///// <summary>
        ///// Les attachements du Message
        ///// </summary>
        //public virtual ICollection<Document> Attachements { get; set; }= new HashSet<Document>();

    }
}
