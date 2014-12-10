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
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
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
            var wind = new StudentInfo (Studentslist.SelectedValue.ToString())
            {
                Owner = Window.GetWindow(this),
                OpenOption = "Mod"
            };
            wind.ShowDialog();
            UpdateStudents ();
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new StudentInfo {Owner = Window.GetWindow(this), OpenOption = "Add"};
            wind.ShowDialog ();
            UpdateStudents ();           
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(Studentslist.SelectedValue == null) {
                MessageBox.Show("Selectionner Un Etudiant A Supprimer !");
                return;
            }

            var theGaName = App.DataS.Students.GetStudentName(Studentslist.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            MessageBox.Show (App.DataS.Students.DeleteStudent (Studentslist.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
            UpdateStudents ();
        }

          
       

        #region Background Works

        private readonly BackgroundWorker _worker = new BackgroundWorker ();
        
        private List<Student> _studentsBuff;

        private void UpdateStudents()
        {
            if(_worker.IsBusy) return;
            _worker.RunWorkerAsync ();          
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {                     
            _studentsBuff = App.DataS.Students.GetAllStudents ();            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            BusyIndicator.IsBusy = false;
            Studentslist.ItemsSource = _studentsBuff;           
            _worker.Dispose();
        }

        #endregion

        
    }
}
