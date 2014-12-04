using System;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    public partial class AddMatiere
    {
        private readonly bool IsAdd;               
        private readonly Matiere MatiereDisplayed = new Matiere();

        /// <summary>
        /// Form pour Ajouter/Modifier une Matiere
        /// </summary>
        /// <param name="CurrentClassID">ID</param>
        /// <param name="MatiereToDisplay">Object</param>
        public AddMatiere ( Guid CurrentClassID, Matiere MatiereToDisplay = null )
        {
            InitializeComponent ();
            
            MatiereDisplayed.CLASSE_ID = CurrentClassID;

            if(MatiereToDisplay == null)
            {
                IsAdd = true;
                MatiereDisplayed.MATIERE_ID = Guid.NewGuid ();               
            }
            else
            { 
                IsAdd = false;               
                MatiereDisplayed = MatiereToDisplay;
                DisplayMatiere (MatiereDisplayed);                                
            }                                     
        }
        
        private void DisplayMatiere(Matiere MatiereToDisplay)
        {
            if(MatiereToDisplay == null) return;

            TitleText.Text = "MODIFICATION";
            MATIERE_NAME_.Text = MatiereToDisplay.NAME;
            SIGLE_.Text = MatiereToDisplay.SIGLE;
            COEFFICIENT_.Value = MatiereToDisplay.COEFFICIENT;
            // ReSharper disable once PossibleNullReferenceException
            COULEUR_.SelectedColor = (Color)ColorConverter.ConvertFromString (MatiereToDisplay.COULEUR);
            DESCRIPTION_.Text =MatiereToDisplay.DESCRIPTION;                         
        }
       
        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

            if(ChampsValidated () != true) return;
            
            MatiereDisplayed.NAME = MATIERE_NAME_.Text.Trim ();            
            MatiereDisplayed.SIGLE = SIGLE_.Text;
            MatiereDisplayed.COEFFICIENT = COEFFICIENT_.Value.GetValueOrDefault();
            MatiereDisplayed.COULEUR = COULEUR_.SelectedColorText;
            MatiereDisplayed.DESCRIPTION = DESCRIPTION_.Text;          

            if(IsAdd)
            {
                try
                {
                    App.DataS.Pedagogy.Matieres.AddMatiere(MatiereDisplayed);
                    ModernDialog.ShowMessage ("Success", "Matrix", MessageBoxButton.OK);                
                }
                catch (Exception ex)
                {                    
                    ModernDialog.ShowMessage (ex.Message, "Matrix", MessageBoxButton.OK);                   
                }
                Close ();
            }
            else
            {
                try
                {
                    App.DataS.Pedagogy.Matieres.UpdateMatiere(MatiereDisplayed);                    
                    ModernDialog.ShowMessage("Success","Matrix", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);                    
                }                                                                         
                Close ();
            }            
        }

        private bool ChampsValidated()
        {
            var Ok = true;

            if(string.IsNullOrEmpty (MATIERE_NAME_.Text))
            {
                MATIERE_NAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                MATIERE_NAME_.BorderBrush = Brushes.Blue;
            }
                       
            if(!Ok) ModernDialog.ShowMessage ("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return Ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

                       
    }
}


//private void UpdateMatiereInstructors()
//        {
//            Parallel.ForEach(StaffBuff, S =>
//            {
//                if (S.IsINSTRUCTOR)
//                {
//                    App.DataS.AddMatiereInstructor(MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
//                }
//                else
//                {
//                    App.DataS.DeleteMatiereInstructor(MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
//                }
//            });
//        }

//#region Background Works
       
//        private void UpdateStaffs ( )
//        {
//            if(worker.IsBusy) return;            
//            worker.RunWorkerAsync ();
//        }

//        private void worker_DoWork ( object sender, DoWorkEventArgs e )
//        {
//            StaffBuff = GetStaffModelList (MatiereDisplayed.MATIERE_ID);
//            if (IsAdd) return;
//            Parallel.ForEach(StaffBuff, S =>
//            {
//                S.IsINSTRUCTOR = App.DataS.IsMatiereInstructor(S.STAFF_ID, MatiereDisplayed.MATIERE_ID);
//            });
//        }

//        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
//        {
//            BusyIndicator.IsBusy = false;
//            InstructeursList.Items.Clear();
//            InstructeursList.ItemsSource = StaffBuff;            
//            worker.Dispose ();
//        }

//        private static List<MatiereStaffsModel> GetStaffModelList (Guid MatiereID)
//        {
//            var ML = new List<MatiereStaffsModel>();
//            var Staffs = App.DataS.GetAllStaffs ();

//            Parallel.ForEach(Staffs, S =>
//            {
//                var M = new MatiereStaffsModel
//                {
//                    STAFF_ID = S.STAFF_ID,
//                    FULL_NAME = S.FULL_NAME,
//                    PHOTO_IDENTITY = S.PHOTO_IDENTITY,
//                    QUALIFICATION = S.QUALIFICATION,
//                    IsINSTRUCTOR = App.DataS.IsMatiereInstructor(S.STAFF_ID, MatiereID)
//                };
//                ML.Add(M);
//            });
//            return ML;
//        }


//        #endregion