using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Matrix
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow
    {
        private bool _isFirstTime;
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

        private void AnneeScolaireBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            _isFirstTime = true;
        }

        private void AnneeScolaireBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isFirstTime) { _isFirstTime = false; return;}
          
            App.DataS.Pedagogy.CurrentAnneeScolaireGuid = new Guid(AnneeScolaireBox.SelectedValue.ToString());
        }
             
        private void MyFlyout_IsOpenChanged(object sender, RoutedEventArgs e)
        {
            if (MyFlyout.IsOpen) UpdateData();       
        }

        private void UpdateData()
        {
            _isFirstTime = true;

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {                    
                    AnneeScolaireBox.ItemsSource = App.DataS.Pedagogy.GetAllAnneeScolaires();                   
                    AnneeScolaireBox.SelectedValue = App.DataS.Pedagogy.CurrentAnneeScolaireGuid;
                    
                    

                }));
                
            }).Start();

        }


        private void MyFlyout_OnLoaded(object sender, RoutedEventArgs e)
        {
            MyFlyout.IsOpenChanged += MyFlyout_IsOpenChanged;
        }
    }     
}
