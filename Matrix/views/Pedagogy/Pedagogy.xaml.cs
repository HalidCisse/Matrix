using System.Windows;

namespace Matrix.views.Pedagogy
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class PedagogyD
    {
        /// <summary>
        /// 
        /// </summary>
        public PedagogyD ( )
        {
            InitializeComponent ();            
        }
        
       
        private void BackBut_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService?.Navigate (new HomePage ());
        }
    }
}
