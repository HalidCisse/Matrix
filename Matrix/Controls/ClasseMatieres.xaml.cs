using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Matrix.views.Pedagogy;

namespace Matrix.Controls
{
    /// <summary>
    /// Interaction logic for ClasseMatieres.xaml
    /// </summary>
    public partial class ClasseMatieres
    {

        #region FIELDS

        /// <summary>
        /// Quand on click sur une Matiere sender = selected MatiereGuid
        /// </summary>
        public event EventHandler SelectionChanged;

        private Guid _classId;

        private DateTime _currentDate = DateTime.Today;

        private bool _isAll;

        #endregion

        /// <summary>
        /// Affiche les Matieres Enseigner Dans Une Classe
        /// </summary>
        public ClasseMatieres()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="classGuid"></param>
        /// <param name="currentDate"></param>
        public void Refresh(Guid? classGuid = null, DateTime? currentDate = null)
        {
            if (classGuid != null) _classId = (Guid)classGuid;
            if (currentDate != null) _currentDate = (DateTime)currentDate;
           
            new Task(() =>
            {                                
                if (_isAll) Dispatcher.BeginInvoke(new Action(() => { MATIERES_LIST.ItemsSource = App.ModelS.GetClassMatieresCards(_classId); }));
               
                else Dispatcher.BeginInvoke(new Action(() => { MATIERES_LIST.ItemsSource = App.ModelS.GetClassMatieresCards(_classId, _currentDate); }));
               
            }).RunSynchronously();
        }

        private void MATIERES_LIST_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var matieres = sender as ListBox;
            if (matieres?.SelectedValue == null) return;
            var matiereToDisplay = App.DataS.Pedagogy.Matieres.GetMatiereById(new Guid(matieres.SelectedValue.ToString()));

            var wind = new AddMatiere(_classId, matiereToDisplay) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            Refresh();
        }

        private void MATIERES_LIST_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;

            SelectionChanged?.Invoke(list.SelectedValue.ToString(), e);
        }
    
        private void CHECK_BOX_OnClick(object sender, RoutedEventArgs e)
        {
            _isAll = CHECK_BOX.IsChecked == true;
            Refresh();
        }




    }
}










//MATIERES_LIST.ItemsSource = App.ModelS.GetClassMatieresCards(_classId, _currentDate);

//Dispatcher.BeginInvoke(new Action(() => { MATIERES_LIST.ItemsSource = App.ModelS.GetClassMatieresCards(_classId, _currentDate); }));
