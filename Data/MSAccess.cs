using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;

namespace DataAccess 
{
    public class MSAccess : CommonInterface
    {



        /// <summary>
        /// Property
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="connectionString">Chaine de connexion</param>
        public MSAccess ( string connectionString )
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Construit une datatable à partir d'une requete SQL
        /// </summary>
        /// <param name="request">Requete SQL</param>
        /// <returns>Data table correspondante</returns>
        private DataTable GetDataTable ( string request )
        {
            var results = new DataTable ();
            using(var sqlConnection = new SqlConnection (ConnectionString))
            {
                var sqlCommand = new SqlCommand (request, sqlConnection);
                var sqlDataAdapter = new SqlDataAdapter (sqlCommand);
                sqlDataAdapter.Fill (results);
            }
            return results;
        }


















        public List<Student> GetAllStudents ( )
        {
            throw new NotImplementedException ();
        }








        public Student GetStudentByGUID ( Guid guid )
        {
            throw new NotImplementedException ();
        }

        public void SaveStudent ( Student student )
        {
            throw new NotImplementedException ();
        }


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
