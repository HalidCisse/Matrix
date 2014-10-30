using System;
using System.Windows;
using System.Windows.Navigation;

namespace Matrix.views
{
    
    public partial class HomePage
    {
        public HomePage ( ) {
            InitializeComponent ();
        }

        #region Home Buttons Commandes


        private void StudentButton_Click ( object sender, RoutedEventArgs e ) {           
            NavigationService.Navigate (new StudentsView(), UriKind.Relative);         
        }

        private void PedagogieButton_Click ( object sender, RoutedEventArgs e ) {
            NavigationService.Navigate (new FilieresView(), UriKind.Relative);
        }

        private void StaffButton_Click ( object sender, RoutedEventArgs e ) {

            NavigationService.Navigate (new StaffsView(), UriKind.Relative); 
        }

        private void AgendaButton_Click ( object sender, RoutedEventArgs e ) {

            NavigationService.Navigate (new StudentPage(), UriKind.Relative);
        }

        private void FinanceButton_Click ( object sender, RoutedEventArgs e ) {


        }
       
        private void StatisticButton_Click ( object sender, RoutedEventArgs e ) {


        }

        

        #endregion

        

       


    }
}
