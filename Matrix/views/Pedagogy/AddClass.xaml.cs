using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DataService.Entities;
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
        //private readonly string FiliereDisplayed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classToDisplay"></param>
        public AddClass (Classe classToDisplay = null )
        {
            InitializeComponent ();
            
            FiliereName.ItemsSource = App.DataS.Pedagogy.Filieres.GetAllFilieresNames ();

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
            FiliereName.SelectedIndex = 0;                     
            Niveau.SelectedIndex = 0;
        }

        private void DisplayClass()
        {
            TitleText.Text = "MODIFICATION";
            FiliereName.SelectedValue = _classDisplayed.ClasseId;
            Niveau.SelectedValue = _classDisplayed.Level;
            ClassName.Text = _classDisplayed.Name;
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {
            if(ChampsValidated () != true) return;

            var myClass = new Classe
            {
                Name = ClassName.Text.Trim (),
                FiliereId = App.DataS.Pedagogy.Filieres.GetFiliereByName(FiliereName.SelectedValue.ToString()).FiliereId,
                Level = Convert.ToInt32 (Niveau.SelectedValue.ToString())              
            };
            
            if(_openOption == "Add")
            {
                try
                {
                    myClass.ClasseId = Guid.NewGuid();
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
                    myClass.ClasseId = _classDisplayed.ClasseId;
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

            if(FiliereName.SelectedValue == null)
            {
                FiliereName.BorderBrush = Brushes.Red;
                ok = false;
            }
            

            if(Niveau.SelectedValue == null)
            {
                Niveau.BorderBrush = Brushes.Red;
                ok = false;
            }


            if(string.IsNullOrEmpty (ClassName.Text))
            {
                ClassName.BorderBrush = Brushes.Red;
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
            if ( FiliereName.SelectedItem == null) return;

            Niveau.ItemsSource = App.DataS.DataEnums.GetFILIERE_NIVEAUX (App.DataS.Pedagogy.Filieres.GetFiliereByName (FiliereName.SelectedItem.ToString ()).FiliereId);
            Niveau.SelectedIndex = 0;
        }





    }
}
