using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities.Pedagogy;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Utils;

namespace Matrix.views.Pedagogy
{
    /// <summary> 
    /// Interaction logic for AddInscription.xaml
    /// </summary>
    public partial class AddInscription
    {
        private readonly bool _isAdd;

        private Inscription _currentInscription = new Inscription();

        /// <summary>
        /// Form Pour Ajouter Inscrire Un Etudiant
        /// </summary>
        /// <param name="inscriptionToOpenGuid"></param>
        public AddInscription(string inscriptionToOpenGuid = null)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(inscriptionToOpenGuid)) { _isAdd = true; }
            
            new Task(() =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    GetPatternData();
                   
                    if (_isAdd) DisplayDefault();
                   
                    else Display(App.DataS.Pedagogy.Inscriptions.GetInscriptionById(new Guid(inscriptionToOpenGuid)));
                   
                }));
            }).Start();
                      
        }

        private void Display(Inscription inscriptionToDisp)
        {
            _currentInscription = inscriptionToDisp;

            STUDENT_.SelectedValue = _currentInscription.StudentGuid ;
            INSCRIPTION_NUM_.Text = _currentInscription.InscriptionId;

            FILIERE_.SelectedValue = App.DataS.Pedagogy.Classes.GetClasseById(_currentInscription.ClasseGuid).FiliereGuid;
            CLASSE_.SelectedValue = _currentInscription.ClasseGuid;

            DESCRIPTION_.Text = _currentInscription.Description;
            
            
        }

        private void GetPatternData()
        {           
            STUDENT_.ItemsSource = App.DataS.Pedagogy.Inscriptions.GetStudentsNotIns(App.DataS.Pedagogy.CurrentAnneeScolaireGuid);
            FILIERE_.ItemsSource = App.DataS.Pedagogy.Filieres.GetAllFilieres();
            INSCRIPTION_NUM_.Text = GenNewInsId();
        }

        private void DisplayDefault()
        {
            if (_isAdd) return;

            TITLE_TEXT.Text = "MODIFICATION";
            
        }

        private void FiliereGuid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CLASSE_.ItemsSource = App.DataS.Pedagogy.Filieres.GetFiliereClasses(new Guid(FILIERE_.SelectedValue.ToString()));
            CLASSE_.SelectedIndex = 0;
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (ChampsValidated() != true) return;
            
            _currentInscription.StudentGuid = new Guid(STUDENT_.SelectedValue.ToString());
            _currentInscription.InscriptionId = INSCRIPTION_NUM_.Text.Trim();

            _currentInscription.AnneeScolaireGuid = App.DataS.Pedagogy.CurrentAnneeScolaireGuid;  
                      
            _currentInscription.ClasseGuid = new Guid(CLASSE_.SelectedValue.ToString());
            _currentInscription.Description = DESCRIPTION_.Text;
           
            if (_isAdd)
            {
                try
                {
                    App.DataS.Pedagogy.Inscriptions.AddInscription(_currentInscription);                  
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);
                    Close();
                }
                ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
                Close();
            }
            else
            {
                try
                {                    
                    App.DataS.Pedagogy.Inscriptions.UpdateInscription(_currentInscription);                    
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);
                    Close();
                }
                ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
                Close();
            }

        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool ChampsValidated()
        {
            var ok = true;

            if (string.IsNullOrEmpty(INSCRIPTION_NUM_.Text.Trim()) || App.DataS.Pedagogy.Inscriptions.InscExist(INSCRIPTION_NUM_.Text.Trim()))
            {
                INSCRIPTION_NUM_.BorderBrush = Brushes.Red;
                ok = false;
                ModernDialog.ShowMessage("Le numéro d'inscription doit etre valide ou  inexistant !!","Matrix", MessageBoxButton.OK);
            }
            else
            {
                INSCRIPTION_NUM_.BorderBrush = Brushes.Blue;
            }
            
            //if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
        }
       
        private static string GenNewInsId()
        {
            string idOut;

            do idOut = "I" + DateTime.Today.Year + "-" + GenId.GetId(2) + "-" + GenId.GetId(3); while (App.DataS.Pedagogy.Inscriptions.InscExist(idOut));

            return idOut;
        }


    }
}
