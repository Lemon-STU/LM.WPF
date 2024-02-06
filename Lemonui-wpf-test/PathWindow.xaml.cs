using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// PathWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PathWindow : Window
    {
        public PathWindow()
        {
            InitializeComponent();
            this.SizeChanged += PathWindow_SizeChanged;
        }

        private void UiHandler(TaskCompletionSource<bool> taskCompletionSource)
        {
            taskCompletionSource.SetResult(true);
        }
        private void PathWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //M0,100  h300 A50,50 0 1 1 400,100 A50,50 0 0 1 300,100 M400,100 h400
            var w = this.ActualWidth;
            var h= this.ActualHeight;

            var margin = (w - 100) / 2;
            var vmargin = (h) / 2;
            StringBuilder sb = new StringBuilder();
            sb.Append($"M0,{vmargin} ");
            sb.Append($"h{margin} ");
            sb.Append($"A50,50 0 1 1 {margin+100},{vmargin} ");
            sb.Append($"A50,50 0 0 1 {margin},{vmargin} ");
            sb.Append($"L{margin+100},{vmargin} ");
            sb.Append($"h{margin} ");

            var geometry = Geometry.Parse(sb.ToString());
            path.Data = geometry;
        }
    }
}
