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

namespace Matrix.Controls
{
    /// <summary>
    /// Interaction logic for ToolbarStudentViews.xaml
    /// </summary>
    public partial class ToolbarStudentViews : UserControl
    {
        public ToolbarStudentViews ( )
        {
            InitializeComponent ();
        }

        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            //MainWindow.MainFrame.NavigationService.Navigate (new Uri ("/views/HomePage.xaml", UriKind.Relative));
        }


    }
}
