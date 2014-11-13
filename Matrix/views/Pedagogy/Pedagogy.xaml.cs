using System.Windows;

namespace Matrix.views
{
    
    public partial class PedagogyD
    {
        public PedagogyD ( )
        {
            InitializeComponent ();            
        }
        
       
        private void BackBut_Click ( object sender, RoutedEventArgs e )
        {
            if(NavigationService != null)
            {
                NavigationService.Navigate (new HomePage ());
            }
        }

    }
}
