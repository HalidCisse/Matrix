using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public List<DayCoursCards> AgendaData
        {
            get;
            set;
        } = new List<DayCoursCards>();
        private readonly BackgroundWorker Worker = new BackgroundWorker();
        /// <summary>
        /// ID de la classe
        /// </summary>
        private Guid ClassID;
        
        #endregion


        /// <summary>
        /// Affiche l'emploi du temps d'une classe pour une semaine
        /// </summary>
        public ScheduleWeek()
        {                    
            InitializeComponent();

            DataContext = AgendaData;
        }

        

        #region EVENTS HANDLER


        private void AgendaUI_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AgendaUI.Items.Clear();
        }

        private void ScheduleWeek_OnLoaded(object sender, RoutedEventArgs e)
        {
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //AgendaUI.Items.Clear();
            UpdateData(ClassID);
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

        }


        #endregion


        #region BACKGROUND WORKER

        /// <summary>
        /// Mettre a jour les information de l'emploi du temps
        /// </summary>
        /// <param name="Class_ID">ID de la classe</param>
        public void UpdateData(Guid Class_ID)
        {
            ClassID = Class_ID;
            if (Worker.IsBusy) return;
            Worker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            AgendaData = App.ModelS.GetClassWeekAgendaData(ClassID, DateTime.Now);            
            Worker.Dispose();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {           
            AgendaUI.ItemsSource = AgendaData;          
            Worker.Dispose();
        }

        #endregion

        
    }
}
