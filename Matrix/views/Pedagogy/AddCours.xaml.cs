using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities.Pedagogy;
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
        /// <param name="currentClassGuid"></param>
        /// <param name="coursToOpen"></param>
        public AddCours (Guid currentClassGuid, Cours coursToOpen = null )
        {
            InitializeComponent ();

            _currentCours.ClasseGuid = currentClassGuid;

            if (coursToOpen == null) _isAdd = true;           
            else _currentCours = coursToOpen;
           
            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetPatternData();
                    
                    Display();
                }));
            }).Start();

            

           

            //_currentCours.ClasseId = currentClassId;
            //_isAdd = true; 

            //if(coursToOpen != null)
            //{
            //    _isAdd = false;
            //    _currentCours = coursToOpen;                
            //}
                            
            //Display ();
        }

        private void GetPatternData()
        {
            MATIERE_ID.ItemsSource = App.DataS.Pedagogy.Classes.GetClassMatieres(_currentCours.ClasseGuid);

            STAFF_ID.ItemsSource = App.DataS.Hr.GetAllStaffs();

            SALLE_NAME.ItemsSource = App.DataS.DataEnums.GetAllSalles();

            TYPE.ItemsSource = App.DataS.DataEnums.GetAllCoursTypes();

            START_DATE.SelectedDate = DateTime.Today;

            END_DATE.SelectedDate = DateTime.Today;
        }

        private void Display()
        {
            if (_isAdd) return;

            TITLE_TEXT.Text = "MODIFICATION";

            MATIERE_ID.SelectedValue = _currentCours.MatiereGuid;
            STAFF_ID.SelectedValue = _currentCours.StaffGuid;
            SALLE_NAME.Text = _currentCours.Salle;
            TYPE.SelectedValue = _currentCours.Type;
            START_TIME.Value = new DateTime(_currentCours.StartTime.Ticks); 
            END_TIME.Value = new DateTime(_currentCours.EndTime.Ticks); 
            START_DATE.SelectedDate = _currentCours.StartDate;
            END_DATE.SelectedDate = _currentCours.EndDate;

            LUN.IsChecked = _currentCours.RecurrenceDays.Contains("1");
            MAR.IsChecked = _currentCours.RecurrenceDays.Contains ("2");
            MER.IsChecked = _currentCours.RecurrenceDays.Contains ("3");
            JEU.IsChecked = _currentCours.RecurrenceDays.Contains ("4");
            VEND.IsChecked = _currentCours.RecurrenceDays.Contains ("5");
            SAM.IsChecked = _currentCours.RecurrenceDays.Contains ("6");
            DIM.IsChecked = _currentCours.RecurrenceDays.Contains ("0");

            DESCRIPTION.Text = _currentCours.Description;
        }
       
        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;
           
            _currentCours.MatiereGuid = new Guid(MATIERE_ID.SelectedValue.ToString()) ;
            _currentCours.StaffGuid = new Guid(STAFF_ID.SelectedValue.ToString());
            _currentCours.Salle = SALLE_NAME.Text;
            _currentCours.Type = TYPE.SelectedValue.ToString();
            _currentCours.StartTime = DateTime.Parse(START_TIME.Value.ToString()).TimeOfDay;     
            _currentCours.EndTime =DateTime.Parse (END_TIME.Value.ToString ()).TimeOfDay;       
            _currentCours.StartDate = START_DATE.SelectedDate.GetValueOrDefault().Date;
            _currentCours.EndDate = END_DATE.SelectedDate.GetValueOrDefault().Date;

            _currentCours.RecurrenceDays = "";
            if(LUN.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 1 ", _currentCours.RecurrenceDays); }
            if(MAR.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 2 ", _currentCours.RecurrenceDays); }
            if(MER.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 3 ", _currentCours.RecurrenceDays); }
            if(JEU.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 4 ", _currentCours.RecurrenceDays); }
            if(VEND.IsChecked == true){ _currentCours.RecurrenceDays = string.Format ("{0} 5 ", _currentCours.RecurrenceDays); }
            if(SAM.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 6 ", _currentCours.RecurrenceDays); }
            if(DIM.IsChecked == true) { _currentCours.RecurrenceDays = string.Format ("{0} 0 ", _currentCours.RecurrenceDays); }
            
            _currentCours.Description = DESCRIPTION.Text;

            if(_isAdd)
            {
                try
                {
                    App.DataS.Pedagogy.Cours.AddCours (_currentCours);                    
                }
                catch(Exception ex)
                {
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                    Close();
                }
                ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
                Close ();
            }
            else
            {
                try
                {
                    App.DataS.Pedagogy.Cours.UpdateCours (_currentCours);                  
                }
                catch(Exception ex)
                {
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                    Close();
                }
                ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
                Close ();
            } 
        }

        private bool ChampsValidated()
        {
            var ok = true;

            if(MATIERE_ID.SelectedValue == null)
            {
                MATIERE_ID.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                MATIERE_ID.BorderBrush = Brushes.Blue;
            }

            if(START_DATE.SelectedDate.GetValueOrDefault () >  END_DATE.SelectedDate.GetValueOrDefault ())
            {
                START_DATE.BorderBrush = Brushes.Red;
                END_DATE.BorderBrush = Brushes.Red;
                ok = false;
                ModernDialog.ShowMessage ("Date de Debut doit etre inferieur a date de Fin !!", "Matrix", MessageBoxButton.OK);
            }
            else
            {
                START_DATE.BorderBrush = Brushes.Blue;
                END_DATE.BorderBrush = Brushes.Blue;
            }

            if(LUN.IsChecked == false && MAR.IsChecked == false && MER.IsChecked == false && JEU.IsChecked == false && VEND.IsChecked == false && SAM.IsChecked == false && DIM.IsChecked == false )
            {
                LUN.BorderBrush = Brushes.Red;
                MAR.BorderBrush = Brushes.Red;
                MER.BorderBrush = Brushes.Red;
                JEU.BorderBrush = Brushes.Red;
                VEND.BorderBrush = Brushes.Red;
                SAM.BorderBrush = Brushes.Red;
                DIM.BorderBrush = Brushes.Red; 
               
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
            if(TITLE_TEXT.Text == "MODIFICATION") return;

            if(TYPE.SelectedValue.ToString () == "Revision")
              { TITLE_TEXT.Text = "AJOUTER UNE " + TYPE.SelectedValue.ToString().ToUpper(); }
            else
            { TITLE_TEXT.Text = "AJOUTER UN " + TYPE.SelectedValue.ToString ().ToUpper (); }

            if(TYPE.SelectedValue.ToString () == "Control" || TYPE.SelectedValue.ToString () == "Examen" || TYPE.SelectedValue.ToString () == "Test")
              { INSTRUCTEUR.Text = "SUPERVISEUR"; }
            else
              { INSTRUCTEUR.Text = "INSTRUCTEUR"; }
            
        }


    }
}
