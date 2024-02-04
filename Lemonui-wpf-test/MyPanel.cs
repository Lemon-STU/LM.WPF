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
        Rectangle rect=new Rectangle();
        Rectangle rect1 = new Rectangle();
        public MyPanel() {
            rect.Fill = Brushes.Red;
            rect1.Fill = Brushes.Green;
            this.Children.Add(rect);
            this.Children.Add(rect1);
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            //rect.Measure(availableSize);
            return new Size(300,200);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            rect.Arrange(new Rect(0, 0, 100, 100));
            rect1.Arrange(new Rect(100, 100, 100, 100));
            finalSize.Height = 200;
            finalSize.Width = 300;
            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);
            Console.WriteLine(rect);
            dc.DrawRectangle(Brushes.Transparent, new Pen(Brushes.Red, 1), rect);
        }
    }
}
