using System;
using System.ComponentModel.DataAnnotations;
using CLib.Database;

namespace Common.Security.Enums {

    /// <summary>
    /// Espace Utilisateur
    /// </summary>
    public class USpace:Tracable {


        /// <summary>
        /// UserGuid
        /// </summary>
        [Key]
        public Guid USpaceGuid { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public UserSpace UserSpace { get; set; }

    }
}
