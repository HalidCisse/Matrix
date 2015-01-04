using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CLib;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;

namespace Matrix.views.Students
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class StudentInfo
    {
        private readonly bool _isAdd;

        private readonly Guid _studentDisplatedGuid ;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentToDisplayGuid"></param>
        public StudentInfo (string studentToDisplayGuid = null )
        {
            InitializeComponent ();

            if (string.IsNullOrEmpty(studentToDisplayGuid)) { _isAdd = true;}
            else _studentDisplatedGuid = new Guid(studentToDisplayGuid);

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetPatternData();

                    if (_isAdd) DisplayDefault();
      
                    else DisplayStudent(App.DataS.Students.GetStudentByGuid(new Guid(studentToDisplayGuid)));
                  
                }));                             
            }).Start();            
        }

        private void GetPatternData()
        {
            TITLE_.ItemsSource = App.DataS.DataEnums.GetTitles();

            NATIONALITY.ItemsSource = App.DataS.DataEnums.GetNationalities();

            BIRTH_PLACE.ItemsSource = App.DataS.DataEnums.GetBIRTH_PLACE();

            STATUT.ItemsSource = App.DataS.DataEnums.GetStudentStatuts();
        }

        private void PhotoID_Click ( object sender, RoutedEventArgs e )
        {
            var openFileDialog = new OpenFileDialog ();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true) {
                if(string.IsNullOrEmpty (openFileDialog.FileName)) return;

                PHOTO_IDENTITY.Source = new BitmapImage (new Uri (openFileDialog.FileName));
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
                StudentId = STUDENT_ID.Text.Trim (),
                Title = TITLE_.SelectedValue.ToString (),
                FirstName = FIRSTNAME.Text.Trim (),
                LastName = LASTNAME.Text.Trim (),
                PhotoIdentity = ImageUtils.GetPngFromImageControl(PHOTO_IDENTITY.Source as BitmapImage),                                              
                IdentityNumber = IDENTITY_NUMBER.Text.Trim (),
                BirthDate = BIRTH_DATE.SelectedDate,
                Nationality = NATIONALITY.Text,
                BirthPlace = BIRTH_PLACE.Text,
                PhoneNumber = PHONE_NUMBER.Text.Trim (),
                EmailAdress = EMAIL_ADRESS.Text.Trim (),
                HomeAdress = HOME_ADRESS.Text.Trim (),
                Statut = STATUT.SelectedValue.ToString (),                                          
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
                    Close();
                }
                ModernDialog.ShowMessage("Ajouter Avec Success", "Matrix", MessageBoxButton.OK);
                Close();
            }
            else
            {
                try
                {
                    myStudent.StudentGuid = _studentDisplatedGuid;
                    App.DataS.Students.UpdateStudent(myStudent);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
                    Close();
                }
                ModernDialog.ShowMessage("Modifier Avec Success", "Matrix", MessageBoxButton.OK);
                Close();
            }            
        }

        private void DisplayStudent (Student studentToDisplay = null)
        {
            if (studentToDisplay == null) return;

            STUDENT_ID.Text = studentToDisplay.StudentId;
            STUDENT_ID.IsEnabled = false;
            TITLE_.SelectedValue = studentToDisplay.Title;
            FIRSTNAME.Text = studentToDisplay.FirstName;
            LASTNAME.Text = studentToDisplay.LastName;
            PHOTO_IDENTITY.Source = ImageUtils.DecodePhoto(studentToDisplay.PhotoIdentity);         
            IDENTITY_NUMBER.Text = studentToDisplay.IdentityNumber;
            BIRTH_DATE.SelectedDate = studentToDisplay.BirthDate;
            NATIONALITY.SelectedValue = studentToDisplay.Nationality;
            BIRTH_PLACE.SelectedValue = studentToDisplay.BirthPlace;
            PHONE_NUMBER.Text = studentToDisplay.PhoneNumber;
            EMAIL_ADRESS.Text = studentToDisplay.EmailAdress;
            HOME_ADRESS.Text = studentToDisplay.HomeAdress;
            STATUT.SelectedValue = studentToDisplay.Statut;
            ENREGISTRER.Content = "Modifier";
        }

        private void DisplayDefault()
        {
            STUDENT_ID.Text = GenNewStudentId();
            TITLE_.SelectedIndex = 0;
            PHOTO_IDENTITY.Source = PHOTO_IDENTITY.Source = new BitmapImage (new Uri (Res.GetRes ("Portrait/defaultStudent.png")));
            PHONE_NUMBER.Text =  "+00";
            BIRTH_DATE.SelectedDate = DateTime.Today.AddDays(-7000);
            NATIONALITY.SelectedIndex = 0;
            BIRTH_PLACE.SelectedIndex = 0;
            STATUT.SelectedIndex = 0;
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
                if (string.IsNullOrEmpty(STUDENT_ID.Text))
                {                
                    STUDENT_ID.BorderBrush = Brushes.Red;
                    ok = false;
                }
                else if (App.DataS.Students.StudentExist(STUDENT_ID.Text.Trim()))
                {
                    MessageBox.Show("Ce numero de Matricule Est Deja Utiliser par " +
                                    App.DataS.Students.GetStudentName(STUDENT_ID.Text.Trim()));
                    return false;
                }
                else
                {
                    STUDENT_ID.BorderBrush = Brushes.Blue;
                }               
            }

            if(string.IsNullOrEmpty (FIRSTNAME.Text))
            {               
                FIRSTNAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                FIRSTNAME.BorderBrush = Brushes.Blue;
            }  

            if(string.IsNullOrEmpty (LASTNAME.Text))
            {              
                LASTNAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                LASTNAME.BorderBrush = Brushes.Blue;
            }  

            if(string.IsNullOrEmpty (IDENTITY_NUMBER.Text))
            {               
                IDENTITY_NUMBER.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                IDENTITY_NUMBER.BorderBrush = Brushes.Blue;
            }  
            
            if (string.IsNullOrEmpty (NATIONALITY.Text))
            {             
                NATIONALITY.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NATIONALITY.BorderBrush = Brushes.Blue;
            }  
            if (string.IsNullOrEmpty (BIRTH_PLACE.Text))
            {               
                BIRTH_PLACE.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                BIRTH_PLACE.BorderBrush = Brushes.Blue;
            }  

            PHONE_NUMBER.BorderBrush = string.IsNullOrEmpty (PHONE_NUMBER.Text) ? Brushes.Red : Brushes.Blue;  
            if(string.IsNullOrEmpty (EMAIL_ADRESS.Text))
            {                
                EMAIL_ADRESS.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                EMAIL_ADRESS.BorderBrush = Brushes.Blue;
            }  
            if(string.IsNullOrEmpty (HOME_ADRESS.Text))
            {               
                HOME_ADRESS.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                HOME_ADRESS.BorderBrush = Brushes.Blue;
            }  
            
            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return ok; 
        }
        
    }
}
