using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.Entities;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for MatieresView.xaml
    /// </summary>
    public partial class MatieresView
    {
        public MatieresView ( string OpenFiliere )
        {
            InitializeComponent ();

            OpenedFiliere = OpenFiliere;
        }

        public string OpenedFiliere { get; set; }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
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
            if(MatiereList.SelectedValue == null)
            {
                MessageBox.Show ("Selectionner Une Matiere A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetMatiereName (MatiereList.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            MessageBox.Show (App.Db.DeleteMatiere (MatiereList.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
            UpdateMatieres ();
        }

        private void MatiereList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            //if(FiliereList == null) return;
            //if(FiliereList.SelectedValue == null) return;
            //var wind = new AddFiliere (FiliereList.SelectedValue.ToString ())
            //{
            //    Owner = Window.GetWindow (this),
            //    OpenOption = "Mod"
            //};
            //wind.ShowDialog ();
            //UpdateFilieres ();
        }




        #region Background Works

        private readonly BackgroundWorker worker = new BackgroundWorker ();

        private List<Matiere> MatieresBuff;

        private void UpdateMatieres ( )
        {
            if(worker.IsBusy) return;
            BusyIndicator.IsBusy = true;
            worker.RunWorkerAsync ();
        }

        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            MatieresBuff = App.Db.GetAllMatieres ();
        }

        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            MatiereList.ItemsSource = MatieresBuff;
            worker.Dispose ();
        }

        #endregion

    }
}
