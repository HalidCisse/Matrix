//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class ETUDIANT
    {
        public System.Guid GUID { get; set; }
        public string MATRICULE { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public string CIVILITE { get; set; }
        public byte[] PHOTO_IDENTITE { get; set; }
        public string NUMERO_ID { get; set; }
        public string TYPE_ID { get; set; }
        public Nullable<System.DateTime> DATE_NAISSANCE { get; set; }
        public string NATIONALITE { get; set; }
        public string LIEU_NAISSANCE { get; set; }
        public string NUMERO_TEL { get; set; }
        public string ADRESS_EMAIL { get; set; }
        public string ADRESS_DOMICILE { get; set; }
        public string NOM_TUTEUR { get; set; }
        public string PRENOM_TUTEUR { get; set; }
        public string NUMERO_TEL_TUTEUR { get; set; }
        public string ADRESS_EMAIL_TUTEUR { get; set; }
        public string ADRESS_DOMICIL_TUTEUR { get; set; }
        public string STATUT { get; set; }
        public Nullable<System.DateTime> DATE_REGISTRATION { get; set; }
    }
}
