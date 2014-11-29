using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.Entities;

namespace Matrix.views.Pedagogy
{

    /// <summary>
    /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe donnee
    /// </summary>
    public partial class ClassDetails
    {       
        private string CurrentSelected;
        private Classe OpenedClass = new Classe();
        
        /// <summary>
        /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe donnee
        /// </summary>
        /// <param name="OpenClassID"> ID De la Classe</param>
        public ClassDetails ( Guid OpenClassID )
        {
            InitializeComponent ();

            OpenedClass.CLASSE_ID = OpenClassID;
            
            new Task(() =>
            {
                OpenedClass = App.DataS.GetClasseByID(OpenClassID);
                Dispatcher.BeginInvoke(new Action(() => { ClassFiliere.Text = App.DataS.GetFiliereByID(OpenedClass.FILIERE_ID).NAME.ToUpper(); }));
                Dispatcher.BeginInvoke(new Action(() => { ClassName.Text = OpenedClass.NAME.ToUpper(); }));               
            }).Start();

        }      
  

        #region EVENT HANDLERS

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {                                            
            UpdateData ();            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = NavigationService;
            navigationService?.Navigate(new PedagogyView());
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            // //var SelectedClasse = App.DataS.GetCoursByID (new Guid(CurrentSelected));
            //var wind = new AddCours (OpenedClass.CLASSE_ID) { Owner = Window.GetWindow (this) };
            //wind.ShowDialog ();
            //UpdateData ();

            var cm = FindResource ("AddContext") as ContextMenu;
            if (cm == null) return;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void AddCours_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddCours(OpenedClass.CLASSE_ID) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddMatiere_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddMatiere(OpenedClass.CLASSE_ID) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            //if(CurrentSelected == null)
            //{
            //    MessageBox.Show ("Selectionner Une Matiere A Supprimer !");
            //    return;
            //}

            //var theGaName = App.Db.GetMatiereName (CurrentSelected);
            //theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            //if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            //MessageBox.Show (App.Db.DeleteMatiere (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
            //UpdateMatieres ();
        }

        private void ClassWeekSchedule_OnSelectionChanged(object sender, EventArgs e)
        {
            var ID = sender as string;
            CurrentSelected = ID;
        }

        private void MatieresList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var Matieres = sender as ListBox;
            if(Matieres?.SelectedValue == null) return;
            var MatiereToDisplay = App.DataS.GetMatiereByID (new Guid(Matieres.SelectedValue.ToString ()));

            var wind = new AddMatiere (OpenedClass.CLASSE_ID, MatiereToDisplay) { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }        
        
        private void StudentsList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {

        }     
        

        #endregion




        #region DATA WORKER

        private void UpdateData ( )
        {
            Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(OpenedClass.CLASSE_ID); }));

            var DataTask = new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(OpenedClass.CLASSE_ID); }));              
            });            
            DataTask.ContinueWith(Cont =>
            {
                Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.GetClassStudents(OpenedClass.CLASSE_ID); }));                
            });
            DataTask.Start();            
        }        

        #endregion

        
    }
}















//Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(OpenedClass.CLASSE_ID); }));
//Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(OpenedClass.CLASSE_ID); }));
//Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.GetClassStudents(OpenedClass.CLASSE_ID); }));