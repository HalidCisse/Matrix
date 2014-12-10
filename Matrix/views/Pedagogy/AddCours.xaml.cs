using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    
    /// <summary>
    /// Form pour ajouter un cours
    /// </summary>
    public partial class AddCours
    {
        private readonly bool _isAdd;
        private readonly Cours _currentCours = new Cours();

        /// <summary>
        /// Form pour ajouter un cours
        /// </summary>
        /// <param name="currentClassId"></param>
        /// <param name="coursToOpen"></param>
        public AddCours (Guid currentClassId, Cours coursToOpen = null )
        {
            InitializeComponent ();

            #region Patterns Data

                MatiereId.ItemsSource = App.DataS.Pedagogy.Classes.GetClassMatieres (currentClassId);

                StaffId.ItemsSource = App.DataS.Hr.GetAllStaffs ();

                SalleName.ItemsSource = App.DataS.DataEnums.GetAllSalles ();

                Type.ItemsSource = App.DataS.DataEnums.GetAllCoursTypes ();

                StartDate.SelectedDate = DateTime.Today;

                EndDate.SelectedDate = DateTime.Today;

            #endregion

            _currentCours.ClasseId = currentClassId;
            _isAdd = true; 

            if(coursToOpen != null)
            {
                _isAdd = false;
                _currentCours = coursToOpen;                
            }
                            
            Display ();
        }

        private void Display()
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
            Mar.IsChecked = _currentCours.RecurrenceDays.Contains ("2");
            Mer.IsChecked = _currentCours.RecurrenceDays.Contains ("3");
            Jeu.IsChecked = _currentCours.RecurrenceDays.Contains ("4");
            Vend.IsChecked = _currentCours.RecurrenceDays.Contains ("5");
            Sam.IsChecked = _currentCours.RecurrenceDays.Contains ("6");
            Dim.IsChecked = _currentCours.RecurrenceDays.Contains ("0");

            Description.Text = _currentCours.Description;
        }
       
        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;
           
            _currentCours.MatiereId = new Guid(MatiereId.SelectedValue.ToString()) ;
            _currentCours.StaffId = StaffId.SelectedValue.ToString();
            _currentCours.Salle = SalleName.Text;
            _currentCours.Type = Type.SelectedValue.ToString();
            _currentCours.StartTime = DateTime.Parse(StartTime.Value.ToString());     
            _currentCours.EndTime =DateTime.Parse (EndTime.Value.ToString ());       
            _currentCours.StartDate = StartDate.SelectedDate.GetValueOrDefault().Date;
            _currentCours.EndDate = EndDate.SelectedDate.GetValueOrDefault().Date;

            _currentCours.RecurrenceDays = "";
            if(Lun.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 1 ", _currentCours.RecurrenceDays); }
            if(Mar.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 2 ", _currentCours.RecurrenceDays); }
            if(Mer.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 3 ", _currentCours.RecurrenceDays); }
            if(Jeu.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 4 ", _currentCours.RecurrenceDays); }
            if(Vend.IsChecked == true){ _currentCours.RecurrenceDays = string.Format ("{0} 5 ", _currentCours.RecurrenceDays); }
            if(Sam.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 6 ", _currentCours.RecurrenceDays); }
            if(Dim.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 0 ", _currentCours.RecurrenceDays); }
            
            _currentCours.Description = Description.Text;

            if(_isAdd)
            {
                try
                {
                    App.DataS.Pedagogy.Cours.AddCours (_currentCours);
                    ModernDialog.ShowMessage ("Success", "Matrix", MessageBoxButton.OK);
                }
                catch(Exception ex)
                {
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                }
                Close ();
            }
            else
            {
                try
                {
                    App.DataS.Pedagogy.Cours.UpdateCours (_currentCours);
                    ModernDialog.ShowMessage ("Success", "Matrix", MessageBoxButton.OK);
                }
                catch(Exception ex)
                {
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                }
                Close ();
            } 
        }

        private bool ChampsValidated()
        {
            var ok = true;

            if(MatiereId.SelectedValue == null)
            {
                MatiereId.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                MatiereId.BorderBrush = Brushes.Blue;
            }

            if(StartDate.SelectedDate.GetValueOrDefault () >  EndDate.SelectedDate.GetValueOrDefault ())
            {
                StartDate.BorderBrush = Brushes.Red;
                EndDate.BorderBrush = Brushes.Red;
                ok = false;
                ModernDialog.ShowMessage ("Date de Debut doit etre inferieur a date de Fin !!", "Matrix", MessageBoxButton.OK);
            }
            else
            {
                StartDate.BorderBrush = Brushes.Blue;
                EndDate.BorderBrush = Brushes.Blue;
            }

            if(Lun.IsChecked == false && Mar.IsChecked == false && Mer.IsChecked == false && Jeu.IsChecked == false && Vend.IsChecked == false && Sam.IsChecked == false && Dim.IsChecked == false )
            {
                Lun.BorderBrush = Brushes.Red;
                Mar.BorderBrush = Brushes.Red;
                Mer.BorderBrush = Brushes.Red;
                Jeu.BorderBrush = Brushes.Red;
                Vend.BorderBrush = Brushes.Red;
                Sam.BorderBrush = Brushes.Red;
                Dim.BorderBrush = Brushes.Red; 
               
                ok = false;
                ModernDialog.ShowMessage ("Choisir Au Moins Un Jour !!", "Matrix", MessageBoxButton.OK);
            }

            if(!ok) ModernDialog.ShowMessage ("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void TYPE__OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TitleText.Text == "MODIFICATION") return;

            if(Type.SelectedValue.ToString () == "Revision")
              { TitleText.Text = "AJOUTER UNE " + Type.SelectedValue.ToString().ToUpper(); }
            else
            { TitleText.Text = "AJOUTER UN " + Type.SelectedValue.ToString ().ToUpper (); }

            if(Type.SelectedValue.ToString () == "Control" || Type.SelectedValue.ToString () == "Examen" || Type.SelectedValue.ToString () == "Test")
              { Instructeur.Text = "SUPERVISEUR"; }
            else
              { Instructeur.Text = "INSTRUCTEUR"; }
            
        }


    }
}
