using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataService.ViewModel;

namespace Matrix.views
{
   
    public partial class ClassesView 
    {

        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private List<FiliereClassCard> ListBuff = new List<FiliereClassCard> ();
        private string CurrentSelected;
        private bool isFirstTime = true;

        public ClassesView ( )
        {
            InitializeComponent ();
                        
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateClass (); 
        }
      
        private void ClassAddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddClass { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateClass ();
        }

        private void ClassDeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(CurrentSelected == null)
            {
                MessageBox.Show ("Selectionner Une Classe A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetClasseName (CurrentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.Db.DeleteClasse (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
            UpdateClass ();
        }
       
        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => E != null && Ep.Header.ToString () != E.Header.ToString ()))
            {
                Ep.IsExpanded = false;
            }
        }

        private void ClassList_Loaded ( object sender, RoutedEventArgs e )
        {
            if(!isFirstTime) return;
            try
            {
                var E = FindVisualChildren<Expander> (this).First ();
                E.IsExpanded = true;
                isFirstTime = false; 
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ClassList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var list = sender as ListBox;
            if(list == null) return;
            if(list.SelectedValue == null) return;

            var navigationService = NavigationService;
            if(navigationService != null)
                navigationService.Navigate (new ClassDetails (new Guid (list.SelectedValue.ToString())));             
        }

        private void ClassList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var Classes = sender as ListBox;

            if(Classes == null) return;
            if(Classes.SelectedValue == null) return;

            CurrentSelected = Classes.SelectedValue.ToString ();
        }

        private void UpdateClass ( )
        {
            if(Worker.IsBusy) return;
            Worker.RunWorkerAsync ();
        }

        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            ListBuff = App.Db.GetFiliereClassCards ();
        }

        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            ClassFiliereList.ItemsSource = ListBuff;
            isFirstTime = true;
            Worker.Dispose ();
        }


        public static IEnumerable<T> FindVisualChildren<T> ( DependencyObject depObj ) where T : DependencyObject
        {
            if(depObj == null) yield break;
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
