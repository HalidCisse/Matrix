using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;

namespace Matrix.views.Pedagogy
{
    
    public partial class AddFiliere
    {
        public string OpenOption;
        private Guid FiliereDisplayedID;
        
        public AddFiliere ( string FiliereToDisplayID = null )
        {
            InitializeComponent ();

            #region Patterns Data

            NIVEAU_ENTREE_.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_ENTREE ();

            NIVEAU_SORTIE_.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAU_SORTIE ();

            foreach(var F in App.DataS.DataEnums.GetFILIERE_LEVELS ())
            {
                if(F.ToString().Equals ("1"))
                {
                    N_ANNEE_.Items.Add (1 +" Annee");
                }
                else
                {
                    N_ANNEE_.Items.Add (F + " Annees");
                }
            }           
           
            #endregion

            if (!string.IsNullOrEmpty(FiliereToDisplayID)) {
                DisplayFiliere(App.DataS.Pedagogy.Filieres.GetFiliereByID(new Guid(FiliereToDisplayID)));
                FiliereDisplayedID = new Guid (FiliereToDisplayID);
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

        private void DisplayFiliere(Filiere FiliereToDisplay)
        {
            if(FiliereToDisplay == null) return;

            FILIERE_NAME_.Text = FiliereToDisplay.NAME;
            NIVEAU_ENTREE_.SelectedValue = FiliereToDisplay.NIVEAU_ENTREE;
            NIVEAU_SORTIE_.SelectedValue = FiliereToDisplay.NIVEAU;
            N_ANNEE_.SelectedValue = FiliereToDisplay.N_ANNEE.ToString();           
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var Myfiliere = new Filiere
            {                       
                NAME = FILIERE_NAME_.Text.Trim(),
                NIVEAU_ENTREE = NIVEAU_ENTREE_.Text.Trim (),
                NIVEAU = NIVEAU_SORTIE_.Text.Trim (),
                N_ANNEE = Convert.ToInt32 ((N_ANNEE_.SelectedValue.ToString ().Substring(0,1)))
            };

            if(OpenOption == "Add")
            {                
                MessageBox.Show (App.DataS.Pedagogy.Filieres.AddFiliere (Myfiliere) ? "Add Success" : "Add Failed");               
            }
            else
            {
                Myfiliere.FILIERE_ID = FiliereDisplayedID;
                MessageBox.Show (App.DataS.Pedagogy.Filieres.UpdateFiliere (Myfiliere) ? "Update Success" : "Update Failed");               
            }
            Close ();
        }
        
        private bool ChampsValidated()
        {
            var Ok = true;

            if(string.IsNullOrEmpty (FILIERE_NAME_.Text))
            {
                FILIERE_NAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                FILIERE_NAME_.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NIVEAU_ENTREE_.Text))
            {
                NIVEAU_ENTREE_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                NIVEAU_ENTREE_.BorderBrush = Brushes.Blue;
            }

            if(string.IsNullOrEmpty (NIVEAU_SORTIE_.Text))
            {
                NIVEAU_SORTIE_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                NIVEAU_SORTIE_.BorderBrush = Brushes.Blue;
            }


            if(!Ok) MessageBox.Show ("Verifier Les Informations !");


            return Ok;

        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close ();
        }
    }
}
