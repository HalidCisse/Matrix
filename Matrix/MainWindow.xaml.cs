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
            MyFlyout.IsOpen = true;
        }
                     
        private void MyFlyout_OnIsOpenChanged(object sender, RoutedEventArgs e)
        {
            if (!MyFlyout.IsOpen) return;

            SettingFrame.Navigate(new SettingsView());
        }

       


    }     
}
