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
        /// <summary>
        /// Content the Key/Value to iterate over
        /// </summary>
        public Dictionary<string, string> DataDictionary
        {            
            set
            {
                if (value == null) return;                
                TheComboBox.ItemsSource = null;
                TheComboBox.ItemsSource = value;
                TheComboBox.SelectedIndex = 0;
                TheLabel.Content = TheComboBox.Text;
            }
        } 

        /// <summary>
        /// Fire When New Value is Selected
        /// </summary>
        public event EventHandler OnSelectionChanged;

        /// <summary>
        /// Content the Current Selceted Value
        /// </summary>
        public string SelectedValue { get; set; }

        /// <summary>
        /// A Control For Iterating A Dictionary
        /// </summary>
        public ListSelector()
        {
            InitializeComponent();                      
        }
        
        private void BackwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (TheComboBox.SelectedIndex < 1) return;
           
            TheComboBox.SelectedIndex = TheComboBox.SelectedIndex - 1;                    
            TheLabel.Content = TheComboBox.Text;
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (TheComboBox.SelectedIndex + 1 >= TheComboBox.Items.Count) return;

            TheComboBox.SelectedIndex = TheComboBox.SelectedIndex + 1;            
            TheLabel.Content = TheComboBox.Text;
        }

        private void TheComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            SelectedValue = TheComboBox.SelectedValue as string ;
            OnSelectionChanged?.Invoke(TheComboBox.SelectedValue, e);

            BackwardButton.IsEnabled = TheComboBox.SelectedIndex > 0;
            ForwardButton.IsEnabled = (TheComboBox.SelectedIndex + 1) < TheComboBox.Items.Count;           
        }
       
    }
}
