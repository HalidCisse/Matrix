using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using DataService.ViewModel;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views
{
    

    public partial class FilieresView
    {

        private readonly BackgroundWorker worker = new BackgroundWorker ();
        private string CurrentSelected;
        private bool isFirstTime = true;

        private List<FiliereClassCard> FilieresBuff = new List<FiliereClassCard> ();

        public FilieresView ( )
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
            //FilieresBuff = App.ModelS.GetAllFilieresCards ();  
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
            var list = sender as ListBox;
            if(list == null) return;            
            if(list.SelectedValue == null){
                MessageBox.Show ("Selectionner Une Classe A Supprimer !");
                return;
            }

            var TheClass = App.DataS.GetClasseByID (new Guid (list.SelectedValue.ToString ()));



            var theName = App.DataS.GetClasseName (list.SelectedValue.ToString ());
            theName = "Ete Vous Sure de supprimer " + TheClass.NAME + " definitivement ?";


            var v = new ModernDialog
            {
                Title = "Matrix",
                Content = theName, BackgroundContent = MessageBoxButton.YesNo, MinWidth = 200
            };
            //v.OkButton.Click += OkButton_Click;
            //v.Buttons = new[] { v.OkButton, v.CancelButton };
           
            var r = v.ShowDialogOKCancel ();
            if(r==MessageBoxResult.OK)
            {
                MessageBox.Show ("ok was clicked");
            }
            else
            {
                MessageBox.Show ("cancel was clicked");
            }


            //if(new ModernDialog.ShowDialogOKCancel(theName, "Matrix", MessageBoxButton.YesNo) != MessageBoxResult.OK) return;

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

        private void OkButton_Click ( object sender, RoutedEventArgs e )
        {
            
        }

        private void AddButon_Click ( object sender, RoutedEventArgs e )
        {
            ContextMenu cm = FindResource ("AddContext") as ContextMenu;
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
                var E = FindVisualChildren<Expander> (this).First ();
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

            CurrentSelected = Classes.SelectedValue.ToString ();
        }

        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var Ep in FindVisualChildren<Expander> (this).Where (Ep => E != null && Ep.Header.ToString () != E.Header.ToString ()))
            {
                Ep.IsExpanded = false;
            }
        }


        

        public static IEnumerable<T> FindVisualChildren<T> ( DependencyObject depObj ) where T : DependencyObject
        {
            if(depObj == null) yield break;
            for(var i = 0; i < VisualTreeHelper.GetChildrenCount (depObj); i++)
            {
                var child = VisualTreeHelper.GetChild (depObj, i);
                if(child is T)
                {
                    yield return (T)child;
                }

                foreach(var childOfChild in FindVisualChildren<T> (child))
                {
                    yield return childOfChild;
                }
            }
        }
        #endregion


        
        
    }

    static class ModernDialogExtension
    {
        static MessageBoxResult result;

        public static MessageBoxResult ShowDialogOKCancel ( this ModernDialog modernDialog )
        {
            result = MessageBoxResult.Cancel;

            modernDialog.OkButton.Click += OkButton_Click;
            modernDialog.Buttons = new[] { modernDialog.OkButton, modernDialog.CloseButton };

            modernDialog.ShowDialog ();

            return result;
        }

        private static void OkButton_Click ( object sender, RoutedEventArgs e )
        {
            result = MessageBoxResult.OK;
        }
    }
}

