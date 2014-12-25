using System.Windows;
using Matrix.views;

namespace Matrix
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
       
        /// <summary>
        /// 
        /// </summary>
        public MainWindow ( ) {

            InitializeComponent ();            

        }
       
        private void SeetingButton_OnClick(object sender, RoutedEventArgs e)
        {
            MY_FLYOUT.IsOpen = true;
        }
                     
        private void MyFlyout_OnIsOpenChanged(object sender, RoutedEventArgs e)
        {
            if (!MY_FLYOUT.IsOpen) return;

            SETTING_FRAME.Navigate(new SettingsView());
        }

       


    }     
}
