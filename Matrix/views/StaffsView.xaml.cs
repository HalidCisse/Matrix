using System;
//using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using DataService;
using DataService.Entities;
using Matrix.Model;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

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
            BusyIndicator.IsBusy = false;
            var wind = new StaffINFO { Owner = Window.GetWindow (this), OpenOption = "Add" };
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

            //N.STAFF_COUNT = N.STAFFS_LIST.Count;

            ListBuff.Add (N);
         
            foreach (var D in Deps)
            {
                var M = new StaffViewModel ();

                if (D != null)
                {
                    M.DEPARTEMENT_NAME = D.ToUpper();

                    M.STAFFS_LIST = App.Db.GetDepStaffs(D);

                    //M.STAFF_COUNT = M.STAFFS_LIST.Count;
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

        private void DepStaffList_Loaded ( object sender, RoutedEventArgs e )
        {
            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => string.IsNullOrEmpty (Ep.Header.ToString ())))
            {
                Ep.IsExpanded = true;
            }
        }

        public static IEnumerable<T> FindVisualChildren<T> ( DependencyObject depObj ) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for(var i = 0; i < VisualTreeHelper.GetChildrenCount (depObj); i++)
            {
                var child = VisualTreeHelper.GetChild (depObj, i);
                if(child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach(var childOfChild in FindVisualChildren<T> (child))
                {
                    yield return childOfChild;
                }
            }
        }

           
    }
}
