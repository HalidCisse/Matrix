using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Matrix.Controls
{
    /// <summary>
    /// Un TextBox avec Watermark
    /// </summary>
    public class TextboxW : TextBox
    {
        /// <summary>
        /// 
        /// </summary>
        public string Watermark
        {
            get { return (string)GetValue(WaterMarkProperty); }
            set { SetValue(WaterMarkProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(TextboxW), new PropertyMetadata(OnWatermarkChanged));

        private bool _isWatermarked;
        private Binding _textBinding;

        /// <summary>
        /// 
        /// </summary>
        public TextboxW()
        {
            Loaded += (s, ea) => ShowWatermark();
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="E:System.Windows.UIElement.GotFocus"/> event reaches this element in its route.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            HideWatermark();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.UIElement.LostFocus"/> event (using the provided arguments).
        /// </summary>
        /// <param name="e">Provides data about the event.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            ShowWatermark();
        }

        private static void OnWatermarkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs ea)
        {
            var tbw = sender as TextboxW;
            if (tbw == null || !tbw.IsLoaded) return; //needed to check IsLoaded so that we didn't dive into the ShowWatermark() routine before initial Bindings had been made
            tbw.ShowWatermark();
        }

        private void ShowWatermark()
        {
            if (!string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(Watermark)) return;

            _isWatermarked = true;

            //save the existing binding so it can be restored
            _textBinding = BindingOperations.GetBinding(this, TextProperty);

            //blank out the existing binding so we can throw in our Watermark
            BindingOperations.ClearBinding(this, TextProperty);

            //set the signature watermark gray
            Foreground = new SolidColorBrush(Color.FromRgb(191, 186, 186));

            //display our watermark text
            Text = Watermark;
        }

        private void HideWatermark()
        {
            if (!_isWatermarked) return;

            _isWatermarked = false;
            ClearValue(ForegroundProperty);
            Text = "";
            if (_textBinding != null) SetBinding(TextProperty, _textBinding);
        }
    }
}



