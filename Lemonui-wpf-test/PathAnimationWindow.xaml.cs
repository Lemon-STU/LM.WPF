using Lemonui_wpf.Helper.Gemometry;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// PathAnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PathAnimationWindow : Window
    {
        public PathAnimationWindow()
        {
            InitializeComponent();
            var len= this.path.Data.GetLength()/this.path.StrokeThickness;
            this.path.StrokeDashArray = new DoubleCollection { len};
            this.pathanimation.From = len;

            var namescope=NameScope.GetNameScope(this.path);

            //PointAnimationUsingPath
            //DoubleAnimationUsingPath
            //MatrixAnimationUsingPath

           // MatrixAnimationUsingPath
        }
    }
}
