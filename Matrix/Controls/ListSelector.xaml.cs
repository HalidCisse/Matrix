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
        public Dictionary<string, Guid> DataDictionary { get; set; } = new Dictionary<string, Guid>();

        /// <summary>
        /// Fire When New Value is Selected
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Content the Current Selceted Value
        /// </summary>
        public Guid SelectedGuid;

        /// <summary>
        /// A Control For Iterating A Dictionary
        /// </summary>
        public ListSelector()
        {
            InitializeComponent();
           
            DataDictionary.Add("Key 1", Guid.NewGuid());
            DataDictionary.Add("Key 2", Guid.NewGuid());
            DataDictionary.Add("Key 3", Guid.NewGuid());
            DataDictionary.Add("Key 4", Guid.NewGuid());
            DataDictionary.Add("Key 5", Guid.NewGuid());
            DataDictionary.Add("Key 6", Guid.NewGuid());
            DataDictionary.Add("Key 7", Guid.NewGuid());
            DataDictionary.Add("Key 8", Guid.NewGuid());

            DataContext = this;
           
        }
        
        private void BackwardButton_OnClick(object sender, RoutedEventArgs e)
        {           
            TheComboBox.SelectedIndex = TheComboBox.SelectedIndex - 1;                    
            TheLabel.Content = TheComboBox.Text;
        }

        private void ForwardButton_OnClick(object sender, RoutedEventArgs e)
        {           
            TheComboBox.SelectedIndex = TheComboBox.SelectedIndex + 1;            
            TheLabel.Content = TheComboBox.Text;
        }

        private void TheComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show("The ID is : " + TheComboBox.SelectedValue);

            SelectedGuid = (Guid) TheComboBox.SelectedValue;
            SelectionChanged?.Invoke(TheComboBox.SelectedValue, e);

            BackwardButton.IsEnabled = TheComboBox.SelectedIndex > 0;
            ForwardButton.IsEnabled = (TheComboBox.SelectedIndex + 1) < TheComboBox.Items.Count;           
        }

        private void TheComboBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            TheComboBox.SelectedIndex = 0;
            TheLabel.Content = TheComboBox.Text;
        }
    }
}
