using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataService.Entities;
using Matrix.Model;

namespace Matrix.views
{
    
    public partial class MatieresView
    {

        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private readonly List<MatiereViewModel> ListBuff = new List<MatiereViewModel> ();
        private string CurrentSelected;
        private bool isFistTime = true;
        public string OpenedFiliere { get; set; }

        public MatieresView ( string OpenFiliere )
        {
            InitializeComponent ();

            OpenedFiliere = OpenFiliere;
        }
       
        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
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
            var wind = new AddMatiere (OpenedFiliere) { Owner = Window.GetWindow (this), OpenOption = "Add" };
            wind.ShowDialog ();
        }

        private void DeleteMatiereButton_Click ( object sender, RoutedEventArgs e )
        {
            
            if(CurrentSelected == null)
            {
                MessageBox.Show ("Selectionner Une Matiere A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetMatiereName (CurrentSelected);
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.Db.DeleteMatiere (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
            UpdateMatieres ();
        }

        private void MatiereList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var Matieres = sender as ListBox;

            if(Matieres == null) return;
            if(Matieres.SelectedValue == null) return;
            var wind = new AddMatiere (Matieres.SelectedValue.ToString ())
            {
                Owner = Window.GetWindow (this),
                OpenOption = "Mod"
            };
            wind.ShowDialog();
            UpdateMatieres ();           
        }

        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => E != null && Ep.Header.ToString () != E.Header.ToString ()))
            {
                Ep.IsExpanded = false;
            }
        }


        private void MatiereList_Loaded ( object sender, RoutedEventArgs e )
        {
            if(!isFistTime) return;
            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => Ep.Header.ToString () == "1 Annee"))
            {
                Ep.IsExpanded = true;
                isFistTime = false;
            }
        }


        private void MatiereList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var Matieres = sender as ListBox;

            if(Matieres == null) return;

            if(Matieres.SelectedValue == null)
            {
                CurrentSelected = null;
                return;
            }
            CurrentSelected = Matieres.SelectedValue.ToString ();
        }

        //_______________________________________________________________________________________//

        private void UpdateMatieres ( )
        {
            if(Worker.IsBusy) return;
            //BusyIndicator.IsBusy = true;
            Worker.RunWorkerAsync ();
        }
        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {            
            var Ans = App.Db.GetFILIERE_NIVEAUX (OpenedFiliere);
           
            foreach(int A in Ans)
            {
                var M = new MatiereViewModel
                {
                    ANNEE_NAME = A + " Annee",
                    MATIERES_LIST = App.Db.GetMatieresOfFiliereYear(OpenedFiliere, A)
                };

                ListBuff.Add (M);
            }
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            AnneeList.ItemsSource = ListBuff;
            Worker.Dispose ();
        }

        //_______________________________________________________________________________________//





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
