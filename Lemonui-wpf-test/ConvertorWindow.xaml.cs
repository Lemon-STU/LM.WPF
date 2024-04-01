using System;
using System.Collections.Generic;
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
    /// Interaction logic for ConvertorWindow.xaml
    /// </summary>
    public partial class ConvertorWindow : Window
    {
        public ConvertorWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string ConnectStatusLabel { get; set; } = "isconnect1";
    }
}
