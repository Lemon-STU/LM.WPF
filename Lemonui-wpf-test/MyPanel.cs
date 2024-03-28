using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    internal class MyPanel:Panel
    {
        Rectangle rect=new Rectangle() { MinWidth = 100 };
        Rectangle rect1 = new Rectangle();
        public MyPanel() {
            rect.Fill = Brushes.Red;
            rect1.Fill = Brushes.Green;
            this.Children.Add(rect);
            this.Children.Add(rect1);
        }
        protected override Size MeasureOverride(Size availableSize)
        {
           // var size=new Size(500,500);
            //rect.Measure(size);
            rect.Measure(availableSize);
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var rectDesiredSize = rect.DesiredSize;
            rect.Arrange(new Rect(0, 0, 100, 100));
            rect1.Arrange(new Rect(100, 100, 100, 100));
            //finalSize.Width = 300;
            //finalSize.Height = 800;
            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            var renderSize = this.RenderSize;
            base.OnRender(dc);
            var rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
            Console.WriteLine(rect);
            rect.Inflate(-1, -1);
            dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Red, 1),rect) ;
        }
    }
}
