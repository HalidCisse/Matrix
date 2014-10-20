using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataService.Entities;
using Matrix.Model;
using Xceed.Wpf.Toolkit;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for AddMatiere.xaml
    /// </summary>
    public partial class AddMatiere
    {
        public string OpenOption { get; set; }
        public string MatiereDisplayed { get; set; }

        public AddMatiere (string FiliereSelected, string MatiereToDisplay = null )
        {
            InitializeComponent ();
           
            NIVEAU_.ItemsSource = App.Db.GetFILIERE_NIVEAUX (FiliereSelected);

            MatiereDisplayed = MatiereToDisplay;
        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            UpdateStaffs ();
        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

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
            StaffBuff = GetStaffModelList (MatiereDisplayed);              
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


    }
}
