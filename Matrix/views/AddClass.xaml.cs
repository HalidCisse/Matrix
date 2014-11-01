using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views
{
    
    public partial class AddClass 
    {
        private readonly string OpenOption;
        private readonly Classe ClassDisplayed;
        //private readonly string FiliereDisplayed;

        public AddClass (string CurrentFiliere = null, Classe ClassToDisplay = null)
        {
            InitializeComponent ();

            //FiliereDisplayed = CurrentFiliere;
            FILIERE_NAME_.ItemsSource = App.DataS.GetAllFilieresNames ();

            if(ClassToDisplay != null)
            {
                OpenOption = "Mod";
                ClassDisplayed = ClassToDisplay;
                DisplayClass ();                               
            }
            else
            {
                OpenOption = "Add";
                DisplayDefault();
            }                
        }

        private void DisplayDefault()
        {
            FILIERE_NAME_.SelectedIndex = 0;                     //.SelectedValue = App.DataS.GetFiliereByID(FiliereDisplayed).NAME;
            NIVEAU_.SelectedIndex = 0;
        }

        private void DisplayClass()
        {
            TitleText.Text = "MODIFICATION";
            FILIERE_NAME_.SelectedValue = ClassDisplayed.NAME;
            NIVEAU_.SelectedValue = ClassDisplayed.LEVEL;
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var MyClass = new Classe
            {
                NAME = CLASS_NAME_.Text.Trim (),
                FILIERE_ID = App.DataS.GetFiliereByName(FILIERE_NAME_.SelectedValue.ToString()).FILIERE_ID,
                LEVEL = Convert.ToInt32 (NIVEAU_.SelectedValue.ToString())              
            };
            
            if(OpenOption == "Add")
            {
                try
                {
                    MyClass.CLASSE_ID = Guid.NewGuid();
                    App.DataS.AddClasse(MyClass);
                    ModernDialog.ShowMessage("Add Success","Matrix",MessageBoxButton.OK);                    
                }
                catch (Exception Ex)
                {                    
                    ModernDialog.ShowMessage(Ex.Message,"Matrix",MessageBoxButton.OK);                   
                }                
            }
            else
            {
                try
                {
                    MyClass.CLASSE_ID = ClassDisplayed.CLASSE_ID;
                    App.DataS.UpdateClasse(MyClass);
                    ModernDialog.ShowMessage("Add Success","Matrix",MessageBoxButton.OK);
                }
                catch (Exception Ex)
                {                    
                    ModernDialog.ShowMessage(Ex.Message,"Matrix",MessageBoxButton.OK);  
                }                
            }
            Close ();
        }

        private bool ChampsValidated()
        {
            var Ok = true;

            if(FILIERE_NAME_.SelectedValue == null)
            {
                FILIERE_NAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            

            if(NIVEAU_.SelectedValue == null)
            {
                NIVEAU_.BorderBrush = Brushes.Red;
                Ok = false;
            }


            if(string.IsNullOrEmpty (CLASS_NAME_.Text))
            {
                CLASS_NAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            
            if(!Ok) ModernDialog.ShowMessage ("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return Ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void FILIERE__SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if ( FILIERE_NAME_.SelectedItem == null) return;

            NIVEAU_.ItemsSource = App.DataS.GetFILIERE_NIVEAUX (App.DataS.GetFiliereByName (FILIERE_NAME_.SelectedItem.ToString ()).FILIERE_ID);
            NIVEAU_.SelectedIndex = 0;
        }





    }
}
