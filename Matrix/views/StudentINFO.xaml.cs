using System;
using System.Windows;
using System.Windows.Media.Imaging;
using DataService.Entities;
using Matrix.Utils;
using Microsoft.Win32;
using Brushes = System.Windows.Media.Brushes;




namespace Matrix.views
{
    
    public partial class StudentINFO
    {
        public string OpenOption;
        
        public StudentINFO (string StudentToDisplayID = null )
        {
            InitializeComponent ();

            #region Patterns Data

            TITLE_.ItemsSource = App.Db.GetTITLES();

            NATIONALITY_.ItemsSource = App.Db.GetNATIONALITIES();

            BIRTH_PLACE_.ItemsSource = App.Db.GetBIRTH_PLACE();

            STATUT_.ItemsSource = App.Db.GetStudentSTATUTS();

            #endregion

            if (!string.IsNullOrEmpty(StudentToDisplayID))
                DisplayStudent(App.Db.GetStudentByID(StudentToDisplayID));
            else 
                DisplayDefault();
        }

        private void PhotoID_Click ( object sender, RoutedEventArgs e )
        {
            var openFileDialog = new OpenFileDialog ();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true) {
                if(string.IsNullOrEmpty (openFileDialog.FileName)) return;

                PHOTO_IDENTITY_.Source = new BitmapImage (new Uri (openFileDialog.FileName));
            }           
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {          
            Close ();          
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e ) {

            if (ChampsValidated() != true) return;           

            var MyStudent = new Student
            {               
                STUDENT_ID = STUDENT_ID_.Text.Trim (),
                TITLE = TITLE_.SelectedValue.ToString (),
                FIRSTNAME = FIRSTNAME_.Text.Trim (),
                LASTNAME = LASTNAME_.Text.Trim (),
                PHOTO_IDENTITY = ImageUtils.getPNGFromImageControl(PHOTO_IDENTITY_.Source as BitmapImage),                                              
                IDENTITY_NUMBER = IDENTITY_NUMBER_.Text.Trim (),
                BIRTH_DATE = BIRTH_DATE_.SelectedDate.Value,
                NATIONALITY = NATIONALITY_.SelectedValue.ToString (),
                BIRTH_PLACE = BIRTH_PLACE_.SelectedValue.ToString (),
                PHONE_NUMBER = PHONE_NUMBER_.Text.Trim (),
                EMAIL_ADRESS = EMAIL_ADRESS_.Text.Trim (),
                HOME_ADRESS = HOME_ADRESS_.Text.Trim (),
                STATUT = STATUT_.SelectedValue.ToString (),                                          
            };

            if (OpenOption == "Add")
            {
                MyStudent.REGISTRATION_DATE = DateTime.Now.Date;
                MessageBox.Show (App.Db.AddStudent (MyStudent) ? "Add Success" : "Add Failed");
                Close ();
            }
            else
            {
                MessageBox.Show (App.Db.UpdateStudent (MyStudent) ? "Update Success" : "Update Failed");
                Close ();
            }
            
        }

        private void DisplayStudent (Student StudentToDisplay = null)
        {
            if (StudentToDisplay == null) return;

            STUDENT_ID_.Text = StudentToDisplay.STUDENT_ID;
            STUDENT_ID_.IsEnabled = false;
            TITLE_.SelectedValue = StudentToDisplay.TITLE;
            FIRSTNAME_.Text = StudentToDisplay.FIRSTNAME;
            LASTNAME_.Text = StudentToDisplay.LASTNAME;
            PHOTO_IDENTITY_.Source = ImageUtils.DecodePhoto(StudentToDisplay.PHOTO_IDENTITY);         
            IDENTITY_NUMBER_.Text = StudentToDisplay.IDENTITY_NUMBER;
            BIRTH_DATE_.SelectedDate = StudentToDisplay.BIRTH_DATE;
            NATIONALITY_.SelectedValue = StudentToDisplay.NATIONALITY;
            BIRTH_PLACE_.SelectedValue = StudentToDisplay.BIRTH_PLACE;
            PHONE_NUMBER_.Text = StudentToDisplay.PHONE_NUMBER;
            EMAIL_ADRESS_.Text = StudentToDisplay.EMAIL_ADRESS;
            HOME_ADRESS_.Text = StudentToDisplay.HOME_ADRESS;
            STATUT_.SelectedValue = StudentToDisplay.STATUT;
            Enregistrer.Content = "Modifier";
        }

