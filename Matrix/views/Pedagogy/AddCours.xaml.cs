using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    
    public partial class AddCours
    {
        private readonly bool IsAdd;
        private Cours CurrentCours = new Cours();
        //private Guid CurrentClassID;

        public AddCours (Guid CurrentClassID, Cours CoursToOpen = null )
        {
            InitializeComponent ();

            #region Patterns Data

                MATIERE_ID_.ItemsSource = App.DataS.GetClassMatieres (CurrentClassID);

                STAFF_ID_.ItemsSource = App.DataS.GetAllStaffs ();

                SALLE_NAME_.ItemsSource = App.DataS.GetAllSalles ();

                TYPE_.ItemsSource = App.DataS.GetAllCoursTypes ();

                START_DATE_.SelectedDate = DateTime.Today;

                END_DATE_.SelectedDate = DateTime.Today;

            #endregion

            CurrentCours.CLASSE_ID = CurrentClassID;
            IsAdd = true; 

            if(CoursToOpen != null)
            {
                IsAdd = false;
                CurrentCours = CoursToOpen;                
            }
                            
            Display ();
        }

        private void Display()
        {
            if (IsAdd) return;

            TitleText.Text = "MODIFICATION";

            MATIERE_ID_.SelectedValue = CurrentCours.MATIERE_ID;
            STAFF_ID_.SelectedValue = CurrentCours.STAFF_ID;
            SALLE_NAME_.Text = CurrentCours.SALLE;
            TYPE_.SelectedValue = CurrentCours.TYPE;
            START_TIME_.Value = CurrentCours.START_TIME;
            END_TIME_.Value = CurrentCours.END_TIME;
            START_DATE_.SelectedDate = CurrentCours.START_DATE;
            END_DATE_.SelectedDate = CurrentCours.END_DATE;

            LUN_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains("LUN");
            MAR_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains ("MAR");
            MER_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains ("MER");
            JEU_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains ("JEU");
            VEND_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains ("VEND");
            SAM_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains ("SAM");
            DIM_.IsChecked = CurrentCours.RECURRENCE_DAYS.Contains ("DIM");

            DESCRIPTION_.Text = CurrentCours.DESCRIPTION;
        }

       

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;
           
            CurrentCours.MATIERE_ID = new Guid(MATIERE_ID_.SelectedValue.ToString()) ;
            CurrentCours.STAFF_ID = STAFF_ID_.SelectedValue.ToString();
            CurrentCours.SALLE = SALLE_NAME_.Text;
            CurrentCours.TYPE = TYPE_.SelectedValue.ToString();
            CurrentCours.START_TIME = DateTime.Parse(START_TIME_.Value.ToString());     
            CurrentCours.END_TIME =DateTime.Parse (END_TIME_.Value.ToString ());       
            CurrentCours.START_DATE = START_DATE_.SelectedDate;
            CurrentCours.END_DATE = END_DATE_.SelectedDate;

            CurrentCours.RECURRENCE_DAYS = "";
            if(LUN_.IsChecked == true) { CurrentCours.RECURRENCE_DAYS = string.Format ("{0} LUN ", CurrentCours.RECURRENCE_DAYS); }
            if(MAR_.IsChecked == true) { CurrentCours.RECURRENCE_DAYS = string.Format ("{0} MAR ", CurrentCours.RECURRENCE_DAYS); }
            if(MER_.IsChecked == true) { CurrentCours.RECURRENCE_DAYS = string.Format ("{0} MER ", CurrentCours.RECURRENCE_DAYS); }
            if(JEU_.IsChecked == true) { CurrentCours.RECURRENCE_DAYS = string.Format ("{0} JEU ", CurrentCours.RECURRENCE_DAYS); }
            if(VEND_.IsChecked == true){ CurrentCours.RECURRENCE_DAYS = string.Format ("{0} VEND ",CurrentCours.RECURRENCE_DAYS); }
            if(SAM_.IsChecked == true) { CurrentCours.RECURRENCE_DAYS = string.Format ("{0} SAM ", CurrentCours.RECURRENCE_DAYS); }
            if(DIM_.IsChecked == true) { CurrentCours.RECURRENCE_DAYS = string.Format ("{0} DIM ", CurrentCours.RECURRENCE_DAYS); }
            
            CurrentCours.DESCRIPTION = DESCRIPTION_.Text;

            if(IsAdd)
            {
                try
                {
                    App.DataS.AddCours (CurrentCours);
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
                    App.DataS.UpdateCours (CurrentCours);
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
            var Ok = true;

            if(MATIERE_ID_.SelectedValue == null)
            {
                MATIERE_ID_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                MATIERE_ID_.BorderBrush = Brushes.Blue;
            }

            if(START_DATE_.SelectedDate.GetValueOrDefault () >  END_DATE_.SelectedDate.GetValueOrDefault ())
            {
                START_DATE_.BorderBrush = Brushes.Red;
                END_DATE_.BorderBrush = Brushes.Red;
                Ok = false;
                ModernDialog.ShowMessage ("Date de Debut doit etre inferieur a date de Fin !!", "Matrix", MessageBoxButton.OK);
            }
            else
            {
                START_DATE_.BorderBrush = Brushes.Blue;
                END_DATE_.BorderBrush = Brushes.Blue;
            }

            if(LUN_.IsChecked == false && MAR_.IsChecked == false && MER_.IsChecked == false && JEU_.IsChecked == false && VEND_.IsChecked == false && SAM_.IsChecked == false && DIM_.IsChecked == false )
            {
                LUN_.BorderBrush = Brushes.Red;
                MAR_.BorderBrush = Brushes.Red;
                MER_.BorderBrush = Brushes.Red;
                JEU_.BorderBrush = Brushes.Red;
                VEND_.BorderBrush = Brushes.Red;
                SAM_.BorderBrush = Brushes.Red;
                DIM_.BorderBrush = Brushes.Red; 
               
                Ok = false;
                ModernDialog.ShowMessage ("Choisir Au Moins Un Jour !!", "Matrix", MessageBoxButton.OK);
            }

            if(!Ok) ModernDialog.ShowMessage ("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return Ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }


        private void TYPE__OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TitleText.Text != "AJOUTER UN COURS") return;

            if(TYPE_.SelectedValue.ToString () == "Revision")
              { TitleText.Text = "AJOUTER UNE " + TYPE_.SelectedValue.ToString().ToUpper(); }
            else
            { TitleText.Text = "AJOUTER UN " + TYPE_.SelectedValue.ToString ().ToUpper (); }

            if(TYPE_.SelectedValue.ToString () == "Control" || TYPE_.SelectedValue.ToString () == "Examen" || TYPE_.SelectedValue.ToString () == "Test")
              { INSTRUCTEUR_.Text = "SUPERVISEUR"; }
            else
              { INSTRUCTEUR_.Text = "INSTRUCTEUR"; }
            
        }


    }
}
