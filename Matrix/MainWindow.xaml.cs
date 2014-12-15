using System.Windows;
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




        #region Animation

        //Navigating="MainFrame_Navigating"

        //private bool _allowDirectNavigation;
        //private NavigatingCancelEventArgs _navArgs;
        //private readonly Duration _duration = new Duration (TimeSpan.FromSeconds (.001));
        //private double _oldHeight = 0;
        ////NavigationEventArgs
        //private void MainFrame_Navigating ( object sender, NavigatingCancelEventArgs e ) {
        //    if(Content!=null && !_allowDirectNavigation) {
        //        e.Cancel = true;

        //        _navArgs = e;
        //        _oldHeight = MainFrame.ActualHeight;

        //        //DoubleAnimation animation0 = new DoubleAnimation ();
        //        //animation0.From = MainFrame.ActualHeight;
        //        //animation0.To = 0;
        //        //animation0.Duration = _duration;
        //        //animation0.Completed += SlideCompleted;
        //        //MainFrame.BeginAnimation (HeightProperty, animation0);

        //        var animation0 = new DoubleAnimation {From = MainFrame.Opacity, To = 0, Duration = _duration};

        //        animation0.Completed += SlideCompleted;
        //        MainFrame.BeginAnimation (OpacityProperty, animation0);

                
        //    }
        //    _allowDirectNavigation = false;
        //}
        //private void SlideCompleted ( object sender, EventArgs e ) {
        //    _allowDirectNavigation = true;
        //    switch(_navArgs.NavigationMode) {
        //        case NavigationMode.New:
        //            if(_navArgs.Uri == null)
        //                MainFrame.Navigate (_navArgs.Content);
        //            else
        //                MainFrame.Navigate (_navArgs.Uri);
        //            break;
        //        case NavigationMode.Back:
        //            MainFrame.GoBack ();
        //            break;
        //        case NavigationMode.Forward:
        //            MainFrame.GoForward ();
        //            break;
        //        case NavigationMode.Refresh:
        //            MainFrame.Refresh ();
        //            break;
        //    }

        //    Dispatcher.BeginInvoke (DispatcherPriority.Loaded, (ThreadStart)delegate ( ) {
        //            //DoubleAnimation animation0 = new DoubleAnimation ();
        //            //animation0.From = 0;
        //            //animation0.To = _oldHeight;
        //            //animation0.Duration = _duration;
        //            //MainFrame.BeginAnimation (HeightProperty, animation0);

        //        var animation0 = new DoubleAnimation
        //        {
        //            From = 0,
        //            To = 1,
        //            Duration = new Duration(TimeSpan.FromSeconds(.050))
        //        };
        //                                                                                     ;
        //        MainFrame.BeginAnimation ( OpacityProperty, animation0);
                    
        //        });
        //}

        #endregion

        private void SeetingButton_OnClick(object sender, RoutedEventArgs e)
        {
            MyFlyout.IsOpen = true;


            //var wind = new SettingsView() { Owner = Window.GetWindow(this) };
            //wind.Show();
        }
    }     
}
