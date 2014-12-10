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
    
    public partial class MatieresView
    {

        private readonly BackgroundWorker _worker = new BackgroundWorker ();
        private List<FiliereLevelCard> _listBuff = new List<FiliereLevelCard> ();        
        private Guid _currentSelected;
        private bool _isFirstTime = true;
        public Guid OpenedFiliere { get; set; }
        
        public MatieresView ( Guid openFiliere )
        {
            InitializeComponent ();

            OpenedFiliere = openFiliere;
            MatiereHeader.Text = App.DataS.Pedagogy.Filieres.GetFiliereById(openFiliere).Name.ToUpper();
        }
       
        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateMatieres (); 
        }     

        private void BackButton_Click ( object sender, RoutedEventArgs e )
        {
            var navigationService = NavigationService;
            if(navigationService != null)
                navigationService.Navigate (new FilieresView ());
        }

        private void AddMatiereButton_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddMatiere (OpenedFiliere) { Owner = Window.GetWindow (this)};
            wind.ShowDialog ();
            UpdateMatieres ();
        }

        private void DeleteMatiereButton_Click ( object sender, RoutedEventArgs e )
        {

            if(App.DataS.Pedagogy.Matieres.GetMatiereById (_currentSelected) == null)
            {
                MessageBox.Show ("Selectionner Une Matiere A Supprimer !");
                return;
            }

            var theGaName = App.DataS.Pedagogy.Matieres.GetMatiereName (_currentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.DataS.Pedagogy.Matieres.DeleteMatiere (_currentSelected) ? "Supprimer Avec Succes" : "Echec");
            UpdateMatieres ();
        }

        private void MatiereList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var matieres = sender as ListBox;
            if(matieres == null) return;
            if(matieres.SelectedValue == null) return;
            var matiereToDisplay = App.DataS.Pedagogy.Matieres.GetMatiereById(new Guid(matieres.SelectedValue.ToString()));

            var wind = new AddMatiere (OpenedFiliere, matiereToDisplay) { Owner = Window.GetWindow (this) };
            wind.ShowDialog();
            UpdateMatieres ();           
        }

        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var ep in FindVisualChildren<Expander> (this).Where (ep => E != null && ep.Header.ToString () != E.Header.ToString ()))
            {
                ep.IsExpanded = false;
            }
        }

        private void MatiereList_Loaded ( object sender, RoutedEventArgs e )
        {
            if(!_isFirstTime) return;

            var E = FindVisualChildren<Expander>(this).First(ep => ep.Header.ToString() == "1 ere Annee");
            E.IsExpanded = true;
            _isFirstTime = false;            
        }

        private void MatiereList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var matieres = sender as ListBox;

            if(matieres == null) return;

            if (matieres.SelectedValue == null) return;

            _currentSelected = new Guid (matieres.SelectedValue.ToString ()) ;
        }

       

        private void UpdateMatieres ( )
        {
            if(_worker.IsBusy) return;            
            _worker.RunWorkerAsync ();
        }
        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            _listBuff.Clear();
            //ListBuff = App.ModelS.GetFiliereMatieresCards (OpenedFiliere);                        
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;           
            AnneeList.ItemsSource = _listBuff;
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
