using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Matrix.views.Students
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class StudentsView
    {       
        /// <summary>
        /// 
        /// </summary>
        public StudentsView ( )
        {
            InitializeComponent ();               
        }

        private void UpdateData()
        {
            BUSY_INDICATOR.IsBusy = true;

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    STUDENTSLIST.ItemsSource = App.DataS.Students.GetAllStudents();
                    BUSY_INDICATOR.IsBusy = false;
                }));
            }).Start();
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            UpdateData();         
        }
             
        private void StudentsViewBackButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService?.Navigate (new HomePage(), UriKind.Relative);
        }

        private void Studentslist_MouseDoubleClick ( object sender, MouseButtonEventArgs e ) {
            if (STUDENTSLIST?.SelectedValue == null) return;
            var wind = new StudentInfo(STUDENTSLIST.SelectedValue.ToString()) {Owner = Window.GetWindow(this)};
            wind.ShowDialog();
            UpdateData();
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new StudentInfo {Owner = Window.GetWindow(this)};
            wind.ShowDialog ();
            UpdateData();           
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(STUDENTSLIST.SelectedValue == null) {
                MessageBox.Show("Selectionner Un Etudiant A Supprimer !");
                return;
            }

            var theGaName = App.DataS.Students.GetStudentName(STUDENTSLIST.SelectedValue.ToString ());
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            MessageBox.Show (App.DataS.Students.DeleteStudent (STUDENTSLIST.SelectedValue.ToString ()) ? "Supprimer Avec Succes" : "Echec");
            UpdateData();
        }

     
    }
}
