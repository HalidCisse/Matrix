using System;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class AddFiliere
    {
        /// <summary>
        /// 
        /// </summary>
        public string OpenOption;
        private readonly Guid _filiereDisplayedId;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filiereToDisplayId"></param>
        public AddFiliere ( string filiereToDisplayId = null )
        {
            InitializeComponent ();

            #region Patterns Data

            NIVEAU_ENTREE_.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_ENTREE ();

            NIVEAU_SORTIE_.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_SORTIE ();

            foreach(var f in App.DataS.DataEnums.GetFILIERE_LEVELS ())
            {
                if(f.ToString().Equals ("1"))
                {
                    N_ANNEE_.Items.Add (1 +" Annee");
                }
                else
                {
                    N_ANNEE_.Items.Add (f + " Annees");
                }
            }           
           
            #endregion

            if (!string.IsNullOrEmpty(filiereToDisplayId)) {
                DisplayFiliere(App.DataS.Pedagogy.Filieres.GetFiliereByID(new Guid(filiereToDisplayId)));
                _filiereDisplayedId = new Guid (filiereToDisplayId);
                TitleText.Text = "MODIFICATION";
            }
            else
                DisplayDefault();
        }

        private void DisplayDefault()
        {
            NIVEAU_ENTREE_.SelectedIndex = 0;
            NIVEAU_SORTIE_.SelectedIndex = 0;
            N_ANNEE_.SelectedIndex = 0;            
        }

        private void DisplayFiliere(Filiere filiereToDisplay)
        {
            if(filiereToDisplay == null) return;

            FILIERE_NAME_.Text = filiereToDisplay.NAME;
            NIVEAU_ENTREE_.SelectedValue = filiereToDisplay.NIVEAU_ENTREE;
            NIVEAU_SORTIE_.SelectedValue = filiereToDisplay.NIVEAU;
            N_ANNEE_.SelectedValue = filiereToDisplay.N_ANNEE.ToString();           
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myFiliere = new Filiere
            {                       
                FILIERE_ID = Guid.NewGuid(),
                NAME = FILIERE_NAME_.Text.Trim(),
                NIVEAU_ENTREE = NIVEAU_ENTREE_.Text.Trim (),
                NIVEAU = NIVEAU_SORTIE_.Text.Trim (),
                N_ANNEE = Convert.ToInt32 ((N_ANNEE_.SelectedValue.ToString ().Substring(0,1)))
            };

            if(OpenOption == "Add")
            {
                try
                {
                    App.DataS.Pedagogy.Filieres.AddFiliere(myFiliere);
                    GenerateClasses(myFiliere.FILIERE_ID);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "ERREUR !!", MessageBoxButton.OK);
                    return;                                      
                }
                
            }
            else
            {
                try
                {
                    myFiliere.FILIERE_ID = _filiereDisplayedId;
                    App.DataS.Pedagogy.Filieres.UpdateFiliere(myFiliere);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "ERREUR !!", MessageBoxButton.OK);
                    return;
                }
                ModernDialog.ShowMessage("Ajouter Avec Success", "Matrix", MessageBoxButton.OK);                               
            }

            Close ();
        }

        private void GenerateClasses(Guid filiereId)
        {
            var upper = Convert.ToInt32((N_ANNEE_.SelectedValue.ToString().Substring(0, 1)));

            for (var i = 1; i <= upper; i++)
            {
                var newClasse = new Classe {CLASSE_ID = Guid.NewGuid(), FILIERE_ID = filiereId, LEVEL = i};

                if (i == 1) {
                    newClasse.NAME = "1 ere Annee";
                }
                else {
                    newClasse.NAME = i + " eme Annee";
                }

                try
                {
                    App.DataS.Pedagogy.Classes.AddClasse(newClasse);
                }
                catch (Exception e)
                {
                    ModernDialog.ShowMessage(e.Message, "ERREUR !!", MessageBoxButton.OK);
                }
            }
        }
        
        private bool ChampsValidated()
        {
            var ok = true;

            if(string.IsNullOrEmpty (FILIERE_NAME_.Text))
            {
                FILIERE_NAME_.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                FILIERE_NAME_.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NIVEAU_ENTREE_.Text))
            {
                NIVEAU_ENTREE_.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NIVEAU_ENTREE_.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NIVEAU_SORTIE_.Text))
            {
                NIVEAU_SORTIE_.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NIVEAU_SORTIE_.BorderBrush = Brushes.Blue;
            }


            if(!ok) MessageBox.Show ("Verifier Les Informations !");


            return ok;

        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close ();
        }
    }
}
