using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lemonui_wpf.Controls
{
    public class MyWrapPanel:FrameworkElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Green, 1), new Rect(0, 0, 200, 100));
        }
    }
}
