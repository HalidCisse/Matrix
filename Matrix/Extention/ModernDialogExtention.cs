using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.Extention
{    
    static class ModernDialogExtension
    {
        static MessageBoxResult result;

        public static MessageBoxResult ShowDialogOKCancel ( this ModernDialog modernDialog )
        {
            result = MessageBoxResult.Cancel;

            modernDialog.OkButton.Click += OkButton_Click;
            modernDialog.Buttons = new[] { modernDialog.OkButton, modernDialog.CloseButton };

            modernDialog.ShowDialog ();

            return result;
        }

        private static void OkButton_Click ( object sender, RoutedEventArgs e )
        {
            result = MessageBoxResult.OK;
        }
    }
}
