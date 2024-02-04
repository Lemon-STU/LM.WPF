using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBox : UserControl
    {
        List<string> list = new List<string>();
        public SearchBox()
        {
            InitializeComponent();
            this.MouseLeave += SearchBox_MouseLeave;
            this.MouseEnter += SearchBox_MouseEnter;

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
            listbox.ItemsSource = Items;
            
        }
        public ObservableCollection<string> Items { get; private set; }
        private void SearchBox_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse enter the SearchBox");
        }

        private void SearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Mouse leave the SearchBox");
            var pos= e.GetPosition(this);
            
            Console.WriteLine(pos);
            //popup.IsOpen = false;
        }

        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(searchbox.Text))
            {
                popup.IsOpen = false;
                return;
            }
            var items = list.Where(p => p.Contains(searchbox.Text));
            if (items != null && items.Count() > 0)
            {
                Items.Clear();
                foreach (var item in items)
                    Items.Add(item);
                popup.IsOpen = true;
            }
            else
            {
                popup.IsOpen = false;
            }
        }
    }
}
