using System;
using System.Drawing;
using System.Text;
using System.Windows.Media;

namespace ET
{
    public class Student
    {

        #region Constructeurs
        public Student() {}

        public Student ( 
            Guid GUID, 
            string MATRICULE,
            string NOM,
            string PRENOM, 
            string CIVILITE, 
            Image PHOTO_IDENTITE, 
            string NUMERO_IDENTITE, 
            string TYPE_ID,  
            DateTime DATE_NAISSANCE, 
            string NATIONALITE,
            string LIEU_NAISSANCE, 
            string NUMERO_TEL, 
            string ADRESS_EMAIL, 
            string ADRESS_DOMICILE, 
            string NOM_TUTEUR, 
            string PRENOM_TUTEUR,  
            string NUMERO_TEL_TUTEUR, 
            string ADRESS_EMAIL_TUTEUR,
            string ADRESS_DOMICIL_TUTEUR,
            string STATUT, 
            DateTime  DATE_REGISTRATION

            ) {
            this.GUID = GUID;
            this.MATRICULE = MATRICULE;
            this.NOM = NOM;
            this.PRENOM = PRENOM;
            this.CIVILITE = CIVILITE;
            this.PHOTO_IDENTITE = PHOTO_IDENTITE;
            this.NUMERO_IDENTITE = NUMERO_IDENTITE;
            this.TYPE_ID = TYPE_ID;
            this.DATE_NAISSANCE = DATE_NAISSANCE;
            this.NATIONALITE = NATIONALITE;
            this.LIEU_NAISSANCE = LIEU_NAISSANCE;
            this.NUMERO_TEL = NUMERO_TEL;
            this.ADRESS_EMAIL = ADRESS_EMAIL;
            this.ADRESS_DOMICILE = ADRESS_DOMICILE;
            this.NOM_TUTEUR = NOM_TUTEUR;
            this.PRENOM_TUTEUR = PRENOM_TUTEUR;
            this.NUMERO_TEL_TUTEUR = NUMERO_TEL_TUTEUR;
            this.ADRESS_EMAIL_TUTEUR = ADRESS_EMAIL_TUTEUR;
            this.ADRESS_DOMICIL_TUTEUR = ADRESS_DOMICIL_TUTEUR;
            this.STATUT = STATUT;
            this.DATE_REGISTRATION = DATE_REGISTRATION;
        }

        #endregion

        #region Methodes
        public Guid GUID { get; set; }

        public string MATRICULE { get; set; }

        public string NOM { get; set; }

        public string PRENOM { get; set; }

        public string CIVILITE { get; set; }

        public Image PHOTO_IDENTITE { get; set; }

        public string NUMERO_IDENTITE { get; set; }

        public string TYPE_ID { get; set; }

        public DateTime DATE_NAISSANCE { get; set; }

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

        public DateTime DATE_REGISTRATION { get; set; }

        public override string ToString ( )
        {
            var sb = new StringBuilder ();
            sb.Append (PRENOM).Append (" ").Append (NOM);
            return sb.ToString ();
        }

        #endregion






    }
}
