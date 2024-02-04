using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// PopupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopupWindow : Window
    {
        public PopupWindow()
        {
            InitializeComponent();

       
        }

        private void Rectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           
           // popup.StaysOpen = true;
            //this.rect.Focusable = false;
            //var ret= popup.Focus();
           // Console.WriteLine($"{ret}");
        }


        private void popup_Opened(object sender, EventArgs e)
        {
            //popup.StaysOpen = false;
           // popup.Focus();
            //var ret= popup.CaptureMouse();
            
            Console.WriteLine($"StaysOpen is false  ==={popup.StaysOpen}");
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            //popup.StaysOpen = true;
            Console.WriteLine("StaysOpen is true");
            
        }

        private void rect_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("=====================");

            popup.IsOpen = true;
        }

        private void btn_language_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("=======");
        }
    }
}
