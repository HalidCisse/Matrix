using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class AddClass 
    {
        private readonly string _openOption;
        private readonly Classe _classDisplayed;
        //private readonly string FiliereDisplayed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classToDisplay"></param>
        public AddClass (Classe classToDisplay = null )
        {
            InitializeComponent ();

            //FiliereDisplayed = CurrentFiliere;
            FILIERE_NAME_.ItemsSource = App.DataS.Pedagogy.Filieres.GetAllFilieresNames ();

            if(classToDisplay != null)
            {
                _openOption = "Mod";
                _classDisplayed = classToDisplay;
                DisplayClass ();                               
            }
            else
            {
                _openOption = "Add";
                DisplayDefault();
            }                
        }

        private void DisplayDefault()
        {
            FILIERE_NAME_.SelectedIndex = 0;                     
            NIVEAU_.SelectedIndex = 0;
        }

        private void DisplayClass()
        {
            TitleText.Text = "MODIFICATION";
            FILIERE_NAME_.SelectedValue = _classDisplayed.NAME;
            NIVEAU_.SelectedValue = _classDisplayed.LEVEL;
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myClass = new Classe
            {
                NAME = CLASS_NAME_.Text.Trim (),
                FILIERE_ID = App.DataS.Pedagogy.Filieres.GetFiliereByName(FILIERE_NAME_.SelectedValue.ToString()).FILIERE_ID,
                LEVEL = Convert.ToInt32 (NIVEAU_.SelectedValue.ToString())              
            };
            
            if(_openOption == "Add")
            {
                try
                {
                    myClass.CLASSE_ID = Guid.NewGuid();
                    App.DataS.Pedagogy.Classes.AddClasse(myClass);
                    ModernDialog.ShowMessage("Add Success","Matrix",MessageBoxButton.OK);                    
                }
                catch (Exception ex)
                {                    
                    ModernDialog.ShowMessage(ex.Message,"Matrix",MessageBoxButton.OK);                   
                }                
            }
            else
            {
                try
                {
                    myClass.CLASSE_ID = _classDisplayed.CLASSE_ID;
                    App.DataS.Pedagogy.Classes.UpdateClasse(myClass);
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

            NIVEAU_.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAUX (App.DataS.Pedagogy.Filieres.GetFiliereByName (FILIERE_NAME_.SelectedItem.ToString ()).FILIERE_ID);
            NIVEAU_.SelectedIndex = 0;
        }





    }
}
