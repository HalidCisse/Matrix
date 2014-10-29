using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DataService.ViewModel;

namespace Matrix.views
{
    

    public partial class FilieresView
    {

        private readonly BackgroundWorker worker = new BackgroundWorker ();

        private List<FiliereCard> FilieresBuff = new List<FiliereCard> ();

        public FilieresView ( )
        {
            InitializeComponent ();
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateFilieres ();
        }

        private void FiliereDeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(FiliereList.SelectedValue == null)
            {
                MessageBox.Show ("Selectionner Une Filiere A Supprimer !");
                return;
            }

            var theGaName = App.DataS.GetFiliereName (FiliereList.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            MessageBox.Show (App.DataS.DeleteFiliere (FiliereList.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
            UpdateFilieres ();
        }

        private void FiliereAddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddFiliere {Owner = Window.GetWindow(this), OpenOption = "Add"};
            wind.ShowDialog ();
            UpdateFilieres();
        }
       
        private void Filiere_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            if(FiliereList == null) return;
            if(FiliereList.SelectedValue == null) return;

            var navigationService = NavigationService;
            if(navigationService != null)
                navigationService.Navigate (new MatieresView (FiliereList.SelectedValue.ToString ()));
        }

        private void UpdateFilieres ( )
        {
            if(worker.IsBusy) return;            
            worker.RunWorkerAsync ();
        }

        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            FilieresBuff = App.ModelS.GetAllFilieresCards ();            
        }

        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            FiliereList.ItemsSource = FilieresBuff;
            worker.Dispose ();
        }

       

    }
}
