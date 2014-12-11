using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataService.ViewModel;

namespace Matrix.views.Staffs
{
    
    public partial class StaffsView
    {
        private readonly BackgroundWorker _worker = new BackgroundWorker ();
        private List<DepStaffCard> _listBuff = new List<DepStaffCard> ();
        private string _currentSelected;        
        private bool _isFirstTime = true;

        public StaffsView ( ) {

            InitializeComponent ();
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateDep ();            
        }

        
        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            if (NavigationService != null)
                NavigationService.Navigate (new HomePage(), UriKind.Relative);
        }

        private void AddButton_Click ( object sender, RoutedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            var wind = new StaffInfo { Owner = Window.GetWindow (this), OpenOption = "Add" };
            wind.ShowDialog ();
            UpdateDep ();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(_currentSelected == null)
            {
                MessageBox.Show ("Selectionner Un Staff A Supprimer !");
                return;
            }

            var theGaName = App.DataS.Hr.GetStaffFullName (_currentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.DataS.Hr.DeleteStaff (_currentSelected) ? "Supprimer Avec Succes" : "Echec");
            UpdateDep ();
        }       

        private void DepStaffList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var staff = sender as ListBox;

            if(staff == null) return;

            if(staff.SelectedValue == null)
            {
                _currentSelected = null;
                return;
            }
            _currentSelected = staff.SelectedValue.ToString ();
        }
       
        private void DepStaffList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {          
            var staff = sender as ListBox;
            if(staff == null) return;
            if(staff.SelectedValue == null) return;

            var wind = new StaffInfo (staff.SelectedValue.ToString ()) {
                Owner = Window.GetWindow(this),
                OpenOption = "Mod"
            };
            wind.ShowDialog ();
            UpdateDep ();  
        }

        
        private void UpdateDep ( )
        {
            if(_worker.IsBusy) return;           
            _worker.RunWorkerAsync ();
        }
        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            _listBuff = App.ModelS.GetDepStaffsCard ();            
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {           
            BusyIndicator.IsBusy = false;
            StaffList.ItemsSource = _listBuff;
            _isFirstTime = true;                      
            _worker.Dispose ();
        }

        
        private void DepStaffList_Loaded ( object sender, RoutedEventArgs e )
        {
            if (!_isFirstTime) return;

            var E = FindVisualChildren<Expander> (this).FirstOrDefault (ep => ep.Header.ToString () == "");

            if (E != null) E.IsExpanded = true;
            _isFirstTime = false;                     
        }
      
        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;
                     
            foreach(var ep in FindVisualChildren<Expander>(this).Where(ep => E != null && ep.Header.ToString() != E.Header.ToString()))
            {
                ep.IsExpanded = false;                
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
