using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Matrix.Model;

namespace Matrix.views
{
    
    public partial class MatieresView
    {

        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private readonly List<MatiereViewModel> ListBuff = new List<MatiereViewModel>();        
        private string CurrentSelected;
        private bool isFistTime = true;
        public string OpenedFiliere { get; set; }
        
        public MatieresView ( string OpenFiliere )
        {
            InitializeComponent ();

            OpenedFiliere = OpenFiliere;
            MatiereHeader.Text = App.Db.GetFiliereByID(OpenFiliere).NAME.ToUpper();
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
            var wind = new AddMatiere (OpenedFiliere) { Owner = Window.GetWindow (this)};
            wind.ShowDialog ();
            UpdateMatieres ();
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
            var MatiereToDisplay = App.Db.GetMatiereByID(Matieres.SelectedValue.ToString());

            var wind = new AddMatiere (OpenedFiliere, MatiereToDisplay) { Owner = Window.GetWindow (this) };
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

            CurrentSelected = Matieres.SelectedValue != null ? Matieres.SelectedValue.ToString() : null;
        }

        //_______________________________________________________________________________________//

        private void UpdateMatieres ( )
        {
            if(Worker.IsBusy) return;            
            Worker.RunWorkerAsync ();
        }
        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            ListBuff.Clear();

            var Ans = App.Db.GetFILIERE_NIVEAUX (OpenedFiliere);
                       
            foreach(int A in Ans)
            {
                var MM = new MatiereViewModel {ANNEE_NAME = A + " Annee"};

                var MOFY = App.Db.GetMatieresOfFiliereYear(OpenedFiliere, A);
                foreach(var M in MOFY)
                {
                    var MMM = new MatiereModel
                    {
                        MATIERE_ID = M.MATIERE_ID,
                        NAME = M.NAME,
                        HEURES_PAR_SEMAINE = M.GetHEURE_PAR_SEMAINE(OpenedFiliere, Convert.ToInt32(A)),
                        INSTRUCTEURS_COUNT = App.Db.GetNofMatiereInstructor(M.MATIERE_ID)
                    };
                    MM.MATIERES_MODEL_LIST.Add (MMM);
                }
                ListBuff.Add (MM);
            }                       
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            AnneeList.ItemsSource = new List<MatiereViewModel>();
            AnneeList.ItemsSource = ListBuff;
            isFistTime = true;
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
