using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using DataService.Entities.Pedagogy;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    /// <summary>
    /// Interaction logic for AddInscription.xaml
    /// </summary>
    public partial class AddInscription
    {
        private readonly bool _isAdd;
        private readonly Inscription _currentInscription = new Inscription();

        /// <summary>
        /// Form Pour Ajouter Inscrire Un Etudiant
        /// </summary>
        /// <param name="inscriptionToOpen"></param>
        public AddInscription(string inscriptionToOpen)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(inscriptionToOpen)) { _isAdd = true; }

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
                    Display(App.DataS.Pedagogy.Inscriptions.GetInscriptionById(new Guid(inscriptionToOpen)));
                }
            }).Start();
           
        }

        private void Display(Inscription getInscriptionById)
        {
            throw new NotImplementedException();
        }

        private void GetPatternData()
        {
            //todo: Add List Of Setting in App

            StudentId.ItemsSource = App.DataS.Pedagogy.Inscriptions.GetStudentsNotIns();

            StaffId.ItemsSource = App.DataS.Hr.GetAllStaffs();

            SalleName.ItemsSource = App.DataS.DataEnums.GetAllSalles();

            Type.ItemsSource = App.DataS.DataEnums.GetAllCoursTypes();

            StartDate.SelectedDate = DateTime.Today;

            EndDate.SelectedDate = DateTime.Today;
        }

        private void DisplayDefault()
        {
            if (_isAdd) return;

            TitleText.Text = "MODIFICATION";

            MatiereId.SelectedValue = _currentCours.MatiereId;
            StaffId.SelectedValue = _currentCours.StaffId;
            SalleName.Text = _currentCours.Salle;
            Type.SelectedValue = _currentCours.Type;
            StartTime.Value = _currentCours.StartTime;
            EndTime.Value = _currentCours.EndTime;
            StartDate.SelectedDate = _currentCours.StartDate;
            EndDate.SelectedDate = _currentCours.EndDate;

            Lun.IsChecked = _currentCours.RecurrenceDays.Contains("1");
            Mar.IsChecked = _currentCours.RecurrenceDays.Contains("2");
            Mer.IsChecked = _currentCours.RecurrenceDays.Contains("3");
            Jeu.IsChecked = _currentCours.RecurrenceDays.Contains("4");
            Vend.IsChecked = _currentCours.RecurrenceDays.Contains("5");
            Sam.IsChecked = _currentCours.RecurrenceDays.Contains("6");
            Dim.IsChecked = _currentCours.RecurrenceDays.Contains("0");

            Description.Text = _currentCours.Description;
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (ChampsValidated() != true) return;

            _currentCours.MatiereId = new Guid(MatiereId.SelectedValue.ToString());
            _currentCours.StaffId = StaffId.SelectedValue.ToString();
            _currentCours.Salle = SalleName.Text;
            _currentCours.Type = Type.SelectedValue.ToString();
            _currentCours.StartTime = DateTime.Parse(StartTime.Value.ToString());
            _currentCours.EndTime = DateTime.Parse(EndTime.Value.ToString());
            _currentCours.StartDate = StartDate.SelectedDate.GetValueOrDefault().Date;
            _currentCours.EndDate = EndDate.SelectedDate.GetValueOrDefault().Date;

            

            if (_isAdd)
            {
                try
                {
                    App.DataS.Pedagogy.Cours.AddCours(_currentCours);
                    ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);
                }
                Close();
            }
            else
            {
                try
                {
                    App.DataS.Pedagogy.Cours.UpdateCours(_currentCours);
                    ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);
                }
                Close();
            }

        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool ChampsValidated()
        {
            var ok = true;

            if (MatiereId.SelectedValue == null)
            {
                MatiereId.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                MatiereId.BorderBrush = Brushes.Blue;
            }

            if (StartDate.SelectedDate.GetValueOrDefault() > EndDate.SelectedDate.GetValueOrDefault())
            {
                StartDate.BorderBrush = Brushes.Red;
                EndDate.BorderBrush = Brushes.Red;
                ok = false;
                ModernDialog.ShowMessage("Date de Debut doit etre inferieur a date de Fin !!", "Matrix", MessageBoxButton.OK);
            }
            else
            {
                StartDate.BorderBrush = Brushes.Blue;
                EndDate.BorderBrush = Brushes.Blue;
            }

            if (Lun.IsChecked == false && Mar.IsChecked == false && Mer.IsChecked == false && Jeu.IsChecked == false && Vend.IsChecked == false && Sam.IsChecked == false && Dim.IsChecked == false)
            {
                Lun.BorderBrush = Brushes.Red;
                Mar.BorderBrush = Brushes.Red;
                Mer.BorderBrush = Brushes.Red;
                Jeu.BorderBrush = Brushes.Red;
                Vend.BorderBrush = Brushes.Red;
                Sam.BorderBrush = Brushes.Red;
                Dim.BorderBrush = Brushes.Red;

                ok = false;
                ModernDialog.ShowMessage("Choisir Au Moins Un Jour !!", "Matrix", MessageBoxButton.OK);
            }

            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
        }
    }
}
