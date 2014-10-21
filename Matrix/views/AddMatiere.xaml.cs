using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Matrix.Model;

namespace Matrix.views
{
    
    public partial class AddMatiere
    {
        public string OpenOption { get; set; }

        public string FiliereDisplayedID { get; set; }
        
        public Matiere MatiereDisplayed = new Matiere();

        public AddMatiere (string FiliereSelectedID, string MatiereToDisplayID = null )
        {
            InitializeComponent ();
           
            NIVEAU_.ItemsSource = App.Db.GetFILIERE_NIVEAUX (FiliereSelectedID);

            FiliereDisplayedID = FiliereSelectedID;

            if (!string.IsNullOrEmpty(MatiereToDisplayID))
            {
                MatiereDisplayed = App.Db.GetMatiereByID(MatiereToDisplayID);
                DisplayMatiere(MatiereDisplayed);                
            }
            else
            {
                MatiereDisplayed.MATIERE_ID = Guid.NewGuid().ToString();
                DisplayDefault ();
            }               
        }

        private void DisplayDefault()
        {
            NIVEAU_.SelectedIndex = 0;            
        }

        private void DisplayMatiere(Matiere MatiereToDisplay)
        {
            if(MatiereToDisplay == null) return;

            MATIERE_NAME_.Text = MatiereToDisplay.NAME;
            NIVEAU_.SelectedValue = App.Db.GetFiliereMatiereNiveau (FiliereDisplayedID, MatiereToDisplay.MATIERE_ID);            
        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            UpdateStaffs ();
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

            if(ChampsValidated () != true) return;
            
            MatiereDisplayed.NAME = MATIERE_NAME_.Text.Trim ();
            
            if(OpenOption == "Add")
            {
                if(!App.Db.AddMatiere (MatiereDisplayed))
                {
                    ModernDialog.ShowMessage ("Erreur D Enregistrement", "Matrix", MessageBoxButton.OK);
                }
                else
                {
                    UpdateMatiereInstructors ();
                    ModernDialog.ShowMessage (App.Db.AddFiliereMatiere (FiliereDisplayedID, MatiereDisplayed.MATIERE_ID,
                        Convert.ToInt32(NIVEAU_.SelectedValue.ToString()))
                        ? "Success" : "Erreur", "Matrix", MessageBoxButton.OK);                   
                }
                Close ();
            }
            else
            {
                if(App.Db.UpdateMatiere (MatiereDisplayed))
                {
                    UpdateMatiereInstructors ();
                    ModernDialog.ShowMessage(
                        App.Db.UpdateFiliereMatiere (FiliereDisplayedID, MatiereDisplayed.MATIERE_ID,
                            Convert.ToInt32(NIVEAU_.SelectedValue.ToString()))
                            ? "Success" : "Erreur", "Matrix", MessageBoxButton.OK);                    
                }
                else
                {
                    ModernDialog.ShowMessage ("Erreur", "Matrix", MessageBoxButton.OK);
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


        #region Background Works

        private readonly BackgroundWorker worker = new BackgroundWorker ();

        private List<MatiereStaffsModel> StaffBuff;

        private void UpdateStaffs ( )
        {
            if(worker.IsBusy) return;
            BusyIndicator.IsBusy = true;
            worker.RunWorkerAsync ();
        }

        private void worker_DoWork ( object sender, DoWorkEventArgs e )
        {
            StaffBuff = GetStaffModelList (MatiereDisplayed.MATIERE_ID);              
        }

        private void worker_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            BusyIndicator.IsBusy = false;
            InstructeursList.Items.Clear();
            InstructeursList.ItemsSource = StaffBuff;
            worker.Dispose ();
        }

        private static List<MatiereStaffsModel> GetStaffModelList (string MatiereID)
        {
            var ML = new List<MatiereStaffsModel>();
            var Staffs = App.Db.GetAllStaffs ();

            foreach (var S in Staffs)
            {
                var M = new MatiereStaffsModel
                {
                    STAFF_ID = S.STAFF_ID,
                    FULL_NAME = S.FULL_NAME,
                    PHOTO_IDENTITY = S.PHOTO_IDENTITY,
                    QUALIFICATION = S.QUALIFICATION,
                    IsINSTRUCTOR = App.Db.IsMatiereInstructor(S.STAFF_ID, MatiereID)
                };

                ML.Add(M);
            }
            return ML;
        }


        #endregion


        private void UpdateMatiereInstructors()
        {
            foreach (var S in StaffBuff)
            {
                if (S.IsINSTRUCTOR)
                {
                    App.Db.AddMatiereInstructor (MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
                }
                else
                {
                    App.Db.DeleteMatiereInstructor (MatiereDisplayed.MATIERE_ID, S.STAFF_ID);
                }
            }
        }






    }
}
