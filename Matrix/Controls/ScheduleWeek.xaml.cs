using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Matrix.views.Pedagogy;

namespace Matrix.Controls
{
    /// <summary>
    /// Interaction logic for ScheduleWeek.xaml
    /// </summary>
    public partial class ScheduleWeek
    {

        #region FIELDS
        
        /// <summary>
        /// Quand on click sur une un cours e = ID
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// ID de la classe
        /// </summary>
        private Guid ClassID;
        
        #endregion


        /// <summary>
        /// UI Emploi du temps d'une classe en une semaine
        /// </summary>
        public ScheduleWeek()
        {                    
            InitializeComponent();
            BWorker.DoWork += BWorkerDoWork;
        }
        

        #region EVENTS HANDLER
              
        private void DayCoursList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            var wind = new AddCours(ClassID, App.DataS.GetCoursByID(new Guid(list.SelectedValue.ToString()))) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData(ClassID);
        }
       
        private void DayCoursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            SelectionChanged?.Invoke(list.SelectedValue.ToString(), e);
        }

        #endregion


        #region BACKGROUND WORKER

        private readonly BackgroundWorker BWorker = new BackgroundWorker();

        /// <summary>
        /// Mettre a jour les information de l'emploi du temps
        /// </summary>
        /// <param name="Class_ID">ID de la classe</param>
        public void UpdateData(Guid Class_ID)
        {
            ClassID = Class_ID;
            if (BWorker.IsBusy) { BWorker.CancelAsync(); }            
            BWorker.RunWorkerAsync();
        }
        private void BWorkerDoWork(object sender, DoWorkEventArgs e)
        {           
            Dispatcher.BeginInvoke(new Action(() => { ScheduleUI.ItemsSource = App.ModelS.GetClassWeekAgendaData(ClassID, DateTime.Now); }));
            BWorker.Dispose();
        }
       
        #endregion


    }
}
