using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.Drawing;
//using Entities;
//using Business;


namespace Matrix.Controls
{
    /// <summary>
    /// Interaction logic for StudentsList.xaml
    /// </summary>
    public partial class StudentsList
    {
        public StudentsList ( ) {
            InitializeComponent ();

            Studentslist.ItemsSource = GetStudentList ();
        }

        
        private IEnumerable<Student> GetStudentList ( ) {

            List<Student> EmpList = new List<Student> ();

            EmpList.Add (new Student () { NOM = "Halid", PRENOM = "Cisse", PHOTO_IDENTITE = GetImageFromFile ("Images/ico1.png") });
            EmpList.Add (new Student () { NOM = "Ousou Djounou", PRENOM = "CMO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico2.png") });
            EmpList.Add (new Student () { NOM = "Amad Cisse", PRENOM = "CTO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico3.png") });
            EmpList.Add (new Student () { NOM = "Keita Assouma", PRENOM = "TCM", PHOTO_IDENTITE = GetImageFromFile ("Images/ico4.png") });
            EmpList.Add (new Student () { NOM = "Bayogo Madou", PRENOM = "NMO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico5.png") });
            EmpList.Add (new Student () { NOM = "Maiga Ouamr", PRENOM = "YHN", PHOTO_IDENTITE = GetImageFromFile ("Images/ico6.png") });
            EmpList.Add (new Student () { NOM = "Toure Kemera", PRENOM = "RTE", PHOTO_IDENTITE = GetImageFromFile ("Images/ico7.png") });
            EmpList.Add (new Student () { NOM = "Diallo Adam", PRENOM = "VCI", PHOTO_IDENTITE = GetImageFromFile ("Images/ico8.png") });
            EmpList.Add (new Student () { NOM = "Barry Alhassane", PRENOM = "OKM", PHOTO_IDENTITE = GetImageFromFile ("Images/ico9.png") });
            EmpList.Add (new Student () { NOM = "Cisse Aicha", PRENOM = "LLK", PHOTO_IDENTITE = GetImageFromFile ("Images/ico10.png") });
            EmpList.Add (new Student () { NOM = "Halid", PRENOM = "CEO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico1.png") });
            EmpList.Add (new Student () { NOM = "Ousou", PRENOM = "CMO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico2.png") });
            EmpList.Add (new Student () { NOM = "Amad", PRENOM = "CTO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico3.png") });
            EmpList.Add (new Student () { NOM = "Keita", PRENOM = "TCM", PHOTO_IDENTITE = GetImageFromFile ("Images/ico4.png") });
            EmpList.Add (new Student () { NOM = "Bayogo", PRENOM = "NMO", PHOTO_IDENTITE = GetImageFromFile ("Images/ico5.png") });
            EmpList.Add (new Student () { NOM = "Maiga", PRENOM = "YHN", PHOTO_IDENTITE = GetImageFromFile ("Images/ico6.png") });
            EmpList.Add (new Student () { NOM = "Toure", PRENOM = "RTE", PHOTO_IDENTITE = GetImageFromFile ("Images/ico7.png") });
            EmpList.Add (new Student () { NOM = "Diallo", PRENOM = "VCI", PHOTO_IDENTITE = GetImageFromFile ("Images/ico8.png") });
            EmpList.Add (new Student () { NOM = "Barry", PRENOM = "OKM", PHOTO_IDENTITE = GetImageFromFile ("Images/ico9.png") });
            EmpList.Add (new Student () { NOM = "Cisse", PRENOM = "LLK", PHOTO_IDENTITE = GetImageFromFile ("Images/ico10.png") });

            return EmpList;
        }


        public class Student {

            private string _NOM;
            private string _PRENOM;
            private System.Drawing.Image _PHOTO_IDENTITE;

            public string NOM {
              set;
              get;
            }
            public string PRENOM
            {
                set;
                get;
            }
            public System.Drawing.Image PHOTO_IDENTITE
            {
                set;
                get;
            }

        }






        public System.Drawing.Image GetImageFromFile ( string path ) {
            System.Drawing.Image img = System.Drawing.Image.FromFile (path);
            return img;
        }


    }
}
