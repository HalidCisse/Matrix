using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using DataService.ViewModel;

namespace Matrix.views
{
    
    public partial class ClassDetails
    {
        private readonly BackgroundWorker Worker = new BackgroundWorker ();
        private List<FiliereLevelCard> ListBuff = new List<FiliereLevelCard> ();
        private string CurrentSelected;
        private bool isFirstTime = true;
        public string OpenedClassID { get; set; }
        public ClassDetails (string OpenClassID)
        {
            InitializeComponent ();

            OpenedClassID = OpenClassID;
            //ClassHeader.Text = App.Db.GetClasseByID (OpenClassID).NAME.ToUpper ();

        }












    }
}
