using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CLib;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;

namespace Matrix.views.Staffs
{

    //Todo: Emploi du Temps en faveur de Matiere/Cours
    /// <summary>
    /// 
    /// </summary>
    public partial class StaffInfo
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly bool _isAdd;

        private Guid _staffDisplayedGuid;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffToDisplayGuid"></param>
        public StaffInfo(string staffToDisplayGuid = null)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(staffToDisplayGuid)) { _isAdd = true; }

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetPatternData();

                    if (_isAdd) DisplayDefault();
                    else DisplayStaff(App.DataS.Hr.GetStaffByGuid(new Guid(staffToDisplayGuid)));

                }));
            }).Start();

        }

        private void GetPatternData()
        {
            TITLE_.ItemsSource = App.DataS.DataEnums.GetTitles();

            NATIONALITY.ItemsSource = App.DataS.DataEnums.GetNationalities();

            BIRTH_PLACE.ItemsSource = App.DataS.DataEnums.GetBIRTH_PLACE();

            STATUT.ItemsSource = App.DataS.DataEnums.GetStaffStatuts();

            POSITION.ItemsSource = App.DataS.DataEnums.GetStaffPositions();

            DEPARTEMENT.ItemsSource = App.DataS.DataEnums.GetDepartements();

            QUALIFICATION.ItemsSource = App.DataS.DataEnums.GetStaffQualifications();
        }

        private void PhotoID_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                if (string.IsNullOrEmpty(openFileDialog.FileName)) return;

                PHOTO_IDENTITY.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void DisplayDefault()
        {
            COURS_EXPANDER.Visibility = Visibility.Hidden;
            MATIERE_EXPANDER.Visibility = Visibility.Hidden;
            INFO_EXPANDER.IsExpanded = true;

            STAFF_ID.Text = GenNewStaffId();
            TITLE_.SelectedIndex = 0;
            PHOTO_IDENTITY.Source = PHOTO_IDENTITY.Source = new BitmapImage(new Uri(Res.GetRes("Portrait/defaultStaff.png")));

            POSITION.SelectedIndex = 0;
            DEPARTEMENT.SelectedIndex = 0;
            QUALIFICATION.SelectedIndex = 0;
            HIRED_DATE.SelectedDate = DateTime.Today.Date;

            PHONE_NUMBER.Text = "+00";
            BIRTH_DATE.SelectedDate = DateTime.Today.AddDays(-7000);
            NATIONALITY.SelectedIndex = 0;
            BIRTH_PLACE.SelectedIndex = 0;
            STATUT.SelectedIndex = 0;
        }

        private void DisplayStaff(Staff staffToDisplay)
        {
            if (staffToDisplay == null) return;

            _staffDisplayedGuid = staffToDisplay.StaffGuid;
            STAFF_ID.Text = staffToDisplay.StaffId;
            STAFF_ID.IsEnabled = false;
            TITLE_.SelectedValue = staffToDisplay.Title;
            FIRSTNAME.Text = staffToDisplay.FirstName;
            LASTNAME.Text = staffToDisplay.LastName;
            PHOTO_IDENTITY.Source = ImageUtils.DecodePhoto(staffToDisplay.PhotoIdentity);

            POSITION.Text = staffToDisplay.Position;
            DEPARTEMENT.Text = staffToDisplay.Departement;
            QUALIFICATION.Text = staffToDisplay.Qualification;
            HIRED_DATE.SelectedDate = staffToDisplay.HiredDate;

            IDENTITY_NUMBER.Text = staffToDisplay.IdentityNumber;
            BIRTH_DATE.SelectedDate = staffToDisplay.BirthDate;
            NATIONALITY.Text = staffToDisplay.Nationality;
            BIRTH_PLACE.Text = staffToDisplay.BirthPlace;
            PHONE_NUMBER.Text = staffToDisplay.PhoneNumber;
            EMAIL_ADRESS.Text = staffToDisplay.EmailAdress;
            HOME_ADRESS.Text = staffToDisplay.HomeAdress;
            STATUT.SelectedValue = staffToDisplay.Statut;
            ENREGISTRER.Content = "Modifier";
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (ChampsValidated() != true) return;

            var myStaff = new Staff
            {
                StaffId = STAFF_ID.Text.Trim(),
                Title = TITLE_.SelectedValue.ToString(),
                FirstName = FIRSTNAME.Text.Trim(),
                LastName = LASTNAME.Text.Trim(),
                PhotoIdentity = ImageUtils.GetPngFromImageControl(PHOTO_IDENTITY.Source as BitmapImage),

                Position = POSITION.Text,
                Departement = DEPARTEMENT.Text,
                Qualification = QUALIFICATION.Text,
                HiredDate = HIRED_DATE.SelectedDate,

                IdentityNumber = IDENTITY_NUMBER.Text,
                BirthDate = BIRTH_DATE.SelectedDate,
                Nationality = NATIONALITY.Text,
                BirthPlace = BIRTH_PLACE.Text,
                PhoneNumber = PHONE_NUMBER.Text,
                EmailAdress = EMAIL_ADRESS.Text,
                HomeAdress = HOME_ADRESS.Text,
                Statut = STATUT.SelectedValue.ToString(),
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
                    myStaff.StaffGuid = _staffDisplayedGuid;
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

        private static string GenNewStaffId()
        {
            string idOut;

            do idOut = "ST" + DateTime.Today.Year + "-" + GenId.GetId(3) + "-" + GenId.GetId(4); while (App.DataS.Hr.StaffIdExist(idOut));

            return idOut;
        }

        private bool ChampsValidated()
        {
            var ok = true;

            if (_isAdd)
            {
                if (string.IsNullOrEmpty(STAFF_ID.Text))
                {
                    STAFF_ID.BorderBrush = Brushes.Red;
                    ok = false;
                }
                else if (App.DataS.Students.StudentExist(STAFF_ID.Text.Trim()))
                {
                    MessageBox.Show("Ce Numero de Matricule Est Deja Utiliser par " + App.DataS.Hr.GetStaffFullName(STAFF_ID.Text.Trim()));
                    return false;
                }
                else
                {
                    STAFF_ID.BorderBrush = Brushes.Blue;
                }
            }

            if (string.IsNullOrEmpty(FIRSTNAME.Text))
            {
                FIRSTNAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                FIRSTNAME.BorderBrush = Brushes.Blue;
            }

            if (string.IsNullOrEmpty(LASTNAME.Text))
            {
                LASTNAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                LASTNAME.BorderBrush = Brushes.Blue;
            }

            if (string.IsNullOrEmpty(IDENTITY_NUMBER.Text))
            {
                IDENTITY_NUMBER.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                IDENTITY_NUMBER.BorderBrush = Brushes.Blue;
            }

            if (string.IsNullOrEmpty(NATIONALITY.Text))
            {
                NATIONALITY.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NATIONALITY.BorderBrush = Brushes.Blue;
            }
            if (string.IsNullOrEmpty(BIRTH_PLACE.Text))
            {
                BIRTH_PLACE.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                BIRTH_PLACE.BorderBrush = Brushes.Blue;
            }
            if (string.IsNullOrEmpty(PHONE_NUMBER.Text))
            {
                PHONE_NUMBER.BorderBrush = Brushes.Red;
                //Ok = false;
            }
            else
            {
                PHONE_NUMBER.BorderBrush = Brushes.Blue;
            }
            if (string.IsNullOrEmpty(EMAIL_ADRESS.Text))
            {
                EMAIL_ADRESS.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                EMAIL_ADRESS.BorderBrush = Brushes.Blue;
            }
            if (string.IsNullOrEmpty(HOME_ADRESS.Text))
            {
                HOME_ADRESS.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                HOME_ADRESS.BorderBrush = Brushes.Blue;
            }

            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
        }



        #region EXPANDERS

        private void CoursExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (INFO_EXPANDER != null && (ROLE_EXPANDER != null && (MATIERE_EXPANDER != null && (!MATIERE_EXPANDER.IsExpanded && !ROLE_EXPANDER.IsExpanded && !INFO_EXPANDER.IsExpanded))))
            {
                MATIERE_EXPANDER.IsExpanded = true;
            }
        }

        private void CoursExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (MATIERE_EXPANDER != null) MATIERE_EXPANDER.IsExpanded = false;
            if (ROLE_EXPANDER != null) ROLE_EXPANDER.IsExpanded = false;
            if (INFO_EXPANDER != null) INFO_EXPANDER.IsExpanded = false;
        }

        private void MatiereExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (COURS_EXPANDER != null && (INFO_EXPANDER != null && (ROLE_EXPANDER != null && (!ROLE_EXPANDER.IsExpanded && !INFO_EXPANDER.IsExpanded && !COURS_EXPANDER.IsExpanded))))
            {
                ROLE_EXPANDER.IsExpanded = true;
            }
        }

        private void MatiereExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (ROLE_EXPANDER != null) ROLE_EXPANDER.IsExpanded = false;
            if (INFO_EXPANDER != null) INFO_EXPANDER.IsExpanded = false;
            if (COURS_EXPANDER != null) COURS_EXPANDER.IsExpanded = false;
        }

        private void RoleExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (COURS_EXPANDER != null && (INFO_EXPANDER != null && (MATIERE_EXPANDER != null && (!MATIERE_EXPANDER.IsExpanded && !INFO_EXPANDER.IsExpanded && !COURS_EXPANDER.IsExpanded))))
            {
                INFO_EXPANDER.IsExpanded = true;
            }
        }

        private void RoleExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (MATIERE_EXPANDER != null) MATIERE_EXPANDER.IsExpanded = false;
            if (INFO_EXPANDER != null) INFO_EXPANDER.IsExpanded = false;
            if (COURS_EXPANDER != null) COURS_EXPANDER.IsExpanded = false;
        }

        private void InfoExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            if (COURS_EXPANDER != null && (ROLE_EXPANDER != null && (MATIERE_EXPANDER != null && (!MATIERE_EXPANDER.IsExpanded && !ROLE_EXPANDER.IsExpanded && !COURS_EXPANDER.IsExpanded))))
            {
                COURS_EXPANDER.IsExpanded = true;
            }
        }

        private void InfoExpander_Expanded(object sender, RoutedEventArgs e)
        {
            if (MATIERE_EXPANDER != null) MATIERE_EXPANDER.IsExpanded = false;
            if (ROLE_EXPANDER != null) ROLE_EXPANDER.IsExpanded = false;
            if (COURS_EXPANDER != null) COURS_EXPANDER.IsExpanded = false;
        }

        #endregion




    }
}
