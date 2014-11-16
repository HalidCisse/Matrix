using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

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

            ModernDialog.ShowMessage ("Hi Going Back do you like it Hi Going Back do you like it", "Matrix", MessageBoxButton.YesNo);
           

            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack ();
            }
        }
    }
}
