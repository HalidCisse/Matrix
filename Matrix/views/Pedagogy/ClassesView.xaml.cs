using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataService.ViewModel;

namespace Matrix.views.Pedagogy
{

    /// <summary>
    /// 
    /// </summary>
    public partial class ClassesView 
    {

        private readonly BackgroundWorker _worker = new BackgroundWorker ();
        private List<ClassCard> _listBuff = new List<ClassCard> ();
        private Guid _openedFiliereId;
        //private string CurrentSelected;
        private bool _isFirstTime = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openFiliere"></param>
        public ClassesView (Guid openFiliere)
        {
            InitializeComponent ();
            _openedFiliereId = openFiliere;
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateClass (); 
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = NavigationService;
            if (navigationService != null)
                navigationService.Navigate(new FilieresView());
        }

        private void ClassAddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddClass() { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateClass ();
        }

        private void ClassDeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if (ClassList == null) return;
            if (ClassList.SelectedValue == null)
            {
                MessageBox.Show ("Selectionner Une Classe A Supprimer !");
                return;
            }

            var currentSelected = new Guid (ClassList.SelectedValue.ToString ());
           
            var theGaName = App.DataS.Pedagogy.Classes.GetClasseName (currentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.DataS.Pedagogy.Classes.DeleteClasse (currentSelected) ? "Supprimer Avec Succes" : "Echec");
            UpdateClass ();
        }
       
        //private void Expander_Expanded ( object sender, RoutedEventArgs e )
        //{
        //    var E = sender as Expander;

        //    foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => E != null && Ep.Header.ToString () != E.Header.ToString ()))
        //    {
        //        Ep.IsExpanded = false;
        //    }
        //}

        private void ClassList_Loaded ( object sender, RoutedEventArgs e )
        {
            if(!_isFirstTime) return;
            try
            {
                var E = FindVisualChildren<Expander> (this).First ();
                E.IsExpanded = true;
                _isFirstTime = false; 
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ClassList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var list = sender as ListBox;
            if(list?.SelectedValue == null) return;

            var navigationService = NavigationService;
            navigationService?.Navigate (new ClassDetails (new Guid (list.SelectedValue.ToString())));
        }

        private void ClassList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            //var Classes = sender as ListBox;

            //if(Classes == null) return;
            //if(Classes.SelectedValue == null) return;

            //CurrentSelected = Classes.SelectedValue.ToString ();
        }

        private void UpdateClass ( )
        {
            if(_worker.IsBusy) return;
            _worker.RunWorkerAsync ();
        }

        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            _listBuff = App.ModelS.GetFiliereClassCards (_openedFiliereId);
        }

        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            ClassList.ItemsSource = _listBuff;
            _isFirstTime = true;
            _worker.Dispose ();
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
