using System.Windows;
using System.Windows.Controls;

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
