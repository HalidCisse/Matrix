using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CLib.Database;
using CLib.Enums;

namespace Common.Shared.Entity {

    /// <summary>
    /// Document d'une personne
    /// </summary>
    public class Document: Tracable {

        /// <summary>
        /// Document, IsDocumentFile
        /// </summary>
        [Key]
        public Guid DocumentGuid { get; set; }

        /// <summary>
        /// Guid de la personne Proprietaire
        /// </summary>
        public Guid PersonGuid { get; set; }

        /// <summary>
        /// Document
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type de Document
        /// </summary>
        public DocumentType FileType { get; set; }

        /// <summary>
        /// Les donnee Du Document
        /// </summary>
        public byte[] DataBytes { get; set; }



        /// <summary>
        /// La personne Associer
        /// </summary>
        [ForeignKey("PersonGuid")]
        public virtual Person Person { get; set; }

    }
}
