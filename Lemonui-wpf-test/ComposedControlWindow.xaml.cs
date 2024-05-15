using Lemonui_wpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// Interaction logic for ComposedControlWindow.xaml
    /// </summary>
    public partial class ComposedControlWindow : Window,INotifyPropertyChanged
    {
        public ComposedControlWindow()
        {
            InitializeComponent();
            this.Loaded += ComposedControlWindow_Loaded;
        }

        private void ComposedControlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ctext.DataContext = this;
            Binding binding = new Binding("TextTest");
            binding.Source = this;
            //binding.Mode= BindingMode.TwoWay;
            //BindingOperations.SetBinding(ctext, CommonText.TextProperty, binding);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _TextTest= "LOG";
        public string TextTest
        {
            get { return _TextTest; } 
            set { 
                _TextTest = value; 
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextTest"));  
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ctext.Text = "GTLE(> <=)";
            MessageBox.Show(ctext.Text);
            MessageBox.Show(TextTest);

        }
    }
}
