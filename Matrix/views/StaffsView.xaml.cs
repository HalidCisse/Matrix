using System;
//using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using DataService;
using DataService.Entities;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for StaffsView.xaml
    /// </summary>
    public partial class StaffsView
    {

        public StaffsView ( )
        {
            InitializeComponent ();
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            UpdateStaff();
        }

        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate (new Uri ("/views/HomePage.xaml", UriKind.Relative));
        }

        private void AddButton_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new StaffINFO {Owner = Window.GetWindow(this), OpenOption = "Add"};
            wind.ShowDialog ();          
            UpdateStaff();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(StaffList.SelectedValue == null) {

                MessageBox.Show ("Selectionner Un Staff A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetStaffFullName (StaffList.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.Db.DeleteStaff (StaffList.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
            UpdateStaff ();
        }

        private void StaffList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            if (StaffList == null) return;
            if (StaffList.SelectedValue == null) return;
            var wind = new StaffINFO (StaffList.SelectedValue.ToString ())
            {
                Owner = Window.GetWindow(this),
                OpenOption = "Mod"
            };
            wind.ShowDialog ();
            UpdateStaff ();
        }




        #region Background Works

        private readonly BackgroundWorker worker = new BackgroundWorker ();

        private List<Staff> StaffBuff;
        private void UpdateStaff ( )
        {
            if(worker.IsBusy) return;
            BusyIndicator.IsBusy = true;
            worker.RunWorkerAsync ();
        }
        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            StaffBuff = App.Db.GetAllStaffs();         
        }
        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            StaffList.ItemsSource = StaffBuff;
            worker.Dispose ();
        }

        #endregion



    }
}
