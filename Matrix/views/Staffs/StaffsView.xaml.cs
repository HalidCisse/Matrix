using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataService.ViewModel;
using Matrix.Model;

namespace Matrix.views
{
    
    public partial class StaffsView
    {
        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private List<DepStaffCard> ListBuff = new List<DepStaffCard> ();
        private string CurrentSelected;        
        private bool isFirstTime = true;

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

        
        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            if (NavigationService != null)
                NavigationService.Navigate (new HomePage(), UriKind.Relative);
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

            var theGaName = App.DataS.GetStaffFullName (CurrentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.DataS.DeleteStaff (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
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

        
        private void UpdateDep ( )
        {
            if(Worker.IsBusy) return;           
            Worker.RunWorkerAsync ();
        }
        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            ListBuff = App.ModelS.GetDepStaffsCard ();            
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {           
            BusyIndicator.IsBusy = false;
            StaffList.ItemsSource = ListBuff;
            isFirstTime = true;                      
            Worker.Dispose ();
        }

        
        private void DepStaffList_Loaded ( object sender, RoutedEventArgs e )
        {
            if (!isFirstTime) return;

            var E = FindVisualChildren<Expander> (this).FirstOrDefault (Ep => Ep.Header.ToString () == "");

            if (E != null) E.IsExpanded = true;
            isFirstTime = false;                     
        }
      
        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;
                     
            foreach(var Ep in FindVisualChildren<Expander>(this).Where(Ep => E != null && Ep.Header.ToString() != E.Header.ToString()))
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
