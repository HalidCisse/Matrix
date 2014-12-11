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
        public ScheduleWeek()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Mettre a jour les information de l'emploi du temps
        /// </summary>
        /// <param name="classId">ID de la classe</param>
        public void UpdateData(Guid classId)
        {
            _classId = classId;

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() => { ScheduleUi.ItemsSource = App.ModelS.GetClassWeekAgendaData(_classId, DateTime.Now); }));
            }).Start();

        }

        private void DayCoursList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            var wind = new AddCours(_classId, App.DataS.Pedagogy.Cours.GetCoursById(new Guid(list.SelectedValue.ToString()))) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData(_classId);
        }

        private void DayCoursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            SelectionChanged?.Invoke(list.SelectedValue.ToString(), e);
        }


        //todo : Restyle Schedule

    }
}
