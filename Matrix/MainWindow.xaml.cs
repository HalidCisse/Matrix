using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Matrix
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// 
        /// </summary>
        public MainWindow ( ) {
            InitializeComponent ();            
        }
       
        private void SeetingButton_OnClick(object sender, RoutedEventArgs e)
        {
            MyFlyout.IsOpen = true;


            //var wind = new SettingsView() { Owner = Window.GetWindow(this) };
            //wind.Show();
        }

        private void AnneeScolaireBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void AnneeScolaireBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }
       
        private void MyTabControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            MyFlyout.IsOpenChanged += MyFlyout_IsOpenChanged;
        }

        private void MyFlyout_IsOpenChanged(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    //AnneeScolaireBox.ItemsSource = App.DataS.Pedagogy.GetAllAnneeScolaires();                   
                    //AnneeScolaireBox.SelectedValue = App.DataS.MatrixSettings.CurrentAnneeScolaireGuid();

                    //var x = App.CurrentUser.UserProfile
                }));
                //Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(_openedClass.ClasseId); }));
                //Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(_openedClass.ClasseId); }));
                //Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.Pedagogy.Classes.GetClassStudents(_openedClass.ClasseId); }));

            }).Start();

        }



    }     
}
