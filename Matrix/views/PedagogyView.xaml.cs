using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.ViewModel;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Extention;
using Matrix.Utils;

namespace Matrix.views
{
    
    public partial class PedagogyView
    {
        
        private Guid CurrentSelected;
        private bool isFirstTime = true;
        private readonly BackgroundWorker worker = new BackgroundWorker ();
        private List<FiliereClassCard> FilieresBuff = new List<FiliereClassCard> ();

        public PedagogyView ( )
        {
            InitializeComponent ();
        }
        
        private void UpdateData ( )
        {
            if(worker.IsBusy) return;            
            worker.RunWorkerAsync ();
        }

        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {            
            FilieresBuff = App.ModelS.GetFiliereClassCards ();
        }

        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            FiliereList.ItemsSource = FilieresBuff;
            worker.Dispose ();
        }


        #region Eventhandler

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateData ();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            
            MessageBox.Show (CurrentSelected + "");


            var TheClass = App.DataS.GetClasseByID (CurrentSelected);


            var theName = App.DataS.GetClasseName (CurrentSelected);
            theName = "Ete Vous Sure de supprimer " + TheClass.NAME + " definitivement ?";

            var MD = new ModernDialog {
                Title = "Matrix",
                Content = theName
            };
                      
            var r = MD.ShowDialogOKCancel ();
            if (r != MessageBoxResult.OK)
            {
                return;
            }
           
            try
            {
                App.DataS.DeleteClasse (TheClass.CLASSE_ID);
            }
            catch(Exception Ex)
            {
                ModernDialog.ShowMessage (Ex.Message, "Matrix", MessageBoxButton.OK);
            }
                                  
            if (App.DataS.GetFiliereClassCount(TheClass.FILIERE_ID) == 1){
                if(MessageBox.Show ("Vouler Vous Supprimer " + App.DataS.GetFiliereByID (TheClass.FILIERE_ID).NAME + " definitivement ?")!= MessageBoxResult.Yes) return;

                try
                {
                    App.DataS.DeleteFiliere (TheClass.FILIERE_ID);
                }
                catch (Exception Ex)
                {
                    ModernDialog.ShowMessage (Ex.Message, "Matrix", MessageBoxButton.OK);
                }                
            }       
            UpdateData ();
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            var cm = FindResource ("AddContext") as ContextMenu;
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
            var navigationService = NavigationService;
            if (navigationService != null)
                navigationService.Navigate(new HomePage());
        }

        private void ClassList_Loaded ( object sender, RoutedEventArgs e )
        {
            if(!isFirstTime) return;
            try
            {
                var E = FindVisual.FindVisualChildren<Expander> (this).First ();
                E.IsExpanded = true;
                isFirstTime = false;
            }
            catch(Exception)
            {
                // ignored
            }
        }

        private void ClassList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var list = sender as ListBox;
            if(list == null) return;
            if(list.SelectedValue == null) return;

            var navigationService = NavigationService;
            if(navigationService != null)
                navigationService.Navigate (new ClassDetails (new Guid (list.SelectedValue.ToString ())));
        }

        private void ClassList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var Classes = sender as ListBox;

            if(Classes == null) return;
            if(Classes.SelectedValue == null) return;

            CurrentSelected = new Guid (Classes.SelectedValue.ToString ());
        }

        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var Ep in FindVisual.FindVisualChildren<Expander> (this).Where (Ep => E != null && Ep.Header.ToString () != E.Header.ToString ()))
            {
                Ep.IsExpanded = false;
            }
        }
        
        #endregion

       
        private void ClassContextDel_Click ( object sender, RoutedEventArgs e )
        {
            var theName = App.DataS.GetClasseName (CurrentSelected);
            theName = "Ete Vous Sure de supprimer " + theName + " definitivement ?";

            var MD = new ModernDialog
            {
                Title = "Matrix",
                Content = theName
            };

            var r = MD.ShowDialogOKCancel ();
            if(r != MessageBoxResult.OK) return;
                
            App.DataS.DeleteClasse (CurrentSelected);
            
            //ModernDialog.ShowMessage ("Success", "Matrix", MessageBoxButton.OK);

            UpdateData();
        }
                
    }

        
}
