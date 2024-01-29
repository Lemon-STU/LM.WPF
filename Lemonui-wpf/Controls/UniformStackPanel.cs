using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Lemonui_wpf.Controls
{
    public class UniformStackPanel : Panel
    {
        public UniformStackPanel()
        {
            this.ClipToBounds = true;
        }



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
            var counts = this.InternalChildren.Count;

            foreach (UIElement child in this.InternalChildren)
            {
                if (Orientation == Orientation.Horizontal)
                {
                    child.Measure(new Size(availableSize.Width / counts, availableSize.Height));
                }
                else
                {
                    child.Measure(new Size(availableSize.Height / counts, availableSize.Height));
                }
            }
            return availableSize;
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
                    child.Arrange(new Rect(i * (w + Space) + Padding.Left, Padding.Top, w, finalSize.Height - Padding.Top - Padding.Bottom));
                }
            }
            else
            {
                var w = (finalSize.Height - Space * (count - 1) - Padding.Top - Padding.Bottom) / count;
                for (int i = 0; i < InternalChildren.Count; i++)
                {
                    var child = InternalChildren[i];
                    child.Arrange(new Rect(Padding.Left, i * (w + Space) + Padding.Top, finalSize.Width - Padding.Left - Padding.Right, w));
                }
            }

            //double desiredWidth = 0;
            //for (int i = 0;i<count;i++)
            //{
            //    desiredWidth += InternalChildren[i].DesiredSize.Width;
            //}

            //double space = (finalSize.Width - desiredWidth - 20) / (count-1);
            //double posx = 10;
            //for(int i = 0;i<count;i++)
            //{
            //    var child = InternalChildren[i];
            //    child.Arrange(new Rect(posx,0,child.DesiredSize.Width,finalSize.Height));
            //    posx += child.DesiredSize.Width+space;
            //}
            return finalSize;
        }
    }
}