        private void DisplayDefault()
        {
            STUDENT_ID_.Text = GenNewStudentID();
            TITLE_.SelectedIndex = 0;
            PHOTO_IDENTITY_.Source = PHOTO_IDENTITY_.Source = new BitmapImage (new Uri (Res.GetRes ("Portrait/defaultStudent.png")));
            PHONE_NUMBER_.Text =  "+00";
            BIRTH_DATE_.SelectedDate = DateTime.Today.AddDays(-7000);
            NATIONALITY_.SelectedIndex = 0;
            BIRTH_PLACE_.SelectedIndex = 0;
            STATUT_.SelectedIndex = 0;
        }

        private static string GenNewStudentID()
        {
            string idOut;

            do idOut = "M" + DateTime.Today.Year + GenID.GetID(4); while (App.Db.StudentExist(idOut));

            return idOut;
        }

        private bool ChampsValidated ( )
        {

            var Ok = true;


            if(OpenOption == "Add")
            {
                if (string.IsNullOrEmpty(STUDENT_ID_.Text))
                {                
                    STUDENT_ID_.BorderBrush = Brushes.Red;
                    Ok = false;
                }
                else if (App.Db.StudentExist(STUDENT_ID_.Text.Trim()))
                {
                    MessageBox.Show("Ce numero de Matricule Est Deja Utiliser par " +
                                    App.Db.GetStudentName(STUDENT_ID_.Text.Trim()));
                    return false;
                }
                else
                {
                    STUDENT_ID_.BorderBrush = Brushes.Blue;
                }               
            }

            if(string.IsNullOrEmpty (FIRSTNAME_.Text))
            {               
                FIRSTNAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                FIRSTNAME_.BorderBrush = Brushes.Blue;
            }  

            if(string.IsNullOrEmpty (LASTNAME_.Text))
            {              
                LASTNAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                LASTNAME_.BorderBrush = Brushes.Blue;
            }  

            if(string.IsNullOrEmpty (IDENTITY_NUMBER_.Text))
            {               
                IDENTITY_NUMBER_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                IDENTITY_NUMBER_.BorderBrush = Brushes.Blue;
            }  
            
            if (NATIONALITY_.SelectedValue == null)
            {             
                NATIONALITY_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                NATIONALITY_.BorderBrush = Brushes.Blue;
            }  
            if (BIRTH_PLACE_.SelectedValue == null)
            {               
                BIRTH_PLACE_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                BIRTH_PLACE_.BorderBrush = Brushes.Blue;
            }  
            if(string.IsNullOrEmpty (PHONE_NUMBER_.Text))
            {               
                PHONE_NUMBER_.BorderBrush = Brushes.Red;
                //Ok = false;
            }
            else
            {
                PHONE_NUMBER_.BorderBrush = Brushes.Blue;
            }  
            if(string.IsNullOrEmpty (EMAIL_ADRESS_.Text))
            {                
                EMAIL_ADRESS_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                EMAIL_ADRESS_.BorderBrush = Brushes.Blue;
            }  
            if(string.IsNullOrEmpty (HOME_ADRESS_.Text))
            {               
                HOME_ADRESS_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                HOME_ADRESS_.BorderBrush = Brushes.Blue;
            }  
            
            if (!Ok) MessageBox.Show("Verifier Les Informations !");

            return Ok; 
        }
        
    }
}
