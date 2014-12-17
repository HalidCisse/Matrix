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

            NiveauEntree.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_ENTREE ();

            NiveauSortie.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_SORTIE ();

            foreach(var f in App.DataS.DataEnums.GetFILIERE_LEVELS ())
            {
                if(f.ToString().Equals ("1"))
                {
                    NAnnee.Items.Add (1 +" Annee");
                }
                else
                {
                    NAnnee.Items.Add (f + " Annees");
                }
            }           
           
            #endregion

            if (!string.IsNullOrEmpty(filiereToDisplayId)) {
                DisplayFiliere(App.DataS.Pedagogy.Filieres.GetFiliereById(new Guid(filiereToDisplayId)));
                _filiereDisplayedId = new Guid (filiereToDisplayId);
                TitleText.Text = "MODIFICATION";
            }
            else
                DisplayDefault();
        }

        private void DisplayDefault()
        {
            NiveauEntree.SelectedIndex = 0;
            NiveauSortie.SelectedIndex = 0;
            NAnnee.SelectedIndex = 0;            
        }

        private void DisplayFiliere(Filiere filiereToDisplay)
        {
            if(filiereToDisplay == null) return;

            FiliereName.Text = filiereToDisplay.Name;
            NiveauEntree.SelectedValue = filiereToDisplay.NiveauEntree;
            NiveauSortie.SelectedValue = filiereToDisplay.Niveau;
            NAnnee.SelectedValue = filiereToDisplay.NAnnee.ToString();           
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myFiliere = new Filiere
            {                       
                FiliereGuid = Guid.NewGuid(),
                Name = FiliereName.Text.Trim(),
                NiveauEntree = NiveauEntree.Text.Trim (),
                Niveau = NiveauSortie.Text.Trim (),
                NAnnee = Convert.ToInt32 ((NAnnee.SelectedValue.ToString ().Substring(0,1)))
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
            var upper = Convert.ToInt32((NAnnee.SelectedValue.ToString().Substring(0, 1)));

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

            if(string.IsNullOrEmpty (FiliereName.Text))
            {
                FiliereName.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                FiliereName.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NiveauEntree.Text))
            {
                NiveauEntree.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NiveauEntree.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NiveauSortie.Text))
            {
                NiveauSortie.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                NiveauSortie.BorderBrush = Brushes.Blue;
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
