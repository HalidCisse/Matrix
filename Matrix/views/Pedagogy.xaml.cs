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
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views
{
    
    public partial class Pedagogy
    {
        public Pedagogy ( )
        {
            InitializeComponent ();            
        }
        
       
        private void BackBut_Click ( object sender, RoutedEventArgs e )
        {
            if(NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack ();
            }
        }

        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            if(NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack ();
            }
        }
       
        private void AddClasseButton_Click ( object sender, RoutedEventArgs e )
        {

            var wind = new AddClass { Owner = Window.GetWindow (this), OpenOption = "Add" };
            wind.ShowDialog ();
            

            //var cm = FindResource ("cmButton") as ContextMenu;
            //cm.PlacementTarget = sender as Button;
            //cm.IsOpen = true;
        }

        private void DeleteClasseButton_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void AddFiliere_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void AddClasse_Click ( object sender, RoutedEventArgs e )
        {

        }


    }
}
