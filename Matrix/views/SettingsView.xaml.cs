using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView
    {
        private bool _isFirstTime;

        /// <summary>
        /// 
        /// </summary>
        public SettingsView()
        {
            InitializeComponent();
        }

        private void AnneeScolaireBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void AnneeScolaireBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isFirstTime) { _isFirstTime = false; return; }
          
            App.DataS.Pedagogy.CurrentAnneeScolaireGuid = new Guid(AnneeScolaireBox.SelectedValue.ToString());
        }

        private void RefreshData()
        {
            _isFirstTime = true;

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    AnneeScolaireBox.ItemsSource = App.DataS.Pedagogy.GetAllAnneeScolaires();
                    AnneeScolaireBox.SelectedValue = App.DataS.Pedagogy.CurrentAnneeScolaireGuid;

                }));

            }).Start();

        }
        
       
    }
}
