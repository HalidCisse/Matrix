using System;
using System.Windows;
using System.Windows.Input;

namespace Matrix.Styles
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MyForm
    {
      
        void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.Close());
        }

        void TitleBarMouseMove ( object sender, MouseEventArgs e )
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                sender.ForWindowFromTemplate (w =>
                {
                    if(w.WindowState != WindowState.Maximized) return;
                    w.BeginInit ();
                    const double adjustment = 40.0;
                    var mouse1 = e.MouseDevice.GetPosition (w);
                    var width1 = Math.Max (w.ActualWidth - 2 * adjustment, adjustment);
                    w.WindowState = WindowState.Normal;
                    var width2 = Math.Max (w.ActualWidth - 2 * adjustment, adjustment);
                    w.Left = (mouse1.X - adjustment) * (1 - width2 / width1);
                    w.Top = -7;
                    w.EndInit ();
                    w.DragMove ();
                });
            }
        }

        void TitleBarMouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
        {            
            if (e.LeftButton != MouseButtonState.Pressed) return;
            sender.ForWindowFromTemplate (w => w.DragMove ());
        }
    }
}