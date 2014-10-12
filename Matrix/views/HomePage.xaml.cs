using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage
    {
        public HomePage ( ) {
            InitializeComponent ();
        }

        #region Home Buttons Commandes
        private void StudentButton_Click ( object sender, RoutedEventArgs e ) {  
         
            NavigationService.Navigate (new Uri ("/views/studentsView.xaml", UriKind.Relative));         
        }

        private void PedagogieButton_Click ( object sender, RoutedEventArgs e ) {

        }
        private void StaffButton_Click ( object sender, RoutedEventArgs e ) {

            NavigationService.Navigate (new Uri ("/views/StaffsView.xaml", UriKind.Relative)); 
        }

        private void AgendaButton_Click ( object sender, RoutedEventArgs e ) {


        }
        private void FinanceButton_Click ( object sender, RoutedEventArgs e ) {


        }
       
        private void StatisticButton_Click ( object sender, RoutedEventArgs e ) {


        }

        

        #endregion

        

       


    }
}
