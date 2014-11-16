using System;
using System.Windows;
using DataService.Entities;

namespace Matrix.views.Pedagogy
{
    

    public partial class AddCours
    {

        private bool IsAdd;
        private Cours OpenedCours;
        private Guid CurrentClassID;

        public AddCours (Guid CurrentClassID, Cours CoursToOpen = null )
        {
            InitializeComponent ();

            #region Patterns Data

            //NIVEAU_ENTREE_.ItemsSource = App.DataS.GetFILIERE_NIVEAU_ENTREE ();

            //NIVEAU_SORTIE_.ItemsSource = App.DataS.GetFILIERE_NIVEAU_SORTIE ();

            //foreach(var F in App.DataS.GetFILIERE_LEVELS ())
            //{
            //    if(F.ToString ().Equals ("1"))
            //    {
            //        N_ANNEE_.Items.Add (1 +" Annee");
            //    }
            //    else
            //    {
            //        N_ANNEE_.Items.Add (F + " Annees");
            //    }
            //}

            #endregion

            this.CurrentClassID = CurrentClassID;
            if(CoursToOpen != null)
            {
                DisplayClasse (CoursToOpen);
                OpenedCours = CoursToOpen;
                TitleText.Text = "MODIFICATION";
            }
            else
                DisplayDefault ();
        }

        private void DisplayClasse(Cours classToDisplay)
        {           
            //FILIERE_NAME_.Text = FiliereToDisplay.NAME;
            //NIVEAU_ENTREE_.SelectedValue = FiliereToDisplay.NIVEAU_ENTREE;
            //NIVEAU_SORTIE_.SelectedValue = FiliereToDisplay.NIVEAU;
            //N_ANNEE_.SelectedValue = FiliereToDisplay.N_ANNEE.ToString ();
        }

        private void DisplayDefault()
        {
            
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {

        }
    }

}
