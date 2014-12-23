using System;
using System.Threading.Tasks;
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
        private Guid _classId;


        #endregion

        /// <summary>
        /// UI Emploi du temps d'une classe en une semaine
        /// </summary>
        [Obsolete("ScheduleWeek is deprecated, please use ClassSchedule instead.")]     
        public ScheduleWeek()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Mettre a jour les information de l'emploi du temps
        /// </summary>
        /// <param name="classId">ID de la classe</param>
        public void Refresh(Guid classId)
        {
            _classId = classId;

            Dispatcher.BeginInvoke(new Action(() => { SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_classId, DateTime.Now); }));
        }

        private void DayCoursList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            var wind = new AddCours(_classId, App.DataS.Pedagogy.Cours.GetCoursById(new Guid(list.SelectedValue.ToString()))) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            Refresh(_classId);
        }

        private void DayCoursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var courId = ((ListBox) sender).SelectedValue?.ToString();            
            if (courId == null) return;

            SelectionChanged?.Invoke(courId, e);
        }

        //bug : Test Zone
        //todo : Restyle Schedule

    }
}














//new Task(() =>
//            {
//                SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_classId, DateTime.Now);
//            }).RunSynchronously();
//Dispatcher.BeginInvoke(new Action(() => { SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_classId, DateTime.Now); }));