using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities.Pedagogy;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    
    /// <summary>
    /// Form Pour Ajouter Une Classe
    /// </summary>
    public partial class AddClass 
    {
        private readonly string _openOption;
        private readonly Classe _classDisplayed;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classToDisplay"></param>
        public AddClass (Classe classToDisplay = null )
        {
            InitializeComponent ();
            
            FILIERE_NAME.ItemsSource = App.DataS.Pedagogy.Filieres.GetAllFilieresNames ();

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
            FILIERE_NAME.SelectedIndex = 0;                     
            NIVEAU.SelectedIndex = 0;
        }

        private void DisplayClass()
        {
            TITLE_TEXT.Text = "MODIFICATION";
            FILIERE_NAME.SelectedValue = _classDisplayed.ClasseGuid;
            NIVEAU.SelectedValue = _classDisplayed.Level;
            CLASS_NAME.Text = _classDisplayed.Name;
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myClass = new Classe
            {
                Name = CLASS_NAME.Text.Trim (),
                FiliereGuid = App.DataS.Pedagogy.Filieres.GetFiliereByName(FILIERE_NAME.SelectedValue.ToString()).FiliereGuid,
                Level = Convert.ToInt32 (NIVEAU.SelectedValue.ToString())              
            };
            
            if(_openOption == "Add")
            {
                try
                {
                    myClass.ClasseGuid = Guid.NewGuid();
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
                    myClass.ClasseGuid = _classDisplayed.ClasseGuid;
                    App.DataS.Pedagogy.Classes.UpdateClasse(myClass);
                    ModernDialog.ShowMessage("Add Success","Matrix",MessageBoxButton.OK);
                }
                catch (Exception ex)
                {                    
                    ModernDialog.ShowMessage(ex.Message,"Matrix",MessageBoxButton.OK);  
                }                
            }
            Close ();
        }

        private bool ChampsValidated()
        {
            var ok = true;

            if(FILIERE_NAME.SelectedValue == null)
            {
                FILIERE_NAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            

            if(NIVEAU.SelectedValue == null)
            {
                NIVEAU.BorderBrush = Brushes.Red;
                ok = false;
            }


            if(string.IsNullOrEmpty (CLASS_NAME.Text))
            {
                CLASS_NAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            
            if(!ok) ModernDialog.ShowMessage ("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void FILIERE__SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if ( FILIERE_NAME.SelectedItem == null) return;

            NIVEAU.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAUX (App.DataS.Pedagogy.Filieres.GetFiliereByName (FILIERE_NAME.SelectedItem.ToString ()).FiliereGuid);
            NIVEAU.SelectedIndex = 0;
        }





    }
}
