using System.Windows;
using DataAccess;
using ET;

namespace Manager
{
    public class Manager : CommonInterface
    {

        #region Constructeur



        public DataManager DataManager { get; set; }

        public Manager() {

             DataManager.PROVIDER = DataProvider.EFR;
            DataManager = DataManager.Instance;
        }



        #endregion









        #region Implementations

        public void SaveStudent ( Student student )
        {
            MessageBox.Show ("Inside Bussiness");

            DataManager.SaveStudent (student);
        }

        public System.Collections.Generic.List<Student> GetAllStudents ( )
        {
            throw new System.NotImplementedException ();
        }

        public Student GetStudentByGUID ( System.Guid guid )
        {
            throw new System.NotImplementedException ();
        }

        public void AddStudent ( Student student )
        {
            MessageBox.Show ("Inside Bussiness addStudent");

            DataManager.AddStudent (student);
        }

        public void UpdateStudent ( Student student )
        {
            MessageBox.Show ("Inside Bussiness UpdateStudent");

            DataManager.UpdateStudent (student);
        }













        #endregion







        
    }
}
