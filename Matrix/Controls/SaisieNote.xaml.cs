using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataService.Entities.Pedagogy;
using DataService.ViewModel;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.Controls
{
    /// <summary>
    /// Control Pour Saisirr les Notes d'un Control
    /// </summary>
    public partial class SaisieNote
    {
        private readonly Guid _currentCoursGuid;
        
        /// <summary>
        /// 
        /// </summary>
        public SaisieNote(Guid currentCoursGuid)
        {
            _currentCoursGuid = currentCoursGuid;
            InitializeComponent();

            new Task(() => Dispatcher.BeginInvoke(new Action(() =>
            {
                NOTE_LIST.ItemsSource = App.ModelS.GetStudentsNotesCards(_currentCoursGuid);

            }))).RunSynchronously();
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {            
            var studentNoteCard = (StudentNoteCard)((Slider) sender).DataContext;
            
            var studentNote = new StudentNote
            {
                StudentNoteGuid = studentNoteCard.StudentNoteGuid,
                CoursGuid = studentNoteCard.CoursGuid,
                StudentGuid = studentNoteCard.StudentGuid,
                Note = studentNoteCard.Note,
                Appreciation = studentNoteCard.Appreciation,
            };

            try
            {
                App.DataS.Pedagogy.StudentNote.AddOrUpdateStudentNote(studentNote);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ModernDialog.ShowMessage("Erreur de Connection a la Base de Donneé", "ERREUR", MessageBoxButton.OK);
            }
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            var studentNoteCard = (StudentNoteCard)((TextboxW)sender).DataContext;
            
            var studentNote = new StudentNote
            {
                StudentNoteGuid = studentNoteCard.StudentNoteGuid,
                CoursGuid = studentNoteCard.CoursGuid,
                StudentGuid = studentNoteCard.StudentGuid,
                Note = studentNoteCard.Note,
                Appreciation = studentNoteCard.Appreciation //((TextboxW)sender).Text
            };

            try
            {
                App.DataS.Pedagogy.StudentNote.AddOrUpdateStudentNote(studentNote);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ModernDialog.ShowMessage("Erreur de Connection a la Base de Donneé", "ERREUR", MessageBoxButton.OK);
            }

        }



    }
}
