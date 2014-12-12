using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Extention;

namespace Matrix.views.Pedagogy
{

    /// <summary>
    /// Affiche les filieres et leurs classes
    /// </summary>
    public partial class PedagogyView
    {
        
        private Guid _currentSelected;
        
        /// <summary>
        /// Affiche les filieres et leurs classes
        /// </summary>
        public PedagogyView ( )
        {
            InitializeComponent ();          
        }

        private void UpdateData()
        {
            BusyIndicator.IsBusy = true;

            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    FiliereList.ItemsSource = App.ModelS.GetFiliereClassCards();
                    BusyIndicator.IsBusy = false;
                }));
            }).Start();            
        }

        #region Eventhandlers

        private void PedagogyView_OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            
            var theClass = App.DataS.Pedagogy.Classes.GetClasseById (_currentSelected);
           
            var theName = "Ete Vous Sure de supprimer " + theClass.Name + " definitivement ?";

            var md = new ModernDialog { Title = "Matrix", Content = theName };
                      
            var r = md.ShowDialogOkCancel ();
            if (r != MessageBoxResult.OK) return;
           
            try
            {
                App.DataS.Pedagogy.Classes.DeleteClasse (theClass.ClasseId);
            }
            catch(Exception ex)
            {
                ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                return;
            }
                                  
            if (App.DataS.Pedagogy.Filieres.GetFiliereClassCount(theClass.FiliereId) < 1){
                if(MessageBox.Show ("Vouler Vous Supprimer " + App.DataS.Pedagogy.Filieres.GetFiliereById (theClass.FiliereId).Name + " definitivement ?")!= MessageBoxResult.Yes) return;

                try
                {
                    App.DataS.Pedagogy.Filieres.DeleteFiliere (theClass.FiliereId);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                    return;
                }                
            }       
            UpdateData ();
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            var cm = FindResource ("AddContext") as ContextMenu;
            if (cm == null) return;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void AddFiliere_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddFiliere { Owner = Window.GetWindow (this), OpenOption = "Add" };
            wind.ShowDialog ();
            UpdateData ();
        }

        private void AddClasse_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddClass { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }
               
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {           
            NavigationService?.Navigate(new HomePage());
        }

        private void ClassList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var list = sender as ListBox;
            if(list?.SelectedValue == null) return;
            
            NavigationService?.Navigate(new ClassDetails(new Guid(list.SelectedValue.ToString())));
        }

        private void ClassList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var classes = sender as ListBox;

            if(classes?.SelectedValue == null) return;

            _currentSelected = new Guid (classes.SelectedValue.ToString ());
        }

        private void ClassContextDel_Click(object sender, RoutedEventArgs e)
        {
            var list = ((FrameworkElement)sender).Tag as ListBox;

            if (list?.SelectedValue == null) return;
            
            var theName = App.DataS.Pedagogy.Classes.GetClasseName(new Guid(list.SelectedValue.ToString()));
            theName = "Ete Vous Sure de supprimer " + theName + " definitivement ?";

            var md = new ModernDialog { Title = "Matrix", Content = theName };

            var r = md.ShowDialogOkCancel();
            if (r != MessageBoxResult.OK) return;

            try
            {
                App.DataS.Pedagogy.Classes.DeleteClasse(new Guid(list.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
                return;
            }
            
            ModernDialog.ShowMessage("Supprimer Avec Success", "Matrix", MessageBoxButton.OK);

            UpdateData();
        }

        private void ClassContextMod_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)e.Source;
            var menu = (ContextMenu)menuItem.Parent;
            var list = (ListBox)menu.PlacementTarget;
            if (list?.SelectedValue == null) return;

            var wind = new AddClass(App.DataS.Pedagogy.Classes.GetClasseById(new Guid(list.SelectedValue.ToString()))) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void ClassContextDetail_OnClick(object sender, RoutedEventArgs e)
        {
            var list = ((FrameworkElement)sender).Tag as ListBox;

            if (list?.SelectedValue == null) return;

            NavigationService?.Navigate(new ClassDetails(new Guid(list.SelectedValue.ToString())));
        }




        #endregion

        
    }

        
}
