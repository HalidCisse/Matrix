using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Utils;
using Microsoft.Win32;

namespace Matrix.views.Staffs
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class StaffInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly bool _isAdd;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffToDisplayId"></param>
        public StaffInfo (string staffToDisplayId = null )
        {            
            InitializeComponent ();

            if (string.IsNullOrEmpty(staffToDisplayId)) { _isAdd = true; }

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(GetPatternData));
            }).ContinueWith(delegate
            {
                if (_isAdd) {
                    DisplayDefault();
                }
                else {
                    DisplayStaff(App.DataS.Hr.GetStaffById(staffToDisplayId));
                }                
            }).Start();
                        
        }

        private void GetPatternData()
        {           
            TITLE_.ItemsSource = App.DataS.DataEnums.GetTitles();

            Nationality.ItemsSource = App.DataS.DataEnums.GetNationalities();

            BirthPlace.ItemsSource = App.DataS.DataEnums.GetBIRTH_PLACE();

            Statut.ItemsSource = App.DataS.DataEnums.GetStaffStatuts();

            Position.ItemsSource = App.DataS.DataEnums.GetStaffPositions();

            Departement.ItemsSource = App.DataS.DataEnums.GetDepartements();

            Qualification.ItemsSource = App.DataS.DataEnums.GetStaffQualifications();
        }

        private void PhotoID_Click ( object sender, RoutedEventArgs e )
        {
            var openFileDialog = new OpenFileDialog ();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if(openFileDialog.ShowDialog () == true)
            {
                if(string.IsNullOrEmpty (openFileDialog.FileName)) return;

                PhotoIdentity.Source = new BitmapImage (new Uri (openFileDialog.FileName));
            }
        }

        private void DisplayDefault()
        {
            CoursExpander.Visibility = Visibility.Hidden;
            MatiereExpander.Visibility = Visibility.Hidden;
            InfoExpander.IsExpanded = true;

            StaffId.Text = GenNewStaffId ();
            TITLE_.SelectedIndex = 0;
            PhotoIdentity.Source = PhotoIdentity.Source = new BitmapImage (new Uri (Res.GetRes ("Portrait/defaultStaff.png")));
            
            Position.SelectedIndex = 0;
            Departement.SelectedIndex = 0;
            Qualification.SelectedIndex = 0;
            HiredDate.SelectedDate = DateTime.Today.Date;

            PhoneNumber.Text =  "+00";
            BirthDate.SelectedDate = DateTime.Today.AddDays (-7000);
            Nationality.SelectedIndex = 0;
            BirthPlace.SelectedIndex = 0;
            Statut.SelectedIndex = 0;
        }

        private void DisplayStaff ( Staff staffToDisplay )
        {
            if(staffToDisplay == null) return;


            StaffId.Text = staffToDisplay.StaffId;
            StaffId.IsEnabled = false;
            TITLE_.SelectedValue = staffToDisplay.Title;
            Firstname.Text = staffToDisplay.FirstName;
            Lastname.Text = staffToDisplay.LastName;
            PhotoIdentity.Source = ImageUtils.DecodePhoto (staffToDisplay.PhotoIdentity);

            Position.Text = staffToDisplay.Position;
            Departement.Text = staffToDisplay.Departement;
            Qualification.Text = staffToDisplay.Qualification;
            HiredDate.SelectedDate = staffToDisplay.HiredDate;

            IdentityNumber.Text = staffToDisplay.IdentityNumber;
            BirthDate.SelectedDate = staffToDisplay.BirthDate;
            Nationality.Text = staffToDisplay.Nationality;
            BirthPlace.Text = staffToDisplay.BirthPlace;
            PhoneNumber.Text = staffToDisplay.PhoneNumber;
            EmailAdress.Text = staffToDisplay.EmailAdress;
            HomeAdress.Text = staffToDisplay.HomeAdress;
            Statut.SelectedValue = staffToDisplay.Statut;
            Enregistrer.Content = "Modifier";
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myStaff= new Staff
            {
                StaffId = StaffId.Text.Trim (),
                Title = TITLE_.SelectedValue.ToString (),
                FirstName = Firstname.Text.Trim (),
                LastName = Lastname.Text.Trim (),
                PhotoIdentity = ImageUtils.GetPngFromImageControl (PhotoIdentity.Source as BitmapImage),

                Position = Position.Text,
                Departement = Departement.Text,
                Qualification = Qualification.Text,
                HiredDate = HiredDate.SelectedDate,

                IdentityNumber = IdentityNumber.Text,
                BirthDate = BirthDate.SelectedDate,
                Nationality = Nationality.Text,
                BirthPlace = BirthPlace.Text,
                PhoneNumber = PhoneNumber.Text,
                EmailAdress = EmailAdress.Text,
                HomeAdress = HomeAdress.Text,
                Statut = Statut.SelectedValue.ToString (),
            };

            if (_isAdd)
            {
                try
                {
                    App.DataS.Hr.AddStaff(myStaff);
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
                    App.DataS.Hr.UpdateStaff(myStaff);
                }
                catch (Exception ex)
                {

                    ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
                    return;
                }


                ModernDialog.ShowMessage("Modifer Avec Success", "Matrix", MessageBoxButton.OK);
                Close();
            }
        }

        private static string GenNewStaffId ( )
        {
            string idOut;

            do idOut = "ST" + DateTime.Today.Year + "-" + GenId.GetId (3) + "-" + GenId.GetId (4); while(App.DataS.Hr.StaffExist (idOut));

            return idOut;
        }

        private bool ChampsValidated ( )
        {          
            var ok = true;

            if(_isAdd)
            {
                if(string.IsNullOrEmpty (StaffId.Text))
                {
                    StaffId.BorderBrush = Brushes.Red;
                    ok = false;
                }
                else if(App.DataS.Students.StudentExist (StaffId.Text.Trim ()))
                {
                    MessageBox.Show ("Ce Numero de Matricule Est Deja Utiliser par " + App.DataS.Hr.GetStaffFullName (StaffId.Text.Trim ()));
                    return false;
                }
                else
                {
                    StaffId.BorderBrush = Brushes.Blue;
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

            if(string.IsNullOrEmpty (Nationality.Text))
            {
                Nationality.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                Nationality.BorderBrush = Brushes.Blue;
            }
            if(string.IsNullOrEmpty (BirthPlace.Text))
            {
                BirthPlace.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                BirthPlace.BorderBrush = Brushes.Blue;
            }
            if(string.IsNullOrEmpty (PhoneNumber.Text))
            {
                PhoneNumber.BorderBrush = Brushes.Red;
                //Ok = false;
            }
            else
            {
                PhoneNumber.BorderBrush = Brushes.Blue;
            }
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

            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
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


       

    }
}
