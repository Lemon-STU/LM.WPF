using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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

namespace LM.WPF
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Lemonui_wpf"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Lemonui_wpf;assembly=Lemonui_wpf"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomControl1 : Control
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
        }

        public CustomControl1()
        {
            Rect = new Rect(100,100,100,100);
            HotBrush = Brushes.AliceBlue;
            PenBrush = Brushes.AliceBlue;
        }
        public Rect Rect { get; set; }

        private Brush _HotBrush;
        private Brush _PenBrush;
        public Brush HotBrush { 
            get { return _HotBrush; } 
            set {
                if(_HotBrush != value)
                {
                    _HotBrush = value;
                    this.InvalidateVisual();
                    Console.WriteLine("invalid visual");
                }
            } 
        }
        public Brush PenBrush { 
            get { return _PenBrush; } 
            set { 
                if(_PenBrush != value) {
                    _PenBrush = value;
                    this.InvalidateVisual();
                    Console.WriteLine("invalid visual");
                }
            } 
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (Rect.Contains(e.GetPosition(this)))
            {
                PenBrush = Brushes.Red;
            }

        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Rect.Contains(e.GetPosition(this)))
            {
                HotBrush= Brushes.DarkTurquoise;
                Cursor = Cursors.Hand;
            }
            else
            {
                HotBrush = Brushes.AliceBlue;
                Cursor = Cursors.Arrow;
                PenBrush= Brushes.AliceBlue;
            }
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var bgColor = Color.FromRgb(0xF9, 0xF9, 0xF9);
            dc.DrawRectangle(new SolidColorBrush(bgColor), null, new Rect(0, 0, ActualWidth, ActualHeight));
            var text = new FormattedText("ABC l 123 Gfijdr", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);
            dc.DrawText(text, new Point(0, 0));
            dc.DrawLine(new Pen(Brushes.Black, 1), new Point(0.5, 10.5), new Point(50.5, 10.5));
            dc.DrawLine(new Pen(Brushes.Black, 1), new Point(30.5, 0.5), new Point(30.5, 40.5));

            dc.DrawRectangle(HotBrush, new Pen(PenBrush, 1), Rect);
        }
    }
}
