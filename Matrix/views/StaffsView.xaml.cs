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
using Matrix.Model;

namespace Matrix.views
{
    
    public partial class StaffsView
    {

        public StaffsView ( )
        {
            InitializeComponent ();
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            UpdateDep ();
        }


        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate (new Uri ("/views/HomePage.xaml", UriKind.Relative));
        }

        private void AddButton_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new StaffINFO {Owner = Window.GetWindow(this), OpenOption = "Add"};
            wind.ShowDialog ();
            UpdateDep ();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(CurrentSelected == null)
            {
                MessageBox.Show ("Selectionner Un Staff A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetStaffFullName (CurrentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.Db.DeleteStaff (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
            UpdateDep ();
        }

        private string CurrentSelected;

        private void DepStaffList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var Staff = sender as ListBox;

            if(Staff == null) return;

            if(Staff.SelectedValue == null)
            {
                CurrentSelected = null;
                return;
            }
            CurrentSelected = Staff.SelectedValue.ToString ();
        }
       
        private void DepStaffList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {          
            var Staff = sender as ListBox;

            if(Staff == null) return;
            if(Staff.SelectedValue == null) return;
            var wind = new StaffINFO (Staff.SelectedValue.ToString ()) {
                Owner = Window.GetWindow(this),
                OpenOption = "Mod"
            };
            wind.ShowDialog ();
            UpdateDep ();            
        }

             

        #region Background Load


        private readonly BackgroundWorker Worker = new BackgroundWorker ();

        private readonly List<StaffViewModel> ListBuff = new List<StaffViewModel>();
        private void UpdateDep ( )
        {
            if(Worker.IsBusy) return;
            BusyIndicator.IsBusy = true;
            Worker.RunWorkerAsync ();
        }
        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {          
            var Deps = App.Db.GetDEPARTEMENTS();
            
            var N = new StaffViewModel {DEPARTEMENT_NAME = "", STAFFS_LIST = App.Db.GetDepStaffs()};
            ListBuff.Add (N);
         
            foreach (var D in Deps)
            {
                var M = new StaffViewModel ();

                if (D != null)
                {
                    M.DEPARTEMENT_NAME = D;
                    
                    M.STAFFS_LIST = App.Db.GetDepStaffs(M.DEPARTEMENT_NAME);
                }

                ListBuff.Add (M);
            }
           
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {          
            BusyIndicator.IsBusy = false;
            StaffList.ItemsSource = ListBuff;
            Worker.Dispose ();
        }

        #endregion

        

        

        

        



       
       



    }
}
