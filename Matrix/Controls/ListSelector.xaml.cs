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
            //get { return DataDictionary; }
            set
            {
                if (value == null) return;                
                TheComboBox.ItemsSource = null;
                TheComboBox.ItemsSource = value;
                TheComboBox.SelectedIndex = 0;
                TheLabel.Content = TheComboBox.Text;
            }
        } //= new Dictionary<string, string>();

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
           
            //DataDictionary.Add("Key 1", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 2", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 3", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 4", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 5", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 6", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 7", Guid.NewGuid().ToString());
            //DataDictionary.Add("Key 8", Guid.NewGuid().ToString());

           // DataContext = this;
           
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
            //MessageBox.Show("The ID is : " + TheComboBox.SelectedValue);

            SelectedValue = (string) TheComboBox.SelectedValue ;
            OnSelectionChanged?.Invoke(TheComboBox.SelectedValue, e);

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
