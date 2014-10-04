using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using ET;


namespace DataAccess
{
    public class SQLServer : CommonInterface
    {

        #region Constructeur

        private String _connectionString;

        public String ConnectionString {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        
        public SQLServer ( string connectionString ) {
            ConnectionString = connectionString;
        }

        #endregion


        #region Helper

        private DataTable GetDataTable ( string SQLrequete ) {
            var results = new DataTable ();
            using(var sqlConnection = new SqlConnection (ConnectionString)) {
                var sqlCommand = new SqlCommand (SQLrequete, sqlConnection);
                var sqlDataAdapter = new SqlDataAdapter (sqlCommand);
                sqlDataAdapter.Fill (results);
            }
            return results;
        }

        private void ExecureNonQueryRequest ( SqlConnection sqlConnection, string SQLrequest )
        {
            var sqlCommand = new SqlCommand (SQLrequest, sqlConnection);
            var sqlDataAdapter = new SqlDataAdapter (sqlCommand);

            sqlCommand.ExecuteNonQuery ();
        }

        public DataTable GetStudentsTable ( ) {
            return GetDataTable ("select * from dbo.ETUDIANTS");
        }

        #endregion


        #region Implementations SQLServeur

        public List<Student> GetAllStudents ( )
        {
            var Students = new List<Student> ();
            DataTable table = GetStudentsTable ();
            foreach(DataRow col in table.Rows)
            {
                Guid GUID =                    new Guid(col["GUID"].ToString());
                string MATRICULE =             col["MATRICULE"].ToString ();
                string NOM =                   col["NOM"].ToString ();
                string PRENOM =                col["PRENOM"].ToString ();
                string CIVILITE =              col["CIVILITE"].ToString ();
                Image PHOTO_IDENTITE =         DBHelper.ByteArrayToImage((byte[])col["PHOTO_IDENTITE"]);
                string NUMERO_ID =             col["NUMERO_ID"].ToString ();
                string TYPE_ID =               col["TYPE_ID"].ToString ();
                DateTime DATE_NAISSANCE =      DateTime.Parse(col["DATE_NAISSANCE"].ToString ());
                string NATIONALITE =           col["NATIONALITE"].ToString ();
                string LIEU_NAISSANCE =        col["LIEU_NAISSANCE"].ToString ();
                string NUMERO_TEL =            col["NUMERO_TEL"].ToString ();
                string ADRESS_EMAIL =          col["ADRESS_EMAIL"].ToString ();
                string ADRESS_DOMICILE =       col["ADRESS_DOMICILE"].ToString ();
                string NOM_TUTEUR =            col["NOM_TUTEUR"].ToString ();
                string PRENOM_TUTEUR =         col["PRENOM_TUTEUR"].ToString ();
                string NUMERO_TEL_TUTEUR =     col["NUMERO_TEL_TUTEUR"].ToString ();
                string ADRESS_EMAIL_TUTEUR =   col["ADRESS_EMAIL_TUTEUR"].ToString ();
                string ADRESS_DOMICIL_TUTEUR = col["ADRESS_DOMICIL_TUTEUR"].ToString ();
                string STATUT =                col["STATUT"].ToString ();
                DateTime DATE_REGISTRATION =   DateTime.Parse(col["DATE_REGISTRATION"].ToString ());
            
                Students.Add (
                                new Student (
                                             GUID, MATRICULE,                                            
                                             NOM, PRENOM,                                             
                                             CIVILITE, PHOTO_IDENTITE,                                             
                                             NUMERO_ID, TYPE_ID,                                               
                                             DATE_NAISSANCE, NATIONALITE,                                             
                                             LIEU_NAISSANCE, NUMERO_TEL,                                             
                                             ADRESS_EMAIL, ADRESS_DOMICILE,                                              
                                             NOM_TUTEUR, PRENOM_TUTEUR,                                               
                                             NUMERO_TEL_TUTEUR, ADRESS_EMAIL_TUTEUR,                                             
                                             ADRESS_DOMICIL_TUTEUR, STATUT, DATE_REGISTRATION                                                                                          
                                             )
                                     );
              }
            return Students;
        }

        public Student GetStudentByGUID ( Guid inGuid )
        {
            Student StudentOut = null;
            DataTable table = GetDataTable ("select * from ETUDIANTS where GUID = '" + inGuid + "';");

            if(table.Rows.Count == 1)
            {
                DataRow col = table.Rows[0];

                Guid GUID =                    new Guid (col["GUID"].ToString ());
                string MATRICULE =             col["MATRICULE"].ToString ();
                string NOM =                   col["NOM"].ToString ();
                string PRENOM =                col["PRENOM"].ToString ();
                string CIVILITE =              col["CIVILITE"].ToString ();
                Image PHOTO_IDENTITE =         DBHelper.ByteArrayToImage ((byte[])col["PHOTO_IDENTITE"]);
                string NUMERO_ID =             col["NUMERO_ID"].ToString ();
                string TYPE_ID =               col["TYPE_ID"].ToString ();
                DateTime DATE_NAISSANCE =      DateTime.Parse (col["DATE_NAISSANCE"].ToString ());
                string NATIONALITE =           col["NATIONALITE"].ToString ();
                string LIEU_NAISSANCE =        col["LIEU_NAISSANCE"].ToString ();
                string NUMERO_TEL =            col["NUMERO_TEL"].ToString ();
                string ADRESS_EMAIL =          col["ADRESS_EMAIL"].ToString ();
                string ADRESS_DOMICILE =       col["ADRESS_DOMICILE"].ToString ();
                string NOM_TUTEUR =            col["NOM_TUTEUR"].ToString ();
                string PRENOM_TUTEUR =         col["PRENOM_TUTEUR"].ToString ();
                string NUMERO_TEL_TUTEUR =     col["NUMERO_TEL_TUTEUR"].ToString ();
                string ADRESS_EMAIL_TUTEUR =   col["ADRESS_EMAIL_TUTEUR"].ToString ();
                string ADRESS_DOMICIL_TUTEUR = col["ADRESS_DOMICIL_TUTEUR"].ToString ();
                string STATUT =                col["STATUT"].ToString ();
                DateTime DATE_REGISTRATION =   DateTime.Parse (col["DATE_REGISTRATION"].ToString ());

                StudentOut = new Student (
                                        GUID,MATRICULE,                                        
                                        NOM,PRENOM,                                        
                                        CIVILITE, PHOTO_IDENTITE,                                        
                                        NUMERO_ID, TYPE_ID,                                       
                                        DATE_NAISSANCE, NATIONALITE,                                        
                                        LIEU_NAISSANCE, NUMERO_TEL,                                        
                                        ADRESS_EMAIL, ADRESS_DOMICILE,                                        
                                        NOM_TUTEUR, PRENOM_TUTEUR,                                        
                                        NUMERO_TEL_TUTEUR, ADRESS_EMAIL_TUTEUR,
                                        ADRESS_DOMICIL_TUTEUR, STATUT, DATE_REGISTRATION
                                        );
            }
            return StudentOut;
        }

        public void SaveStudent ( Student Mystudent ) {
           
            var table = GetDataTable ("select * from ETUDIANTS where GUID = '" + Mystudent.GUID + "';");

            if(table.Rows.Count == 0) {    // Add New Student  

                DataRow col = table.NewRow ();

                MessageBox.Show ("Inside adding");

                col["GUID"]  =                 new Guid();
                col["MATRICULE"] =             Mystudent.MATRICULE;
                col["NOM"] =                   Mystudent.NOM;
                col["PRENOM"] =                Mystudent.PRENOM;
                col["CIVILITE"] =              Mystudent.CIVILITE;
                //col["PHOTO_IDENTITE"] =        DBHelper.ImageToByteArray (Mystudent.PHOTO_IDENTITE);
                col["NUMERO_ID"] =             Mystudent.NUMERO_IDENTITE;
                col["TYPE_ID"] =               Mystudent.TYPE_ID;
                col["DATE_NAISSANCE"] =        Mystudent.DATE_NAISSANCE;
                col["NATIONALITE"] =           Mystudent.NATIONALITE;
                col["LIEU_NAISSANCE"] =        Mystudent.LIEU_NAISSANCE;
                col["NUMERO_TEL"] =            Mystudent.NUMERO_TEL;
                col["ADRESS_EMAIL"] =          Mystudent.ADRESS_EMAIL;
                col["ADRESS_DOMICILE"] =       Mystudent.ADRESS_DOMICILE;
                col["NOM_TUTEUR"] =            Mystudent.NOM_TUTEUR;
                col["PRENOM_TUTEUR"] =         Mystudent.PRENOM_TUTEUR;
                col["NUMERO_TEL_TUTEUR"] =     Mystudent.NUMERO_TEL_TUTEUR;
                col["ADRESS_EMAIL_TUTEUR"] =   Mystudent.ADRESS_EMAIL_TUTEUR;
                col["ADRESS_DOMICIL_TUTEUR"] = Mystudent.ADRESS_DOMICIL_TUTEUR;
                col["STATUT"] =                Mystudent.STATUT;
                col["DATE_REGISTRATION"] =     Mystudent.DATE_REGISTRATION;

                table.AcceptChanges();

            } else {    //Update Student Informations

                DataRow col = table.Rows[0];

                MessageBox.Show ("Inside update");

                col["MATRICULE"] =             Mystudent.MATRICULE;
                col["NOM"] =                   Mystudent.NOM;
                col["PRENOM"] =                Mystudent.PRENOM;
                col["CIVILITE"] =              Mystudent.CIVILITE;
                col["PHOTO_IDENTITE"] =        DBHelper.ImageToByteArray (Mystudent.PHOTO_IDENTITE);
                col["NUMERO_ID"] =             Mystudent.NUMERO_IDENTITE;
                col["TYPE_ID"] =               Mystudent.TYPE_ID;
                col["DATE_NAISSANCE"] =        Mystudent.DATE_NAISSANCE;
                col["NATIONALITE"] =           Mystudent.NATIONALITE;
                col["LIEU_NAISSANCE"] =        Mystudent.LIEU_NAISSANCE;
                col["NUMERO_TEL"] =            Mystudent.NUMERO_TEL;
                col["ADRESS_EMAIL"] =          Mystudent.ADRESS_EMAIL;
                col["ADRESS_DOMICILE"] =       Mystudent.ADRESS_DOMICILE;
                col["NOM_TUTEUR"] =            Mystudent.NOM_TUTEUR;
                col["PRENOM_TUTEUR"] =         Mystudent.PRENOM_TUTEUR;
                col["NUMERO_TEL_TUTEUR"] =     Mystudent.NUMERO_TEL_TUTEUR;
                col["ADRESS_EMAIL_TUTEUR"] =   Mystudent.ADRESS_EMAIL_TUTEUR;
                col["ADRESS_DOMICIL_TUTEUR"] = Mystudent.ADRESS_DOMICIL_TUTEUR;
                col["STATUT"] =                Mystudent.STATUT;
                
                table.AcceptChanges ();
            }

            
            

        }



        #endregion





        public void AddStudent ( Student student )
        {
            throw new NotImplementedException ();
        }

        public void UpdateStudent ( Student student )
        {
            throw new NotImplementedException ();
        }
    }
}

