using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Utils;
using Microsoft.Win32;

namespace Matrix.views.Students
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class StudentInfo
    {
        private readonly bool _isAdd;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentToDisplayId"></param>
        public StudentInfo (string studentToDisplayId = null )
        {
            InitializeComponent ();

            if (!string.IsNullOrEmpty(studentToDisplayId)) { _isAdd = true;}
            
            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(GetPatternData));
            }).ContinueWith(delegate
            {
                if (_isAdd)
                {
                    DisplayDefault();
                }
                else
                {
                    DisplayStudent(App.DataS.Students.GetStudentById(studentToDisplayId));
                }
            }).Start();            
        }

        private void GetPatternData()
        {
            TITLE_.ItemsSource = App.DataS.DataEnums.GetTitles();

            Nationality.ItemsSource = App.DataS.DataEnums.GetNationalities();

            BirthPlace.ItemsSource = App.DataS.DataEnums.GetBIRTH_PLACE();

            Statut.ItemsSource = App.DataS.DataEnums.GetStudentStatuts();
        }

        private void PhotoID_Click ( object sender, RoutedEventArgs e )
        {
            var openFileDialog = new OpenFileDialog ();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true) {
                if(string.IsNullOrEmpty (openFileDialog.FileName)) return;

                PhotoIdentity.Source = new BitmapImage (new Uri (openFileDialog.FileName));
            }           
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {          
            Close ();          
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e ) {

            if (ChampsValidated() != true) return;           

            var myStudent = new Student
            {               
                StudentId = StudentId.Text.Trim (),
                Title = TITLE_.SelectedValue.ToString (),
                FirstName = Firstname.Text.Trim (),
                LastName = Lastname.Text.Trim (),
                PhotoIdentity = ImageUtils.GetPngFromImageControl(PhotoIdentity.Source as BitmapImage),                                              
                IdentityNumber = IdentityNumber.Text.Trim (),
                BirthDate = BirthDate.SelectedDate,
                Nationality = Nationality.Text,
                BirthPlace = BirthPlace.Text,
                PhoneNumber = PhoneNumber.Text.Trim (),
                EmailAdress = EmailAdress.Text.Trim (),
                HomeAdress = HomeAdress.Text.Trim (),
                Statut = Statut.SelectedValue.ToString (),                                          
            };

            if (_isAdd)
            {
                try
                {
                    App.DataS.Students.AddStudent(myStudent);
                }
                catch (Exception ex)
                {

                    ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
                    return;
                }
                ModernDialog.ShowMessage("Ajouter Avec Success", "Matrix", MessageBoxButton.OK);
                Close();
            }
            else
            {
                try
                {
                    App.DataS.Students.UpdateStudent(myStudent);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
                    return;
                }
                ModernDialog.ShowMessage("Modifier Avec Success", "Matrix", MessageBoxButton.OK);
                Close();
            }            
        }

        private void DisplayStudent (Student studentToDisplay = null)
        {
            if (studentToDisplay == null) return;

            StudentId.Text = studentToDisplay.StudentId;
            StudentId.IsEnabled = false;
            TITLE_.SelectedValue = studentToDisplay.Title;
            Firstname.Text = studentToDisplay.FirstName;
            Lastname.Text = studentToDisplay.LastName;
            PhotoIdentity.Source = ImageUtils.DecodePhoto(studentToDisplay.PhotoIdentity);         
            IdentityNumber.Text = studentToDisplay.IdentityNumber;
            BirthDate.SelectedDate = studentToDisplay.BirthDate;
            Nationality.SelectedValue = studentToDisplay.Nationality;
            BirthPlace.SelectedValue = studentToDisplay.BirthPlace;
            PhoneNumber.Text = studentToDisplay.PhoneNumber;
            EmailAdress.Text = studentToDisplay.EmailAdress;
            HomeAdress.Text = studentToDisplay.HomeAdress;
            Statut.SelectedValue = studentToDisplay.Statut;
            Enregistrer.Content = "Modifier";
        }

        private void DisplayDefault()
        {
            StudentId.Text = GenNewStudentId();
            TITLE_.SelectedIndex = 0;
            PhotoIdentity.Source = PhotoIdentity.Source = new BitmapImage (new Uri (Res.GetRes ("Portrait/defaultStudent.png")));
            PhoneNumber.Text =  "+00";
            BirthDate.SelectedDate = DateTime.Today.AddDays(-7000);
            Nationality.SelectedIndex = 0;
            BirthPlace.SelectedIndex = 0;
            Statut.SelectedIndex = 0;
        }

        private static string GenNewStudentId()
        {
            string idOut;

            do idOut = "M" + DateTime.Today.Year + GenId.GetId(4); while (App.DataS.Students.StudentExist(idOut));

            return idOut;
        }

        private bool ChampsValidated ( )
        {

            var ok = true;

            if(_isAdd)
            {
                if (string.IsNullOrEmpty(StudentId.Text))
                {                
                    StudentId.BorderBrush = Brushes.Red;
                    ok = false;
                }
                else if (App.DataS.Students.StudentExist(StudentId.Text.Trim()))
                {
                    MessageBox.Show("Ce numero de Matricule Est Deja Utiliser par " +
                                    App.DataS.Students.GetStudentName(StudentId.Text.Trim()));
                    return false;
                }
                else
                {
                    StudentId.BorderBrush = Brushes.Blue;
                }               
            }

            if(string.IsNullOrEmpty (Firstname.Text))
            {               
                Firstname.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                Firstname.BorderBrush = Brushes.Blue;
            }  

            if(string.IsNullOrEmpty (Lastname.Text))
            {              
                Lastname.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                Lastname.BorderBrush = Brushes.Blue;
            }  

            if(string.IsNullOrEmpty (IdentityNumber.Text))
            {               
                IdentityNumber.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                IdentityNumber.BorderBrush = Brushes.Blue;
            }  
            
            if (string.IsNullOrEmpty (Nationality.Text))
            {             
                Nationality.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                Nationality.BorderBrush = Brushes.Blue;
            }  
            if (string.IsNullOrEmpty (BirthPlace.Text))
            {               
                BirthPlace.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                BirthPlace.BorderBrush = Brushes.Blue;
            }  

            PhoneNumber.BorderBrush = string.IsNullOrEmpty (PhoneNumber.Text) ? Brushes.Red : Brushes.Blue;  
            if(string.IsNullOrEmpty (EmailAdress.Text))
            {                
                EmailAdress.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                EmailAdress.BorderBrush = Brushes.Blue;
            }  
            if(string.IsNullOrEmpty (HomeAdress.Text))
            {               
                HomeAdress.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                HomeAdress.BorderBrush = Brushes.Blue;
            }  
            
            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return ok; 
        }
        
    }
}
