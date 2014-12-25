using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.ViewModel;

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

        private Guid _currentclassId;

        private DateTime _currentDate = DateTime.Today;

       
        #endregion

        /// <summary>
        /// L'Emploi du Temps d'une Classe Avec Gestion Cours, Note, Absence
        /// </summary>
        public ClasseSchedule()
        {
            InitializeComponent();

            DATE_PICKER.SelectedDate = DateTime.Today;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classeGuid"></param>
        /// <param name="currentDate"></param>
        public void Refresh(Guid? classeGuid = null, DateTime? currentDate = null)
        {
            if (classeGuid != null) _currentclassId = (Guid) classeGuid;
            if (currentDate != null) _currentDate = (DateTime) currentDate;
            
            Dispatcher.BeginInvoke(new Action(() => { SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_currentclassId, _currentDate); }));
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
        }

        private void DATE_PICKER_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentDate = DATE_PICKER.SelectedDate.GetValueOrDefault();
            SelectedDateChanged?.Invoke(_currentDate, e);
            
            Refresh();
        }




        #endregion

        private void BACK_BUTTON_OnClick(object sender, RoutedEventArgs e)
        {
            SCHEDULE_FRAME.NavigationService.GoBack();
            BACK_BUTTON.Visibility = Visibility.Collapsed;
        }
    }
}
