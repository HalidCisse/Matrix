using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DataService.ViewModel;
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
        /// Les donnees de l'Agenda
        /// </summary>
        private List<DayCoursCards> AgendaData{get; set; } = new List<DayCoursCards>();

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// ID de la classe
        /// </summary>
        public Guid ClassID;
        
        #endregion


        /// <summary>
        /// UI Emploi du temps d'une classe en une semaine
        /// </summary>
        public ScheduleWeek()
        {                    
            InitializeComponent();
                        
        }
        

        #region EVENTS HANDLER
       
        private void ScheduleWeek_OnLoaded(object sender, RoutedEventArgs e)
        {
            BWorker.DoWork += BWorkerDoWork;
            //BWorker.RunWorkerCompleted += BWorkerRunBWorkerCompleted;                       
        }

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
            SelectionChanged?.Invoke(this, e);
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
            AgendaData = App.ModelS.GetClassWeekAgendaData(ClassID, DateTime.Now);
            Dispatcher.BeginInvoke(new Action(() => { ScheduleUI.ItemsSource = AgendaData; }));
            BWorker.Dispose();
        }
       

        #endregion


    }
}
