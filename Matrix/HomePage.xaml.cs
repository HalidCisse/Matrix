using System;
using System.Windows;
using Matrix.views;
using Matrix.views.Pedagogy;
using Matrix.views.Staffs;
using Matrix.views.Students;

namespace Matrix
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class HomePage
    {
        /// <summary>
        /// 
        /// </summary>
        public HomePage ( ) {

            InitializeComponent ();

        }

        #region Home Buttons Commandes

        private void StudentButton_Click ( object sender, RoutedEventArgs e ) {           
            NavigationService?.Navigate (new StudentsView(), UriKind.Relative);         
        }

        private void PedagogieButton_Click ( object sender, RoutedEventArgs e ) {
            NavigationService?.Navigate (new PedagogyView(), UriKind.Relative);
        }

        private void StaffButton_Click ( object sender, RoutedEventArgs e ) {
            NavigationService?.Navigate (new StaffsView(), UriKind.Relative); 
        }

        private void AgendaButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService?.Navigate (new StudentPage(), UriKind.Relative);
        }

        private void EconomatButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatisticButton_Click ( object sender, RoutedEventArgs e ) {


        }



        #endregion

        
    }
}
