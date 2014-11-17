using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.Entities;
using DataService.Model;
using Matrix.views.Pedagogy;

namespace Matrix.views
{
    
    public partial class ClassDetails
    {
        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private List<Matiere> MatieresListBuff = new List<Matiere>();
        private List<Staff> StaffListBuff = new List<Staff> ();
        private List<Student> StudentsListBuff = new List<Student> ();
        //private List<CoursCard> CoursListBuff = new List<CoursCard> ();
        private string CurrentSelected;
        private bool isFirstTime = true;
        private Filiere OpenedFiliere;
        private Classe OpenedClass;

        public ClassDetails ( Guid OpenClassID )
        {
            InitializeComponent ();

            OpenedClass = App.DataS.GetClasseByID (OpenClassID);
            OpenedFiliere = App.DataS.GetFiliereByID (OpenedClass.FILIERE_ID);
            ClassName.Text = OpenedClass.NAME.ToUpper ();
            ClassFiliere.Text = OpenedFiliere.NAME.ToUpper();
        }
       
        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
           // //var SelectedClasse = App.DataS.GetCoursByID (new Guid(CurrentSelected));
           //var wind = new AddCours (OpenedClass.CLASSE_ID) { Owner = Window.GetWindow (this) };
           //wind.ShowDialog ();
           //UpdateData ();

           var cm = FindResource ("AddContext") as ContextMenu;
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

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            Worker.DoWork += Worker_DoWork;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateData (); 
        }

        private void UpdateData()
        {
            if(Worker.IsBusy) return;
            Worker.RunWorkerAsync ();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            MatieresListBuff = App.DataS.GetClassMatieres (OpenedClass.CLASSE_ID); 
            StaffListBuff = App.DataS.GetClassStaffs (OpenedClass.CLASSE_ID);
            StudentsListBuff = App.DataS.GetClassStudents (OpenedClass.CLASSE_ID);
            //CoursListBuff = App.ModelS.GetClassCoursCards (OpenedFiliere.FILIERE_ID, OpenedClass.LEVEL); 


        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BusyIndicator.IsBusy = false;

            MatieresList.ItemsSource = MatieresListBuff;
            isFirstTime = true;
            //Worker.Dispose ();
        }

        
        private void MatieresList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var Matieres = sender as ListBox;
            if(Matieres == null) return;
            if(Matieres.SelectedValue == null) return;
            var MatiereToDisplay = App.DataS.GetMatiereByID (new Guid(Matieres.SelectedValue.ToString ()));

            var wind = new AddMatiere (OpenedClass.CLASSE_ID, MatiereToDisplay) { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }

        private void BackButton_Click ( object sender, RoutedEventArgs e )
        {
            var navigationService = NavigationService;
            if(navigationService != null)
                navigationService.Navigate (new FilieresView ());
        }

        private void CoursList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {

        }

        private void StudentsList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {

        }

        private void StaffList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
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












    }
}
