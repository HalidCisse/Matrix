using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.Controls
{
    /// <summary>
    /// Interaction logic for ListSelector.xaml
    /// </summary>
    public partial class ListSelector
    {
        private bool _shouldFire;
        /// <summary>
        /// Content the Key/Value to iterate over
        /// </summary>
        public Dictionary<string, string> DataDictionary
        {            
            set
            {
                if (value == null) return;                
                THE_COMBO_BOX.ItemsSource = null;
                THE_COMBO_BOX.ItemsSource = value;
                _shouldFire = false;
                THE_COMBO_BOX.SelectedIndex = 0;
                THE_LABEL.Content = THE_COMBO_BOX.Text;
            }
        } 

        /// <summary>
        /// Fire When New Value is Selected
        /// </summary>
        public event EventHandler OnSelectionChanged;

        /// <summary>
        /// Content the Current Selceted Value
        /// </summary>
        public string SelectedValue
        {
            get { return THE_COMBO_BOX.SelectedValue?.ToString(); }
            set
            {
                THE_COMBO_BOX.SelectedValue = value;
                THE_LABEL.Content = THE_COMBO_BOX.Text;
            }
        }

        /// <summary>
        /// A Control For Iterating A Dictionary
        /// </summary>
        public ListSelector()
        {
            InitializeComponent();                      
        }
        
        private void BackwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (THE_COMBO_BOX.SelectedIndex < 1) return;
           
            THE_COMBO_BOX.SelectedIndex = THE_COMBO_BOX.SelectedIndex - 1;                    
            THE_LABEL.Content = THE_COMBO_BOX.Text;
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (THE_COMBO_BOX.SelectedIndex + 1 >= THE_COMBO_BOX.Items.Count) return;

            THE_COMBO_BOX.SelectedIndex = THE_COMBO_BOX.SelectedIndex + 1;            
            THE_LABEL.Content = THE_COMBO_BOX.Text;
        }

        private void TheComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {                        
            if (!_shouldFire) { _shouldFire = true;}
            else OnSelectionChanged?.Invoke(SelectedValue, e);

            BACKWARD_BUTTON.IsEnabled = THE_COMBO_BOX.SelectedIndex > 0;
            FORWARD_BUTTON.IsEnabled = (THE_COMBO_BOX.SelectedIndex + 1) < THE_COMBO_BOX.Items.Count;           
        }


       
    }
}
