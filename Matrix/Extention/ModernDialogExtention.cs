using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.Extention
{    
    static class ModernDialogExtension
    {
        static MessageBoxResult _result;

        public static MessageBoxResult ShowDialogOkCancel ( this ModernDialog modernDialog )
        {
            _result = MessageBoxResult.Cancel;

            modernDialog.OkButton.Click += OkButton_Click;
            modernDialog.Buttons = new[] { modernDialog.OkButton, modernDialog.CloseButton };

            modernDialog.ShowDialog ();

            return _result;
        }

        private static void OkButton_Click ( object sender, RoutedEventArgs e )
        {
            _result = MessageBoxResult.OK;
        }
    }
}
