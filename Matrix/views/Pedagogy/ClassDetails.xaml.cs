using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataService.Entities.Pedagogy;
using DataService.Enum;

namespace Matrix.views.Pedagogy
{
    //todo: ClassStudentView as UserControl move listSelector into ClassStudentView
    //todo: ClassStudentView ClassStudentCard should show grade, presence ....
    //todo: create SettingView and content CurrentAnneeScolaire

    /// <summary>
    /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe donnee
    /// </summary>
    public partial class ClassDetails
    {
        private string _currentSelected;
        private Classe _openedClass = new Classe();

        /// <summary>
        /// Affiche l'emploi du temps , les matieres et les Etudiant pour une classe 
        /// </summary>
        /// <param name="openClassId"> ID De la Classe</param>
        public ClassDetails(Guid openClassId)
        {
            InitializeComponent();

            _openedClass.ClasseId = openClassId;

            new Task(() =>
            {
                _openedClass = App.DataS.Pedagogy.Classes.GetClasseById(openClassId);
                Dispatcher.BeginInvoke(new Action(() => { ClassFiliere.Text = App.DataS.Pedagogy.Filieres.GetFiliereById(_openedClass.FiliereId).Name.ToUpper(); }));
                Dispatcher.BeginInvoke(new Action(() => { ClassName.Text = _openedClass.Name.ToUpper(); }));
            }).Start();

        }

        private void UpdateData()
        {            
            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() => { AnneeScolaireSelector.DataDictionary = App.DataS.Pedagogy.GetAllAnneeScolaires(); }));
                Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(_openedClass.ClasseId); }));
                Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(_openedClass.ClasseId); }));
                Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.Pedagogy.Classes.GetClassStudents(_openedClass.ClasseId); }));

            }).Start();

        }


        #region EVENT HANDLERS

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new PedagogyView());
        }

        private void AddButon_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("AddContext") as ContextMenu;
            if (cm == null) return;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void AddCours_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddCours(_openedClass.ClasseId) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddMatiere_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddMatiere(_openedClass.ClasseId) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddInscription_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddInscription(_openedClass.ClasseId.ToString()) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void AddAnneScolaire_Click(object sender, RoutedEventArgs e)
        {
            var wind = new AddAnneeScolaire { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //if(CurrentSelected == null)
            //{
            //    MessageBox.Show ("Selectionner Une Matiere A Supprimer !");
            //    return;
            //}

            //var theGaName = App.Db.GetMatiereName (CurrentSelected);
            //theGaName = "Ete Vous Sure de supprimer " + theGaName + " de la base de donnee ?";

            //if(MessageBox.Show (theGaName, "", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;

            //MessageBox.Show (App.Db.DeleteMatiere (CurrentSelected) ? "Supprimer Avec Succes" : "Echec");
            //UpdateMatieres ();
        }

        private void ClassWeekSchedule_OnSelectionChanged(object sender, EventArgs e)
        {
            var id = sender as string;
            _currentSelected = id;
        }

        private void MatieresList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var matieres = sender as ListBox;
            if (matieres?.SelectedValue == null) return;
            var matiereToDisplay = App.DataS.Pedagogy.Matieres.GetMatiereById(new Guid(matieres.SelectedValue.ToString()));

            var wind = new AddMatiere(_openedClass.ClasseId, matiereToDisplay) { Owner = Window.GetWindow(this) };
            wind.ShowDialog();
            UpdateData();
        }

        private void StudentsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }



        #endregion
        [STAThread]
        private void ListSelector_OnSelectionChanged(object sender, EventArgs e)
        {
            var currentAnneeScolaire = sender as string;

            App.DataS.Settings.SetSetting(MatrixConstants.SystemGuid(), Settings.CurrentAnneeScolaire, currentAnneeScolaire);

           

            //MessageBox.Show(currentAnneeScolaire);
        }

       
    }
}















//Dispatcher.BeginInvoke(new Action(() => { ClassWeekSchedule.UpdateData(OpenedClass.CLASSE_ID); }));
//Dispatcher.BeginInvoke(new Action(() => { MatieresList.ItemsSource = App.ModelS.GetClassMatieresCards(OpenedClass.CLASSE_ID); }));
//Dispatcher.BeginInvoke(new Action(() => { StudentsList.ItemsSource = App.DataS.GetClassStudents(OpenedClass.CLASSE_ID); }));