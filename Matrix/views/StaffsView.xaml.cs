using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Matrix.Model;
using MessageBox = System.Windows.MessageBox;

namespace Matrix.views
{
    
    public partial class StaffsView
    {
        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private readonly List<StaffViewModel> ListBuff = new List<StaffViewModel> ();
        private string CurrentSelected;
        private bool isFistTime = true;

        public StaffsView ( ) {

            InitializeComponent ();
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateDep ();            
        }

        //_______________________________________________________________________________________//

        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            if (NavigationService != null)
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

        //_______________________________________________________________________________________//
             
        private void UpdateDep ( )
        {
            if(Worker.IsBusy) return;
            //BusyIndicator.IsBusy = true;
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

        //_______________________________________________________________________________________//
        
        private void DepStaffList_Loaded ( object sender, RoutedEventArgs e )
        {
            if (!isFistTime) return;
            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => string.IsNullOrEmpty (Ep.Header.ToString ())))
            {
                Ep.IsExpanded = true;
                isFistTime = false;
            }
        }

        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => E != null && Ep.Header.ToString () != E.Header.ToString()))
            {
                Ep.IsExpanded = false;                
            }
        }

        public static IEnumerable<T> FindVisualChildren<T> ( DependencyObject depObj ) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for(var i = 0; i < VisualTreeHelper.GetChildrenCount (depObj); i++)
            {
                var child = VisualTreeHelper.GetChild (depObj, i);
                if(child is T)
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
