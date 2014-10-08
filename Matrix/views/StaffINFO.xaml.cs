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
using DataService.Entities;
using Matrix.Utils;
using Microsoft.Win32;


namespace Matrix.views
{
    
    public partial class StaffINFO
    {
        public string OpenOption;

        public StaffINFO ( string StaffToDisplayID = null )
        {
            InitializeComponent ();

            #region Patterns Data

            TITLE_.ItemsSource = App.Db.GetTITLES ();

            NATIONALITY_.ItemsSource = App.Db.GetNATIONALITIES ();

            BIRTH_PLACE_.ItemsSource = App.Db.GetBIRTH_PLACE ();

            STATUT_.ItemsSource = App.Db.GetSTATUTS ();

            #endregion

            if(!string.IsNullOrEmpty (StaffToDisplayID))
                DisplayStaff (App.Db.GetStaffByID (StaffToDisplayID));
            else
                DisplayDefault ();
        }


        private void PhotoID_Click ( object sender, RoutedEventArgs e )
        {
            var openFileDialog = new OpenFileDialog ();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog () == true)
            {
                if(string.IsNullOrEmpty (openFileDialog.FileName)) return;

                PHOTO_IDENTITY_.Source = new BitmapImage (new Uri (openFileDialog.FileName));
            }
        }

        private void DisplayDefault()
        {
            ID_.Text = GenNewStaffID ();
            TITLE_.SelectedIndex = 0;
            PHOTO_IDENTITY_.Source = PHOTO_IDENTITY_.Source = new BitmapImage (new Uri (Res.GetRes ("Portrait/defaultStaff.png")));
            PHONE_NUMBER_.Text =  "+00";
            BIRTH_DATE_.SelectedDate = DateTime.Today.AddDays (-7000);
            NATIONALITY_.SelectedIndex = 0;
            BIRTH_PLACE_.SelectedIndex = 0;
            STATUT_.SelectedIndex = 0;
        }

        private void DisplayStaff ( Staff StaffToDisplay )
        {
            if(StaffToDisplay == null) return;

            ID_.Text = StaffToDisplay.STAFF_ID;
            ID_.IsEnabled = false;
            TITLE_.SelectedValue = StaffToDisplay.TITLE;
            FIRSTNAME_.Text = StaffToDisplay.FIRSTNAME;
            LASTNAME_.Text = StaffToDisplay.LASTNAME;
            PHOTO_IDENTITY_.Source = ImageUtils.DecodePhoto (StaffToDisplay.PHOTO_IDENTITY);
            IDENTITY_NUMBER_.Text = StaffToDisplay.IDENTITY_NUMBER;
            BIRTH_DATE_.SelectedDate = StaffToDisplay.BIRTH_DATE;
            NATIONALITY_.SelectedValue = StaffToDisplay.NATIONALITY;
            BIRTH_PLACE_.SelectedValue = StaffToDisplay.BIRTH_PLACE;
            PHONE_NUMBER_.Text = StaffToDisplay.PHONE_NUMBER;
            EMAIL_ADRESS_.Text = StaffToDisplay.EMAIL_ADRESS;
            HOME_ADRESS_.Text = StaffToDisplay.HOME_ADRESS;
            STATUT_.SelectedValue = StaffToDisplay.STATUT;
            Enregistrer.Content = "Modifier";
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var MyStaff= new Staff
            {
                STAFF_ID = ID_.Text.Trim (),
                TITLE = TITLE_.SelectedValue.ToString (),
                FIRSTNAME = FIRSTNAME_.Text.Trim (),
                LASTNAME = LASTNAME_.Text.Trim (),
                PHOTO_IDENTITY = ImageUtils.getPNGFromImageControl (PHOTO_IDENTITY_.Source as BitmapImage),
                IDENTITY_NUMBER = IDENTITY_NUMBER_.Text.Trim (),
                BIRTH_DATE = BIRTH_DATE_.SelectedDate.Value,
                NATIONALITY = NATIONALITY_.SelectedValue.ToString (),
                BIRTH_PLACE = BIRTH_PLACE_.SelectedValue.ToString (),
                PHONE_NUMBER = PHONE_NUMBER_.Text.Trim (),
                EMAIL_ADRESS = EMAIL_ADRESS_.Text.Trim (),
                HOME_ADRESS = HOME_ADRESS_.Text.Trim (),
                STATUT = STATUT_.SelectedValue.ToString (),
            };

            switch (OpenOption)
            {
                case "Add":
                    MyStaff.REGISTRATION_DATE = DateTime.Now.Date;
                    MessageBox.Show (App.Db.AddStaff (MyStaff) ? "Add Success" : "Add Failed");
                    Close ();
                    break;
                default:
                    MessageBox.Show (App.Db.UpdateStaff (MyStaff) ? "Update Success" : "Update Failed");
                    Close ();
                    break;
            }

        }


        private static string GenNewStaffID ( )
        {
            string idOut;

            do idOut = "S" + DateTime.Today.Year + GenID.GetID (4); while(App.Db.StudentExist (idOut));

            return idOut;
        }
        private bool ChampsValidated ( )
        {
          
            var Ok = true;


            if(OpenOption == "Add")
            {
                if(string.IsNullOrEmpty (ID_.Text))
                {
                    ID_.BorderBrush = Brushes.Red;
                    Ok = false;
                }
                else if(App.Db.StudentExist (ID_.Text.Trim ()))
                {
                    MessageBox.Show ("Ce Numero de Matricule Est Deja Utiliser par " + App.Db.GetStaffFullName (ID_.Text.Trim ()));
                    return false;
                }
                else
                {
                    ID_.BorderBrush = Brushes.Blue;
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

            if(NATIONALITY_.SelectedValue == null)
            {
                NATIONALITY_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                NATIONALITY_.BorderBrush = Brushes.Blue;
            }
            if(BIRTH_PLACE_.SelectedValue == null)
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

            if(!Ok) MessageBox.Show ("Verifier Les Informations !");

            return Ok;
        }



    }
}
