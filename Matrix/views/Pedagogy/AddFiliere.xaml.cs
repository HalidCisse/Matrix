using System;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using DataService.Entities.Pedagogy;
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

            NIVEAU_ENTREE.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_ENTREE ();

            NIVEAU_SORTIE.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_SORTIE ();

            foreach(var f in App.DataS.DataEnums.GetFILIERE_LEVELS ())
            {
                if(f.ToString().Equals ("1"))
                {
                    N_ANNEE.Items.Add (1 +" Annee");
                }
                else
                {
                    N_ANNEE.Items.Add (f + " Annees");
                }
            }           
           
            #endregion

            if (!string.IsNullOrEmpty(filiereToDisplayId)) {
                DisplayFiliere(App.DataS.Pedagogy.Filieres.GetFiliereById(new Guid(filiereToDisplayId)));
                _filiereDisplayedId = new Guid (filiereToDisplayId);
                TITLE_TEXT.Text = "MODIFICATION";
            }
            else
                DisplayDefault();
        }

        private void DisplayDefault()
        {
            NIVEAU_ENTREE.SelectedIndex = 0;
            NIVEAU_SORTIE.SelectedIndex = 0;
            N_ANNEE.SelectedIndex = 0;            
        }

        private void DisplayFiliere(Filiere filiereToDisplay)
        {
            if(filiereToDisplay == null) return;

            FILIERE_NAME.Text = filiereToDisplay.Name;
            NIVEAU_ENTREE.SelectedValue = filiereToDisplay.NiveauEntree;
            NIVEAU_SORTIE.SelectedValue = filiereToDisplay.Niveau;
            N_ANNEE.SelectedValue = filiereToDisplay.NAnnee.ToString();           
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myFiliere = new Filiere
            {                       
                FiliereGuid = Guid.NewGuid(),
                Name = FILIERE_NAME.Text.Trim(),
                NiveauEntree = NIVEAU_ENTREE.Text.Trim (),
                Niveau = NIVEAU_SORTIE.Text.Trim (),
                NAnnee = Convert.ToInt32 ((N_ANNEE.SelectedValue.ToString ().Substring(0,1)))
            };

            if(OpenOption == "Add")
            {
                try
                {
                    App.DataS.Pedagogy.Filieres.AddFiliere(myFiliere);
                    GenerateClasses(myFiliere.FiliereGuid);
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
                    myFiliere.FiliereGuid = _filiereDisplayedId;
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
            var upper = Convert.ToInt32((N_ANNEE.SelectedValue.ToString().Substring(0, 1)));

            for (var i = 1; i <= upper; i++)
            {
                var newClasse = new Classe {ClasseGuid = Guid.NewGuid(), FiliereGuid = filiereId, Level = i};

                if (i == 1) {
                    newClasse.Name = "1 ere Annee";
                }
                else {
                    newClasse.Name = i + " eme Annee";
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

            if(string.IsNullOrEmpty (FILIERE_NAME.Text))
            {
                FILIERE_NAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                FILIERE_NAME.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NIVEAU_ENTREE.Text))
            {
                NIVEAU_ENTREE.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NIVEAU_ENTREE.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NIVEAU_SORTIE.Text))
            {
                NIVEAU_SORTIE.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NIVEAU_SORTIE.BorderBrush = Brushes.Blue;
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
