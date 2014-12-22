using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Matrix.views.Students;

namespace Matrix.Controls
{
    /// <summary>
    /// Interaction logic for ClasseStudents.xaml
    /// </summary>
    public partial class ClasseStudents
    {

        #region FIELDS

        /// <summary>
        /// Quand on click sur une un etudiant e = ID
        /// </summary>
        public event EventHandler SelectionChanged;

        
        private Guid _classId;
        
        private Guid _currentAnneeScolaireGuid;

        private bool _isFirstTime = true ;


        #endregion

        /// <summary>
        /// Affiche les Etudiants D'Une Classe en une Annee Scolaire
        /// </summary>
        public ClasseStudents()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Mettre a jour les information de l'emploi du temps
        /// </summary>
        /// <param name="classGuid">ID de la classe</param>
        /// <param name="currentAnneeScolaireGuid"></param>
        public void Refresh(Guid? classGuid = null, Guid? currentAnneeScolaireGuid = null)
        {
            if (classGuid != null) _classId = (Guid) classGuid;
            if (currentAnneeScolaireGuid != null) _currentAnneeScolaireGuid = (Guid)currentAnneeScolaireGuid;
           
            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() => { STUDENTS_LIST.ItemsSource = App.DataS.Pedagogy.Classes.GetClassStudents(_classId, _currentAnneeScolaireGuid); }));
               
            }).RunSynchronously();            
        }

        private void ListSelector_OnOnSelectionChanged(object sender, EventArgs e)
        {
            if (_isFirstTime) { _isFirstTime = false; return; }
            if (sender == null) return;

            _currentAnneeScolaireGuid = new Guid((string) sender);
            Refresh();
        }

        private void STUDENTS_LIST_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (STUDENTS_LIST?.SelectedValue == null) return;
            var wind = new StudentInfo(STUDENTS_LIST.SelectedValue.ToString()) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            Refresh();
        }
      
        private void STUDENTS_LIST_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {                   
            var list = sender as ListBox;
            if (list?.SelectedValue == null) return;
            
            SelectionChanged?.Invoke(list.SelectedValue.ToString(), e);
        }

        private void ANNEE_SELECTOR_OnLoaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ANNEE_SELECTOR.DataDictionary = App.DataS.Pedagogy.GetAllAnneeScolaires();
                _isFirstTime = true;
                ANNEE_SELECTOR.SelectedValue = App.DataS.Pedagogy.CurrentAnneeScolaireGuid.ToString();
            }));
        }


        
    }
}
