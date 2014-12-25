using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataService.Entities.Pedagogy;
using DataService.ViewModel;
using FirstFloor.ModernUI.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Matrix.Controls
{
    /// <summary>
    /// Page Pour Saisir La Presence A un Cours
    /// </summary>
    public partial class SaisiePresence
    {      
        private readonly Guid _currentCoursGuid;
        private readonly DateTime _currentCoursDate;

        /// <summary>
        /// Page Pour Saisir La Presence A un Cours
        /// </summary>
        public SaisiePresence(Guid currentCoursGuid, DateTime coursDate)
        {
            _currentCoursGuid = currentCoursGuid;
            _currentCoursDate = coursDate;

            InitializeComponent();
            
            new Task(() => Dispatcher.BeginInvoke(new Action(() =>
            {
                TICKET_LIST.ItemsSource = App.ModelS.GetAbsencesTiketCards(currentCoursGuid, coursDate);

            }))).RunSynchronously();           
        }

        private void THE_CHECK_BOX_OnClick(object sender, RoutedEventArgs e)
        {
            var item = ((Border)((StackPanel)((Grid)((CheckBox) sender).Parent).Parent).Parent).Parent as ListBoxItem;
            var ticketCard = item?.DataContext as AbsenceTicketCard  ;
            
            var absenceTicket = new AbsenceTicket()
            {
                AbsenceTicketGuid = ticketCard.AbsenceTicketGuid,
                CoursGuid = _currentCoursGuid,
                CoursDate = _currentCoursDate,
                PersonGuid = ticketCard.PersonGuid,
                IsPresent = ticketCard.IsPresent,
                RetardTime = new TimeSpan(0,0, ticketCard.RetardTime,0)
            };

            try
            {
                App.DataS.Pedagogy.AbsenceTicket.AddOrUpdateAbsenceTicket(absenceTicket);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                ModernDialog.ShowMessage("Erreur de Connection a la Base de Donneé", "ERREUR", MessageBoxButton.OK);
            }
         }

        private void UpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = ((Border)((StackPanel)((Grid)((IntegerUpDown)sender).Parent).Parent).Parent).Parent as ListBoxItem;
            var ticketCard = item?.DataContext as AbsenceTicketCard;

            var absenceTicket = new AbsenceTicket()
            {
                AbsenceTicketGuid = ticketCard.AbsenceTicketGuid,
                CoursGuid = _currentCoursGuid,
                CoursDate = _currentCoursDate,
                PersonGuid = ticketCard.PersonGuid,
                IsPresent = ticketCard.IsPresent,
                RetardTime = new TimeSpan(0, 0, ticketCard.RetardTime, 0)
            };

            try
            {
                App.DataS.Pedagogy.AbsenceTicket.AddOrUpdateAbsenceTicket(absenceTicket);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                ModernDialog.ShowMessage("Erreur de Connection a la Base de Donneé", "ERREUR", MessageBoxButton.OK);               
            }            
        }




    }
}
