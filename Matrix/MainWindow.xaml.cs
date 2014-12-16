using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

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
            //MainFrame.Navigate (new Uri ("/views/HomePage.xaml", UriKind.Relative));
        }




       
        private void SeetingButton_OnClick(object sender, RoutedEventArgs e)
        {
            MyFlyout.IsOpen = true;


            //var wind = new SettingsView() { Owner = Window.GetWindow(this) };
            //wind.Show();
        }

        private void AnneeScolaireBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void AnneeScolaireBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void MyFlyout_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //if (MyTabControl.IsFocused) return;
            //MyFlyout.IsOpen = false;
        }
      
        private void MyTabControl_OnLostFocus(object sender, RoutedEventArgs e)
        {
            //if (MyFlyout.IsFocused) return;

            //foreach (TabItem tab in MyTabControl.Items)
            //{
            //    if (tab.IsSelected) return;
               
            //}

            //MyFlyout.IsOpen = false;
        }

        private void MyTabControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }     
}
