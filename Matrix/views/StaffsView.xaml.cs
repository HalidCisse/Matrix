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
    /// Interaction logic for StaffsView.xaml
    /// </summary>
    public partial class StaffsView : Page
    {
        public StaffsView ( )
        {
            InitializeComponent ();
        }


        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {

        }

        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService.Navigate (new Uri ("/views/HomePage.xaml", UriKind.Relative));
        }

        private void AddButton_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void StaffList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {

        }
    }
}
