using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DataService.Entities;
using DataService.ViewModel;
using Matrix.Controls;

namespace Matrix.views.Pedagogy
{

    /// <summary>
    /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe donnee
    /// </summary>
    public partial class ClassDetails
    {
        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private List<Matiere> MatieresListBuff = new List<Matiere>();        
        private List<Student> StudentsListBuff = new List<Student> ();
        private List<DayCoursCards> AgendaData = new List<DayCoursCards>();
        private string CurrentSelected;                
        private readonly Classe OpenedClass;

        /// <summary>
        /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe donnee
        /// </summary>
        /// <param name="OpenClassID"> ID De la Classe</param>
        public ClassDetails ( Guid OpenClassID )
        {
            InitializeComponent ();

            OpenedClass = App.DataS.GetClasseByID (OpenClassID);
                       
            ClassName.Text = OpenedClass.NAME.ToUpper ();
            ClassFiliere.Text = App.DataS.GetFiliereByID(OpenedClass.FILIERE_ID).NAME.ToUpper();
            
        }      
  

        #region EVENT HANDLERS

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            AgendaUI.Items.Clear();
            ClassWeekSchedule.SelectionChanged += ClassWeekSchedule_SelectionChanged;
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateData ();
            
        }

        private void ClassWeekSchedule_SelectionChanged(object sender, EventArgs e)
        {
            var List = sender as ListBox;

            if (List?.SelectedValue == null) return;

            CurrentSelected = List.SelectedValue.ToString();
            MessageBox.Show(List.SelectedValue.ToString());
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
        
        private void MatieresList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var Matieres = sender as ListBox;
            if(Matieres?.SelectedValue == null) return;
            var MatiereToDisplay = App.DataS.GetMatiereByID (new Guid(Matieres.SelectedValue.ToString ()));

            var wind = new AddMatiere (OpenedClass.CLASSE_ID, MatiereToDisplay) { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }

        private void BackButton_Click ( object sender, RoutedEventArgs e )
        {
            var navigationService = NavigationService;
            navigationService?.Navigate (new PedagogyView ());
        }
        
        private void StudentsList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {

        }     
        
        private void AddCours_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddCours (OpenedClass.CLASSE_ID) { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }

        private void AddMatiere_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddMatiere (OpenedClass.CLASSE_ID) { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }

        private void DayCoursList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var List = sender as ListBox;

            if (List?.SelectedValue == null) return;

            CurrentSelected = List.SelectedValue.ToString();

            MessageBox.Show("1 ere => " +List.SelectedValue.ToString());
        }

        private void DayCoursList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            var wind = new AddCours(OpenedClass.CLASSE_ID, App.DataS.GetCoursByID(new Guid(list.SelectedValue.ToString()))) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AgendaUI_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AgendaUI.Items.Clear();
        }

        #endregion


        #region BACKGROUND WORKER

        private void UpdateData ( )
        {           
            if (Worker.IsBusy) { Worker.CancelAsync();};
            Worker.RunWorkerAsync ();           
        }

        private void Worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            ClassWeekSchedule.UpdateData(OpenedClass.CLASSE_ID);
            AgendaData = App.ModelS.GetClassWeekAgendaData(OpenedClass.CLASSE_ID, DateTime.Now);
            //MatieresListBuff = App.DataS.GetClassMatieres (OpenedClass.CLASSE_ID);            
            //StudentsListBuff = App.DataS.GetClassStudents (OpenedClass.CLASSE_ID);
            
            Worker.Dispose();
        }
        private void Worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            
            AgendaUI.ItemsSource = AgendaData;
            //ClassWeekSchedule.AgendaData = AgendaData;
            //MatieresList.ItemsSource = MatieresListBuff;
            //StudentsList.ItemsSource = StudentsListBuff;
            //ClassWeekSchedule.AgendaData = AgendaData;
            Worker.Dispose();
        }

        #endregion

        private void ClassWeekSchedule_OnLoaded(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
