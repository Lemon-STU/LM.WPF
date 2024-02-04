using Lemonui_wpf.Common;
using Lemonui_wpf.Helper.Gemometry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> list=new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            //var len =path.Data.GetLength();
            //path.StrokeDashArray = new DoubleCollection {len};
            //pathanimation.From = len;
            //Popup

            list = new List<string>() {
                "11111",
                "22222",
                "33333",
                "44444",
                "55555",
                "23222",
                "34333",
                "45444",
                "56555"
            };
            Items = new ObservableCollection<string>();
            listbox.ItemsSource=Items;

            this.Deactivated += delegate {
                this.Focusable = false;
                Console.WriteLine("deeactivated............");
                popup.IsOpen = false;
            };

            
        }

        public ObservableCollection<string> Items { get; private set; }

        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(searchbox.Text))
            {
                popup.IsOpen = false;
                return;
            }
            var items= list.Where(p => p.Contains(searchbox.Text));
            if(items != null && items.Count()>0)
            {
                Items.Clear();
                foreach(var item in items)
                    Items.Add(item);
                popup.IsOpen = true;
                //listbox.Focus();
            }
            else
            { 
                popup.IsOpen = false; 
            }
        }

        private void searchbox_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine($"===");
        }

        private void menu_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine($"===");
        }

        private void menu_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("xxxxxxxxxx");
        }
    }
}
