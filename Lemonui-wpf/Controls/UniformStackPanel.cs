using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Lemonui_wpf.Controls
{
    public class UniformStackPanel : Panel
    {
        public UniformStackPanel()
        {
            this.ClipToBounds = true;
        }

        public double BorderThickness
        {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(UniformStackPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));


        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(UniformStackPanel), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));


        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(UniformStackPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));


        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(UniformStackPanel), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsRender));



        protected override Size MeasureOverride(Size availableSize)
        {
            double cw = 0;
            double tw = availableSize.Width;
            foreach (FrameworkElement item in this.InternalChildren)
            {
                if (!double.IsNaN(item.Width))
                    tw -= (item.Width + item.Margin.Left + item.Margin.Right);
            }
            if (this.InternalChildren.Count > 0)
                cw = tw / this.InternalChildren.Count;
            foreach (FrameworkElement item in this.InternalChildren)
            {
                if (double.IsNaN(item.Width))
                    item.Measure(new Size(cw, availableSize.Height));
                else
                    item.Measure(new Size(item.Width + item.Margin.Left + item.Margin.Right, availableSize.Height));
            }
            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double xpos = 0;
            double cw = 0;
            double tw = finalSize.Width;
            int itemCnt = 0;
            foreach (FrameworkElement item in this.InternalChildren)
            {
                if (!double.IsNaN(item.Width))
                    tw -= item.DesiredSize.Width;
                else
                    itemCnt++;
            }
            if (this.InternalChildren.Count > 0)
                cw = tw / itemCnt;

            foreach (FrameworkElement item in this.InternalChildren)
            {
                double w = item.DesiredSize.Width;
                if (double.IsNaN(item.Width))
                    w = cw;
                var posRect = new Rect(xpos, 0, w, item.DesiredSize.Height);
                Console.WriteLine($"item: {item} posRect: {posRect}");
                item.Arrange(posRect);
                xpos += w;
            }
            return finalSize;
        }

        //private Pen borderPen = null;
        protected override void OnRender(DrawingContext dc)
        {
            if (this.BorderThickness < 0) this.BorderThickness = 0;
            if (this.Radius < 0) this.Radius = 0;
            var rect= new Rect(this.BorderThickness/2.0, this.BorderThickness/2.0, this.ActualWidth-this.BorderThickness, this.ActualHeight-this.BorderThickness);
            dc.DrawRoundedRectangle(this.Background, new Pen(this.BorderBrush, this.BorderThickness),rect , Radius, Radius);
        }
    }
}
