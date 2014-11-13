using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Model;

namespace Matrix.views
{
    
    public partial class AddMatiere
    {
        public string OpenOption;
        public Guid FiliereDisplayedID;        
        private readonly Matiere MatiereDisplayed = new Matiere();
        private readonly BackgroundWorker worker = new BackgroundWorker ();
        private static List<MatiereStaffsModel> StaffBuff;
        

        public AddMatiere (Guid FiliereSelectedID, Matiere MatiereToDisplay = null )
        {
            InitializeComponent ();
            
            NIVEAU_.ItemsSource = App.DataS.GetFILIERE_NIVEAUX (FiliereSelectedID);
            HEURE_PAR_SEMAINE_.ItemsSource = App.DataS.GetMATIERE_HEURES_PAR_SEMAINE ();

            FiliereDisplayedID = FiliereSelectedID;

            if(MatiereToDisplay == null)
            {
                OpenOption = "Add";
                MatiereDisplayed.MATIERE_ID = Guid.NewGuid ();
                DisplayDefault ();
            }
            else
            {
                OpenOption = "Mod";
                MatiereDisplayed = MatiereToDisplay;
                DisplayMatiere (MatiereDisplayed);
                NIVEAU_.IsEnabled = false;
                TitleText.Text = "MODIFICATION";
            }                                     
        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            BusyIndicator.IsBusy = true;
            UpdateStaffs ();
        }

        private void DisplayDefault()
        {            
            NIVEAU_.SelectedIndex = 0;
            HEURE_PAR_SEMAINE_.SelectedIndex = 0;
        }

        private void DisplayMatiere(Matiere MatiereToDisplay)
        {
            if(MatiereToDisplay == null) return;

            MATIERE_NAME_.Text = MatiereToDisplay.NAME;
            NIVEAU_.SelectedValue = MatiereToDisplay.FILIERE_LEVEL; //App.DataS.GetFiliereMatiereNiveau (FiliereDisplayedID, MatiereToDisplay.MATIERE_ID);
            HEURE_PAR_SEMAINE_.SelectedValue = MatiereToDisplay.HEURE_PAR_SEMAINE;
                //App.DataS.GetFiliereMatiereHeuresParSemaine (FiliereDisplayedID, MatiereToDisplay.MATIERE_ID);
        }

        

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

            if(ChampsValidated () != true) return;
            
            MatiereDisplayed.NAME = MATIERE_NAME_.Text.Trim ();
            MatiereDisplayed.FILIERE_ID = FiliereDisplayedID;
            MatiereDisplayed.FILIERE_LEVEL = Convert.ToInt32(NIVEAU_.SelectedValue);
            MatiereDisplayed.HEURE_PAR_SEMAINE = HEURE_PAR_SEMAINE_.SelectedValue.ToString();
            
            if(OpenOption == "Add")
            {
                try
                {
                    App.DataS.AddMatiere(MatiereDisplayed);
                    UpdateMatiereInstructors ();
                    //App.DataS.SaveFiliereMatiere(FiliereDisplayedID, MatiereDisplayed.MATIERE_ID,
                        //Convert.ToInt32(NIVEAU_.SelectedValue.ToString()), HEURE_PAR_SEMAINE_.SelectedValue.ToString());                   
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
                    App.DataS.UpdateMatiere(MatiereDisplayed);
                    UpdateMatiereInstructors ();
                    //App.DataS.SaveFiliereMatiere(FiliereDisplayedID, MatiereDisplayed.MATIERE_ID,
                     //   Convert.ToInt32(NIVEAU_.SelectedValue.ToString()), HEURE_PAR_SEMAINE_.SelectedValue.ToString());
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

            if(string.IsNullOrEmpty (NIVEAU_.Text))
            {
                NIVEAU_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                NIVEAU_.BorderBrush = Brushes.Blue;
            }
           
            if(!Ok) ModernDialog.ShowMessage ("Verifier Les Informations !","Matrix",MessageBoxButton.OK);

            return Ok;
        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void UpdateMatiereInstructors()
        {
            Parallel.ForEach(StaffBuff, S =>
            {
                if (S.IsINSTRUCTOR)
                {
                    App.DataS.AddMatiereInstructor(MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
                }
                else
                {
                    App.DataS.DeleteMatiereInstructor(MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
                }
            });
        }

        #region Background Works
       
        private void UpdateStaffs ( )
        {
            if(worker.IsBusy) return;            
            worker.RunWorkerAsync ();
        }

        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            StaffBuff = GetStaffModelList (MatiereDisplayed.MATIERE_ID);
            if (OpenOption != "Mod") return;
            Parallel.ForEach(StaffBuff, S =>
            {
                S.IsINSTRUCTOR = App.DataS.IsMatiereInstructor(S.STAFF_ID, MatiereDisplayed.MATIERE_ID);
            });
        }

        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            InstructeursList.Items.Clear();
            InstructeursList.ItemsSource = StaffBuff;            
            worker.Dispose ();
        }

        private static List<MatiereStaffsModel> GetStaffModelList (Guid MatiereID)
        {
            var ML = new List<MatiereStaffsModel>();
            var Staffs = App.DataS.GetAllStaffs ();

            Parallel.ForEach(Staffs, S =>
            {
                var M = new MatiereStaffsModel
                {
                    STAFF_ID = S.STAFF_ID,
                    FULL_NAME = S.FULL_NAME,
                    PHOTO_IDENTITY = S.PHOTO_IDENTITY,
                    QUALIFICATION = S.QUALIFICATION,
                    IsINSTRUCTOR = App.DataS.IsMatiereInstructor(S.STAFF_ID, MatiereID)
                };
                ML.Add(M);
            });
            return ML;
        }


        #endregion

       
    }
}
