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
//using Xceed.Wpf.Toolkit;

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

            STATUT_.ItemsSource = App.Db.GetStaffSTATUTS ();

            POSITION_.ItemsSource = App.Db.GetStaffPOSITIONS ();

            DEPARTEMENT_.ItemsSource = App.Db.GetDEPARTEMENTS ();

            QUALIFICATION_.ItemsSource = App.Db.GetStaffQUALIFICATIONS ();

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
            CoursExpander.Visibility = Visibility.Hidden;
            MatiereExpander.Visibility = Visibility.Hidden;
            InfoExpander.IsExpanded = true;

            STAFF_ID_.Text = GenNewStaffID ();
            TITLE_.SelectedIndex = 0;
            PHOTO_IDENTITY_.Source = PHOTO_IDENTITY_.Source = new BitmapImage (new Uri (Res.GetRes ("Portrait/defaultStaff.png")));
            
            POSITION_.SelectedIndex = 0;
            DEPARTEMENT_.SelectedIndex = 0;
            QUALIFICATION_.SelectedIndex = 0;
            HIRED_DATE_.SelectedDate = DateTime.Today.Date;

            PHONE_NUMBER_.Text =  "+00";
            BIRTH_DATE_.SelectedDate = DateTime.Today.AddDays (-7000);
            NATIONALITY_.SelectedIndex = 0;
            BIRTH_PLACE_.SelectedIndex = 0;
            STATUT_.SelectedIndex = 0;
        }

        private void DisplayStaff ( Staff StaffToDisplay )
        {
            if(StaffToDisplay == null) return;


            STAFF_ID_.Text = StaffToDisplay.STAFF_ID;
            STAFF_ID_.IsEnabled = false;
            TITLE_.SelectedValue = StaffToDisplay.TITLE;
            FIRSTNAME_.Text = StaffToDisplay.FIRSTNAME;
            LASTNAME_.Text = StaffToDisplay.LASTNAME;
            PHOTO_IDENTITY_.Source = ImageUtils.DecodePhoto (StaffToDisplay.PHOTO_IDENTITY);

            POSITION_.Text = StaffToDisplay.POSITION;
            DEPARTEMENT_.Text = StaffToDisplay.DEPARTEMENT;
            QUALIFICATION_.Text = StaffToDisplay.QUALIFICATION;
            HIRED_DATE_.SelectedDate = StaffToDisplay.HIRED_DATE;

            IDENTITY_NUMBER_.Text = StaffToDisplay.IDENTITY_NUMBER;
            BIRTH_DATE_.SelectedDate = StaffToDisplay.BIRTH_DATE;
            NATIONALITY_.Text = StaffToDisplay.NATIONALITY;
            BIRTH_PLACE_.Text = StaffToDisplay.BIRTH_PLACE;
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
                STAFF_ID = STAFF_ID_.Text.Trim (),
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

            if(POSITION_.SelectedValue != null) MyStaff.POSITION = POSITION_.SelectedValue.ToString ();
            if(DEPARTEMENT_.SelectedValue != null) MyStaff.DEPARTEMENT = DEPARTEMENT_.SelectedValue.ToString ();
            if(QUALIFICATION_.SelectedValue != null) MyStaff.QUALIFICATION = QUALIFICATION_.SelectedValue.ToString ();
            if(HIRED_DATE_.SelectedDate != null) MyStaff.HIRED_DATE = HIRED_DATE_.SelectedDate.Value;

            if (OpenOption == "Add")
            {
                MyStaff.REGISTRATION_DATE = DateTime.Now.Date;
                MessageBox.Show(App.Db.AddStaff(MyStaff) ? "Add Success" : "Add Failed");
                Close();
            }
            else
            {
                MessageBox.Show(App.Db.UpdateStaff(MyStaff) ? "Update Success" : "Update Failed");
                Close();
            }
        }

        private static string GenNewStaffID ( )
        {
            string idOut;

            do idOut = "S" + DateTime.Today.Year + GenID.GetID (4); while(App.Db.StaffExist (idOut));

            return idOut;
        }

        private bool ChampsValidated ( )
        {          
            var Ok = true;

            if(OpenOption == "Add")
            {
                if(string.IsNullOrEmpty (STAFF_ID_.Text))
                {
                    STAFF_ID_.BorderBrush = Brushes.Red;
                    Ok = false;
                }
                else if(App.Db.StudentExist (STAFF_ID_.Text.Trim ()))
                {
                    MessageBox.Show ("Ce Numero de Matricule Est Deja Utiliser par " + App.Db.GetStaffFullName (STAFF_ID_.Text.Trim ()));
                    return false;
                }
                else
                {
                    STAFF_ID_.BorderBrush = Brushes.Blue;
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



        #region EXPANDERS

        private void CoursExpander_Collapsed ( object sender, RoutedEventArgs e )
        {
            if(InfoExpander != null && (RoleExpander != null && (MatiereExpander != null && (!MatiereExpander.IsExpanded && !RoleExpander.IsExpanded && !InfoExpander.IsExpanded))))
            {
                MatiereExpander.IsExpanded = true;
            }
        }

        private void CoursExpander_Expanded ( object sender, RoutedEventArgs e )
        {
            if(MatiereExpander != null) MatiereExpander.IsExpanded = false;
            if(RoleExpander != null) RoleExpander.IsExpanded = false;
            if(InfoExpander != null) InfoExpander.IsExpanded = false;
        }

        private void MatiereExpander_Collapsed ( object sender, RoutedEventArgs e )
        {
            if(CoursExpander != null && (InfoExpander != null && (RoleExpander != null && (!RoleExpander.IsExpanded && !InfoExpander.IsExpanded && !CoursExpander.IsExpanded))))
            {
                RoleExpander.IsExpanded = true;
            }
        }

        private void MatiereExpander_Expanded ( object sender, RoutedEventArgs e )
        {
            if (RoleExpander != null) RoleExpander.IsExpanded = false;
            if (InfoExpander != null) InfoExpander.IsExpanded = false;
            if(CoursExpander != null) CoursExpander.IsExpanded = false;
        }

        private void RoleExpander_Collapsed ( object sender, RoutedEventArgs e )
        {
            if(CoursExpander != null && (InfoExpander != null && (MatiereExpander != null && (!MatiereExpander.IsExpanded && !InfoExpander.IsExpanded && !CoursExpander.IsExpanded))))
            {
                InfoExpander.IsExpanded = true;
            }
        }

        private void RoleExpander_Expanded ( object sender, RoutedEventArgs e )
        {
            if (MatiereExpander != null) MatiereExpander.IsExpanded = false;
            if (InfoExpander != null) InfoExpander.IsExpanded = false;
            if(CoursExpander != null) CoursExpander.IsExpanded = false;
        }

        private void InfoExpander_Collapsed ( object sender, RoutedEventArgs e )
        {
            if(CoursExpander != null && (RoleExpander != null && (MatiereExpander != null && (!MatiereExpander.IsExpanded && !RoleExpander.IsExpanded && !CoursExpander.IsExpanded))))
            {
                CoursExpander.IsExpanded = true;
            }
        }

        private void InfoExpander_Expanded ( object sender, RoutedEventArgs e )
        {
            if (MatiereExpander != null) MatiereExpander.IsExpanded = false;
            if (RoleExpander != null) RoleExpander.IsExpanded = false;
            if(CoursExpander != null) CoursExpander.IsExpanded = false;
        }

        
        #endregion


        #region INTER-ACTION

        
        #endregion

       

    }
}
