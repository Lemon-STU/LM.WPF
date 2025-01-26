using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LM.WPF.Controls
{
    public class LMStackPanel:StackPanel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            double maxX = 0;
            double maxY = 0;
            foreach (FrameworkElement child in Children)
            {
                if (!double.IsNaN(child.Height))
                {
                    var h = child.Height + child.Margin.Top + child.Margin.Bottom;
                    var w = child.Width + child.Margin.Left + child.Margin.Right;
                    if (h > maxY)
                        maxY = h;
                    if (w > maxX)
                        maxX = w;
                }
            }
            var avaiSize = Orientation == Orientation.Horizontal ?
                new Size(constraint.Width, maxY)
                :
                new Size(maxX, constraint.Height)
                ;
            foreach (FrameworkElement child in Children)
            {
                child.Measure(avaiSize);
                var d = child.DesiredSize;
            }
            return avaiSize;
        }
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            double aviableSpace = Orientation==Orientation.Horizontal? arrangeSize.Width:arrangeSize.Height;
            double averageSpace = aviableSpace;
            int itemCount = 0;
            foreach (FrameworkElement item in Children)
            {
                if(Orientation == Orientation.Horizontal)
                {
                    if (!double.IsNaN(item.Width))
                    {
                        aviableSpace -=item.DesiredSize.Width;
                    }
                    else
                    {
                        itemCount++;
                        aviableSpace -= (item.Margin.Left + item.Margin.Right);
                    }
                }
                else
                {
                    if (!double.IsNaN(item.Height))
                    {
                        aviableSpace -= item.DesiredSize.Height;
                    }
                    else
                    {
                        itemCount++;
                        aviableSpace -= item.Margin.Top + item.Margin.Bottom;
                    }
                }
                
            }
            if(itemCount>0)
                averageSpace = aviableSpace / itemCount;

            double posx = 0;
            foreach (FrameworkElement item in Children)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    var rect = new Rect(posx, 0, item.DesiredSize.Width, item.DesiredSize.Height);
                    if (double.IsNaN(item.Width))
                        rect = new Rect(posx, 0, (item.Margin.Left + item.Margin.Right) + averageSpace, item.DesiredSize.Height);
                    item.Arrange(rect);
                    posx += rect.Width;
                }
                else
                {
                    var rect = new Rect(0, posx, item.DesiredSize.Width, item.DesiredSize.Height);
                    if (double.IsNaN(item.Height))
                        rect = new Rect(0,posx,item.DesiredSize.Width,averageSpace+item.Margin.Top+item.Margin.Bottom);
                    item.Arrange(rect);
                    posx += rect.Height;
                }
            }


            //if (Orientation == Orientation.Horizontal)
            //{
            //    double aviableXspace = arrangeSize.Width;
            //    int itemCount = 0;
            //    foreach (FrameworkElement item in Children)
            //    {
            //        if (!double.IsNaN(item.Width))
            //        {
            //            aviableXspace -= item.DesiredSize.Width;
            //        }
            //        else
            //        {
            //            itemCount++;
            //            aviableXspace -= (item.Margin.Left + item.Margin.Right);
            //        }
            //    }
            //    double averageW = aviableXspace;
            //    if(itemCount!=0)
            //        averageW = aviableXspace / itemCount;
            //    double posx = 0;
            //    foreach (FrameworkElement item in Children)
            //    {
            //        var rect = new Rect(posx,0,item.DesiredSize.Width, item.DesiredSize.Height);
            //        if(double.IsNaN(item.Width))
            //            rect=new Rect(posx,0, (item.Margin.Left + item.Margin.Right) + averageW,item.DesiredSize.Height);
            //        item.Arrange(rect);
            //        posx += rect.Width;
            //    }
            //}
            return arrangeSize;
        }
    }
}
