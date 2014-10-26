using System.Windows;

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
            if(NavigationService != null)
            {
                NavigationService.Navigate (new HomePage ());
            }
        }

    }
}
