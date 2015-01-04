using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using  CLib;
//using Matrix.Utils;

namespace Matrix.views.Staffs
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class StaffsView
    {        
        private string _currentSelected;        
        private bool _isFirstTime = true;

        /// <summary>
        /// 
        /// </summary>
        public StaffsView ( ) {

            InitializeComponent ();
        }

        private void UpdateData()
        {
            BUSY_INDICATOR.IsBusy = true;

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {                  
                    STAFF_LIST.ItemsSource = App.ModelS.GetDepStaffsCard();
                    BUSY_INDICATOR.IsBusy = false;
                }));
            }).Start();

            _isFirstTime = true;
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {           
            UpdateData();            
        }

        
        private void HomeButton_Click ( object sender, RoutedEventArgs e )
        {
            NavigationService?.Navigate (new HomePage(), UriKind.Relative);
        }

        private void AddButton_Click ( object sender, RoutedEventArgs e )
        {
            BUSY_INDICATOR.IsBusy = false;
            var wind = new StaffInfo { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            if(_currentSelected == null)
            {
                MessageBox.Show ("Selectionner Un Staff A Supprimer !");
                return;
            }

            var theGaName = App.DataS.Hr.GetStaffFullName (new Guid(_currentSelected));
            theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            if (MessageBox.Show(theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            MessageBox.Show (App.DataS.Hr.DeleteStaff (new Guid(_currentSelected)) ? "Supprimer Avec Succes" : "Echec");
            UpdateData();
        }       

        private void DepStaffList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var staff = sender as ListBox;

            if(staff == null) return;

            if(staff.SelectedValue == null)
            {
                _currentSelected = null;
                return;
            }
            _currentSelected = staff.SelectedValue.ToString ();
        }
       
        private void DepStaffList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {          
            var staff = sender as ListBox;
            if(staff?.SelectedValue == null) return;

            var wind = new StaffInfo (staff.SelectedValue.ToString ()) {
                Owner = Window.GetWindow(this),              
            };
            wind.ShowDialog ();
            UpdateData();  
        }
 
        private void DepStaffList_Loaded ( object sender, RoutedEventArgs e )
        {
            if (!_isFirstTime) return;
           
            var eX = FindVisual.FindVisualChildren<Expander>(this).First();

            if (eX != null) eX.IsExpanded = true;
            _isFirstTime = false;                     
        }
      
        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var eX = sender as Expander;
                     
            foreach(var ep in FindVisual.FindVisualChildren<Expander>(this).Where(ep => eX != null && ep.Header?.ToString() != eX.Header?.ToString()))
            {
                ep.IsExpanded = false;                
            }
        }

             
    }
}
