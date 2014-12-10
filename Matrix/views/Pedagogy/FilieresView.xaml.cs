using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataService.ViewModel;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Extention;
using Matrix.Utils;

namespace Matrix.views.Pedagogy
{
    
    public partial class FilieresView
    {
        
        private string _currentSelected;
        private bool _isFirstTime = true;
        private readonly BackgroundWorker _worker = new BackgroundWorker ();
        private List<FiliereClassCard> _filieresBuff = new List<FiliereClassCard> ();

        /// <summary>
        /// Affiche les filiere et leurs classes
        /// </summary>
        public FilieresView ( )
        {
            InitializeComponent ();
        }



        #region BackgroundWorker
        private void UpdateData ( )
        {
            if(_worker.IsBusy) return;            
            _worker.RunWorkerAsync ();
        }

        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {            
            _filieresBuff = App.ModelS.GetFiliereClassCards ();
        }

        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            FiliereList.ItemsSource = _filieresBuff;
            _worker.Dispose ();
        }

        #endregion




        #region Eventhandler

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateData ();
        }

        private void DeleteButton_Click ( object sender, RoutedEventArgs e )
        {
            
            MessageBox.Show (_currentSelected + "");

            var theClass = App.DataS.Pedagogy.Classes.GetClasseById (new Guid (_currentSelected));

            var theName = App.DataS.Pedagogy.Classes.GetClasseName (new Guid (_currentSelected));
            theName = "Ete Vous Sure de supprimer " + theClass.Name + " definitivement ?";

            var md = new ModernDialog {
                Title = "Matrix",
                Content = theName
            };
                      
            var r = md.ShowDialogOkCancel ();
            if (r != MessageBoxResult.OK)
            {
                return;
            }
           
            try
            {
                App.DataS.Pedagogy.Classes.DeleteClasse (theClass.ClasseId);
            }
            catch(Exception ex)
            {
                ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
            }
                                  
            if (App.DataS.Pedagogy.Filieres.GetFiliereClassCount(theClass.FiliereId) == 1){
                if(MessageBox.Show ("Vouler Vous Supprimer " + App.DataS.Pedagogy.Filieres.GetFiliereById (theClass.FiliereId).Name + " definitivement ?")!= MessageBoxResult.Yes) return;

                try
                {
                    App.DataS.Pedagogy.Filieres.DeleteFiliere (theClass.FiliereId);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);
                }                
            }       
            UpdateData ();
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
            //if(!isFirstTime) return;
            //try
            //{
            //    var E = FindVisual.FindVisualChildren<Expander> (this).First ();
            //    E.IsExpanded = true;
            //    isFirstTime = false;
            //}
            //catch(Exception)
            //{
            //    // ignored
            //}
        }

        private void ClassList_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            var list = sender as ListBox;
            if(list?.SelectedValue == null) return;

            var navigationService = NavigationService;
            navigationService?.Navigate (new ClassDetails (new Guid (list.SelectedValue.ToString ())));
        }

        private void ClassList_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var classes = sender as ListBox;

            if(classes?.SelectedValue == null) return;

            _currentSelected = classes.SelectedValue.ToString();
        }

        private void Expander_Expanded ( object sender, RoutedEventArgs e )
        {
            var E = sender as Expander;

            foreach(var ep in FindVisual.FindVisualChildren<Expander> (this).Where (ep => E != null && ep.Header.ToString () != E.Header.ToString ()))
            {
                ep.IsExpanded = false;
            }
        }

        private void ClassContextDel_Click ( object sender, RoutedEventArgs e )
        {
            var theName = App.DataS.Pedagogy.Classes.GetClasseName (new Guid (_currentSelected));
            theName = "Ete Vous Sure de supprimer " + theName + " definitivement ?";

            var md = new ModernDialog
            {
                Title = "Matrix",
                Content = theName
            };

            var r = md.ShowDialogOkCancel ();
            if(r != MessageBoxResult.OK) return;

            App.DataS.Pedagogy.Classes.DeleteClasse (new Guid (_currentSelected));

            //ModernDialog.ShowMessage ("Success", "Matrix", MessageBoxButton.OK);

            UpdateData ();
        }

        private void ClassConTextMod_Click ( object sender, RoutedEventArgs e )
        {
            var wind = new AddClass(App.DataS.Pedagogy.Classes.GetClasseById(new Guid (_currentSelected))) { Owner = Window.GetWindow (this) };
            wind.ShowDialog ();
            UpdateData ();
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
               
            }
            // free native resources
        }

        private void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion




    }

    
}

