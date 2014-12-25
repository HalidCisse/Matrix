using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using DataService.Entities.Pedagogy;
using DataService.ViewModel;

namespace Matrix.Controls
{
    /// <summary>
    /// Une Page Pour Saisir La Presence A un Cours
    /// </summary>
    public partial class SaisiePresence
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AbsenceTicketCard> PresenseList { get; } = new List<AbsenceTicketCard>();

        /// <summary>
        /// Une Page Pour Saisir La Presence A un Cours
        /// </summary>
        public SaisiePresence(Guid currentCoursId, DateTime coursDate)
        {
            InitializeComponent();

            //var p1 = new AbsenceTicket()
            //{
            //    PersonGuid = new Guid("1fe0b429-bc8b-460f-9d44-04ae86e21946"),
            //    CoursDate = coursDate,
            //    CoursGuid = currentCoursId,
            //    AbsenceTicketGuid = Guid.NewGuid(),
            //    IsPresent = true,
            //    RetardTime = new TimeSpan(0,0,10,0)
            //};

            //PresenseList.Add(new AbsenceTicketCard(p1));

            //PERSONS_LIST.ItemsSource = PresenseList;
            //TEXT_BLOCK1.Text = currentCoursId.ToString();
            //TEXT_BLOCK.Text = coursDate.ToString(CultureInfo.CurrentCulture);

            //Dispatcher.BeginInvoke(new Action(() => { SCHEDULE_UI.ItemsSource = App.ModelS.GetClassWeekAgendaData(_classId, DateTime.Now); }));

            new Task(() => Dispatcher.BeginInvoke(new Action(() =>
            {
                PERSONS_LIST.ItemsSource = App.ModelS.GetAbsencesTiketCards(currentCoursId, coursDate);


            }))).RunSynchronously();

            


        }


       






    }
}
