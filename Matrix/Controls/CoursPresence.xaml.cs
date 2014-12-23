using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Matrix.Controls
{
    /// <summary>
    /// Une Page Pour Saisir La Presence A un Cours
    /// </summary>
    public partial class CoursPresence
    {
       

        /// <summary>
        /// Une Page Pour Saisir La Presence A un Cours
        /// </summary>
        public CoursPresence(Guid currentclassId, DateTime currentDayOfWeek)
        {
            InitializeComponent();

            TEXT_BLOCK1.Text = currentclassId.ToString();
            TEXT_BLOCK.Text = currentDayOfWeek.ToString();

            //Dispatcher.BeginInvoke(new Action(() => { SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_classId, DateTime.Now); }));
        }


       




    }
}
