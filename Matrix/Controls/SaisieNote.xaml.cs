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
    /// Interaction logic for SaisieNote.xaml
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
            
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
           
        }



    }
}
