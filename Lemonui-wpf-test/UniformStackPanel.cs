using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Data;
using System.Windows.Media;

namespace Lemonui_wpf_test
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
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(UniformStackPanel), new FrameworkPropertyMetadata(0.0,FrameworkPropertyMetadataOptions.AffectsRender));


        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(UniformStackPanel), new FrameworkPropertyMetadata(Brushes.Transparent,FrameworkPropertyMetadataOptions.AffectsRender));


        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(UniformStackPanel), new FrameworkPropertyMetadata(0.0,FrameworkPropertyMetadataOptions.AffectsRender));




        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Padding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(UniformStackPanel), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.AffectsRender));


        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(UniformStackPanel), new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsRender));


        public double Space
        {
            get { return (double)GetValue(SpaceProperty); }
            set { SetValue(SpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Space.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpaceProperty =
            DependencyProperty.Register("Space", typeof(double), typeof(UniformStackPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));


        protected override Size MeasureOverride(Size availableSize)
        {
            double currentSizew = 0;
            double currentSizeh = 0;
            var counts = this.InternalChildren.Count;
            if(Orientation == Orientation.Horizontal)
            {
                foreach (FrameworkElement child in this.InternalChildren)
                {
                    if (double.IsInfinity(availableSize.Width) || availableSize.Width==0)
                    {
                        if (child.Width > 0)
                        {
                            currentSizew += child.Width;
                        }
                        else if (child.MinWidth > 0)
                            currentSizew += child.Width;

                    }
                    else
                        currentSizew = availableSize.Width;

                    if (double.IsInfinity(availableSize.Height) || availableSize.Height==0)
                    {
                        if(child.Height > 0)
                        {
                            if (currentSizeh < child.Height)
                                currentSizeh = child.Height;
                        }
                        else if(child.MinHeight>0)
                            currentSizeh = child.MinHeight;
                    }
                    else
                        currentSizeh= availableSize.Height;
                    child.Measure(new Size(currentSizew, currentSizeh));
                }
                
                if (double.IsInfinity(availableSize.Width))
                {
                    currentSizew += (counts - 1) * Space + Padding.Left + Padding.Right;
                }
            }
            else
            {
                foreach (FrameworkElement child in this.InternalChildren)
                {
                    if (double.IsInfinity(availableSize.Height) || availableSize.Height == 0)
                    {
                        if (child.Height > 0)
                        {
                            currentSizeh += child.Height;
                        }
                        else if (child.MinHeight > 0)
                            currentSizeh += child.Height;

                    }
                    else
                        currentSizeh = availableSize.Height;

                    if (double.IsInfinity(availableSize.Width) || availableSize.Width == 0)
                    {
                        if (child.Width > 0)
                        {
                            if (currentSizew < child.Width)
                                currentSizew = child.Width;
                        }
                        else if (child.MinWidth > 0)
                            currentSizew = child.MinWidth;
                    }
                    else
                        currentSizew = availableSize.Width;
                    child.Measure(new Size(currentSizew, currentSizeh));
                }
                if(double.IsInfinity(availableSize.Height))
                    currentSizeh += (counts - 1) * Space + Padding.Top + Padding.Bottom;
            }            
            return new Size(currentSizew,currentSizeh);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var count = InternalChildren.Count;
            if (Orientation == Orientation.Horizontal)
            {
                var w = (finalSize.Width - Space * (count - 1) - Padding.Left - Padding.Right) / count;
                for (int i = 0; i < InternalChildren.Count; i++)
                {
                    var child = InternalChildren[i];
                    var rh = finalSize.Height - Padding.Top - Padding.Bottom;
                    if (rh < 0) rh = 0;
                    if (w < 0) w = 0;
                    child.Arrange(new Rect(i * (w + Space) + Padding.Left, Padding.Top, w, rh));
                }
            }
            else
            {
                var h = (finalSize.Height - Space * (count - 1) - Padding.Top - Padding.Bottom) / count;
                for (int i = 0; i < InternalChildren.Count; i++)
                {
                    var child = InternalChildren[i];
                    var rw = finalSize.Width - Padding.Left - Padding.Right;
                    if(rw < 0) rw = 0;
                    if(h < 0) h = 0;
                    child.Arrange(new Rect(Padding.Left, i * (h + Space) + Padding.Top, rw, h));
                }
            }
            return finalSize;
        }

        private Pen borderPen = null;
        protected override void OnRender(DrawingContext dc)
        {
            if (this.BorderThickness < 0) this.BorderThickness = 0;
            if (this.Radius < 0) this.Radius = 0;
            var rect = new Rect(this.BorderThickness / 2.0, this.BorderThickness / 2.0, this.ActualWidth - this.BorderThickness, this.ActualHeight - this.BorderThickness);
            dc.DrawRoundedRectangle(this.Background, new Pen(this.BorderBrush, this.BorderThickness), rect, Radius, Radius);
        }
    }
}
