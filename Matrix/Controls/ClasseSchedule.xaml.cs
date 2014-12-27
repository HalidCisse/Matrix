using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.ViewModel;
using Matrix.views.Pedagogy;

namespace Matrix.Controls
{
    /// <summary>
    /// L'Emploi du Temps d'une Classe Avec Gestion Cours, Note, Absence
    /// </summary>
    public partial class ClasseSchedule
    {

        #region FIELDS

        /// <summary>
        /// Quand on click sur une un cours sender = CoursGuid
        /// </summary>
        public event EventHandler SelectedCoursChanged;

        /// <summary>
        /// Quand on choisie une nouvelle Date sender = selectedDate
        /// </summary>
        public event EventHandler SelectedDateChanged;

        private Guid _currentclassGuid;

        private DateTime _currentDate = DateTime.Today;

        private string _titleText = "Emploie Du Temps";
       
        #endregion

        /// <summary>
        /// L'Emploi du Temps d'une Classe Avec Gestion Cours, Note, Absence
        /// </summary>
        public ClasseSchedule()
        {
            InitializeComponent();

            DATE_PICKER.SelectedDate = _currentDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <param name="currentDate"></param>
        public void Refresh(Guid? classeGuid = null, DateTime? currentDate = null)
        {
            if (classeGuid != null) _currentclassGuid = (Guid) classeGuid;
            if (currentDate != null) _currentDate = (DateTime) currentDate;
            
            Dispatcher.BeginInvoke(new Action(() =>
            {
                SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_currentclassGuid, _currentDate);
                var classe = App.DataS.Pedagogy.Classes.GetClasseById(_currentclassGuid);
                _titleText = "Emploie Du Temps " + classe?.Name + " - " + App.DataS.Pedagogy.Filieres.GetFiliereName(classe?.FiliereGuid);
                TITLE_TEXT.Text = _titleText;
            }));
            
        }

        #region EVENT HANDLERS

        private void DayCoursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            SelectedCoursChanged?.Invoke(list.SelectedValue.ToString(), e);
        }

        private void DayCoursList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            var cour = ((CoursCard)((ListBox)sender).SelectedItem);
                        
            SCHEDULE_FRAME.NavigationService.Navigate(new SaisiePresence(cour.CoursGuid, cour.CoursDate));
            BACK_BUTTON.Visibility = Visibility.Visible;
            TITLE_TEXT.Text = "Presence au cours de " + cour.MatiereName + " avec " + cour.StaffFullName + " entre " + cour.StartTime.ToString("hh\\:mm") + " et " + cour.EndTime.ToString("hh\\:mm");
        } 

        private void DATE_PICKER_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentDate = DATE_PICKER.SelectedDate.GetValueOrDefault();
            SelectedDateChanged?.Invoke(_currentDate, e);
            
            Refresh();
        }

        private void BACK_BUTTON_OnClick(object sender, RoutedEventArgs e)
        {
            SCHEDULE_FRAME.NavigationService.GoBack();
            BACK_BUTTON.Visibility = Visibility.Collapsed;
            TITLE_TEXT.Text = _titleText;
        }



        #endregion


        #region CONTEXT MENUS

        private void CoursContextDel_Click(object sender, RoutedEventArgs e)
        {
            //var myCoursId = ((ListBox)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedValue;

            //if (myCoursId == null) return;
        }

        private void CoursContextMod_OnClick(object sender, RoutedEventArgs e)
        {
            var myCoursId = ((ListBox)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedValue;

            if (myCoursId == null) return;

            var courToOpen = App.DataS.Pedagogy.Cours.GetCoursById(new Guid(myCoursId.ToString()));

            var wind = new AddCours(_currentclassGuid, courToOpen) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            Refresh();            
        }
      
        private void CoursContextPresence_OnClick(object sender, RoutedEventArgs e)
        {
            var cour = ((CoursCard)((ListBox)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedItem);
          
            SCHEDULE_FRAME.NavigationService.Navigate(new SaisiePresence(cour.CoursGuid, cour.CoursDate));
            BACK_BUTTON.Visibility = Visibility.Visible;
            TITLE_TEXT.Text = "Presence au cours de " + cour.MatiereName + " avec " + cour.StaffFullName + " entre " + cour.StartTime.ToString("hh\\:mm") + " et " + cour.EndTime.ToString("hh\\:mm");
        }

        private void CoursContextCorrection_OnClick(object sender, RoutedEventArgs e)
        {
            //var myCoursId = ((ListBox)((ContextMenu)((MenuItem)sender).Parent).PlacementTarget).SelectedValue;

            //if (myCoursId == null) return;
        }

        private void CoursContext_OnOpened(object sender, RoutedEventArgs e)
        {
            var myCoursType = ((CoursCard)((ListBox)((ContextMenu)sender).PlacementTarget).SelectedItem).Type;

            if (myCoursType.Equals("CONTROL"))
            {
                ((MenuItem)((ContextMenu)sender).Items.GetItemAt(1)).Visibility = Visibility.Visible;
            }
            else
            {
                ((MenuItem)((ContextMenu)sender).Items.GetItemAt(1)).Visibility = Visibility.Collapsed;
            }

        }



        #endregion









    }
}
