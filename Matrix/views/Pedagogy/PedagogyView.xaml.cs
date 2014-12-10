using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.ViewModel;
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
        //private bool isFirstTime = true;
        private readonly BackgroundWorker _worker = new BackgroundWorker ();
        private List<FiliereClassCard> _filieresBuff = new List<FiliereClassCard> ();

        /// <summary>
        /// Affiche les filieres et leurs classes
        /// </summary>
        public PedagogyView ( )
        {
            InitializeComponent ();
        }
        
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


        #region Eventhandlers

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

            var theClass = App.DataS.Pedagogy.Classes.GetClasseById (_currentSelected);

            var theName = App.DataS.Pedagogy.Classes.GetClasseName (_currentSelected);
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
            navigationService?.Navigate(new HomePage());
        }

        private void ClassList_Loaded ( object sender, RoutedEventArgs e )
        {
            //var style = FindResource ("LabelTemplate") as Style;           
            //TheScrollBar. = style;

            //var styl = new Style (typeof (ScrollViewer), FindResource ("ScrollThumbs") as Style);
            //styl.TargetType = typeof (ScrollViewer);




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

            _currentSelected = new Guid (classes.SelectedValue.ToString ());
        }

        private void ClassContextDel_Click(object sender, RoutedEventArgs e)
        {
            var theName = App.DataS.Pedagogy.Classes.GetClasseName(_currentSelected);
            theName = "Ete Vous Sure de supprimer " + theName + " definitivement ?";

            var md = new ModernDialog
            {
                Title = "Matrix",
                Content = theName
            };

            var r = md.ShowDialogOkCancel();
            if (r != MessageBoxResult.OK) return;

            App.DataS.Pedagogy.Classes.DeleteClasse(_currentSelected);

            ModernDialog.ShowMessage("Supprimer Avec Success", "Matrix", MessageBoxButton.OK);

            UpdateData();
        }

        #endregion




    }

        
}
