using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ET;

namespace DataAccess
{
    public class DataManager : CommonInterface
    {

        #region Constructeur
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static volatile DataManager _Instance;

        /// <summary>
        /// Singleton Lock
        /// </summary>
        private static object locker = new Object();

        private CommonInterface _dataAccess;

        /// <summary>
        /// The connexion string
        /// </summary>
        private String _connexionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Halid\Documents\Visual Studio 2013\Projects\Matrix\Data\MatrixDB.mdf;Integrated Security=True";

        public static DataProvider PROVIDER;

        /// <summary>
        /// Default constructor
        /// </summary>
        private DataManager ( DataProvider provider )
        {
            switch (provider)
            {
                case DataProvider.EFR:
                    _dataAccess = new EFR();
                    break;
                case DataProvider.ORACLE:
                    break;
                case DataProvider.MSACCESS:
                    break;
                case DataProvider.SQLSERVER:
                    _dataAccess = new SQLServer(_connexionString);
                    break;
            }
        }

        /// <summary>
        /// Singleton property
        /// </summary>
        public static DataManager Instance
        {
            get {
                if (_Instance == null)
                {
                    lock (locker)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new DataManager (PROVIDER);
                        }
                    }
                }
                return _Instance;
            }
        }

        /// <summary>
        /// Dal property
        /// </summary>
        public CommonInterface dataAccess
        {
            get { return _dataAccess; }
            set { _dataAccess = value; }
        }

        #endregion













        #region Implementation
        public List<Student> GetAllStudents ( )
        {
            return _dataAccess.GetAllStudents ();
        }
        public Student GetStudentByGUID ( Guid guid )
        {
            return _dataAccess.GetStudentByGUID (guid);
        }
        public void SaveStudent ( Student student )
        {
             MessageBox.Show ("Inside data");
            _dataAccess.SaveStudent (student);
        }

        public void AddStudent ( Student student )
        {
            MessageBox.Show ("Inside data addStudent");
            _dataAccess.AddStudent (student);
        }

        public void UpdateStudent ( Student student )
        {
            MessageBox.Show ("Inside data UpdateStudent");
            _dataAccess.UpdateStudent (student);
        }


        #endregion



        
    }
}
