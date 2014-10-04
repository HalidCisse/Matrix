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
    /// Interaction logic for StudentPage.xaml
    /// </summary>
    public partial class StudentPage : Page
    {
        public StudentPage ( )
        {
            InitializeComponent ();
        }

        private void BackBut_Click ( object sender, RoutedEventArgs e )
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack ();
            }
        }
    }
}
