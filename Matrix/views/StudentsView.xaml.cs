using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using DataService.Entities;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for StudentsView.xaml
    /// </summary>
    public partial class StudentsView
    {
       

        public StudentsView ( ) {
            InitializeComponent ();               
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e ) {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            if (worker.IsBusy) return;
            worker.RunWorkerAsync();           
        }
      
       
        private void StudentsViewBackButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate (new Uri ("/views/HomePage.xaml", UriKind.Relative));
        }

        private void Studentslist_MouseDoubleClick ( object sender, MouseButtonEventArgs e ) {
            if (Studentslist == null) return;
            if (Studentslist.SelectedValue == null) return;
            var wind = new StudentINFO (Studentslist.SelectedValue.ToString()) { Owner = Window.GetWindow (this) };
            wind.OpenOption = "Mod";
            wind.ShowDialog();
            Studentslist.ItemsSource = App.Db.GetAllStudents ();
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new StudentINFO { Owner = Window.GetWindow (this) };
            wind.OpenOption = "Add";
            wind.ShowDialog ();
            Studentslist.ItemsSource = App.Db.GetAllStudents ();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(Studentslist.SelectedValue == null)
            {
                MessageBox.Show("Selectionner Un Etudiant A Supprimer !");
                return;
            }

            var theGaName = App.Db.GetStudentName(Studentslist.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if(MessageBox.Show (theGaName,"",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {              
                MessageBox.Show (App.Db.DeleteStudent (Studentslist.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
                Studentslist.ItemsSource = App.Db.GetAllStudents ();
            }
        }

          

        

        #region Background Works

        private readonly BackgroundWorker worker = new BackgroundWorker ();
        
        private List<Student> StudentsBuff;

        private void worker_DoWork(object sender, DoWorkEventArgs e) {                     
            StudentsBuff = App.Db.GetAllStudents ();            
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {          
            Studentslist.ItemsSource = StudentsBuff;
            worker.Dispose();
        }

        #endregion

        







    }
}
