using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DataService.Entities;

namespace Matrix.views
{
    
    public partial class StudentsView
    {
       
        public StudentsView ( ) {
            InitializeComponent ();               
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e ) {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateStudents();         
        }
      
       
        private void StudentsViewBackButton_Click ( object sender, RoutedEventArgs e )
        {
            if (NavigationService != null)
                NavigationService.Navigate (new HomePage(), UriKind.Relative);
        }

        private void Studentslist_MouseDoubleClick ( object sender, MouseButtonEventArgs e ) {
            if (Studentslist == null) return;
            if (Studentslist.SelectedValue == null) return;
            var wind = new StudentINFO (Studentslist.SelectedValue.ToString())
            {
                Owner = Window.GetWindow(this),
                OpenOption = "Mod"
            };
            wind.ShowDialog();
            UpdateStudents ();
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new StudentINFO {Owner = Window.GetWindow(this), OpenOption = "Add"};
            wind.ShowDialog ();
            UpdateStudents ();           
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(Studentslist.SelectedValue == null) {
                MessageBox.Show("Selectionner Un Etudiant A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetStudentName(Studentslist.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            MessageBox.Show (App.Db.DeleteStudent (Studentslist.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
            UpdateStudents ();
        }

          
       

        #region Background Works

        private readonly BackgroundWorker worker = new BackgroundWorker ();
        
        private List<Student> StudentsBuff;

        private void UpdateStudents()
        {
            if(worker.IsBusy) return;
            worker.RunWorkerAsync ();          
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {                     
            StudentsBuff = App.Db.GetAllStudents ();            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            BusyIndicator.IsBusy = false;
            Studentslist.ItemsSource = StudentsBuff;           
            worker.Dispose();
        }

        #endregion

        
    }
}
