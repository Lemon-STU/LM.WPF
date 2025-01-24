using LM.WPF.Helper.Gemometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace LM.WPF.Controls
{
    public class LMImage:Image
    {
        private bool _isDirty = false;


        public LMImage()
        {

        }



        public double BorderThickness
        {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }        
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(double), typeof(LMImage),new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender
                ));


        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(LMImage), new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender
                ));




        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LMImage), new FrameworkPropertyMetadata(default(CornerRadius),FrameworkPropertyMetadataOptions.AffectsRender));

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            _isDirty = true;
        }
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var geometry = GeometryHelper.GenerateBorderGeometry(this, this.CornerRadius);
            if (this.Clip != null)
            {
                var a1 = this.Clip.GetArea();
                var a2 = geometry.GetArea();
                if (a1 != a2)
                    this.Clip = geometry;
            }
            else
                this.Clip = geometry;
            dc.DrawGeometry(Brushes.Transparent, new Pen(BorderBrush, BorderThickness), geometry);
        }
    }
}
