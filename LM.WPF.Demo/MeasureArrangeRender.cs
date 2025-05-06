using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LM.WPF.Demo
{
    internal class MeasureArrangeRender:Control
    {


        public int Test
        {
            get { return (int)GetValue(TestProperty); }
            set { SetValue(TestProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Test.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestProperty =
            DependencyProperty.Register("Test", typeof(int), typeof(MeasureArrangeRender), new FrameworkPropertyMetadata(0,
                //FrameworkPropertyMetadataOptions.AffectsMeasure
                //| 
                //FrameworkPropertyMetadataOptions.AffectsArrange
                //|
                FrameworkPropertyMetadataOptions.AffectsRender
                ));


        public MeasureArrangeRender()
        {
            
        }
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
        
    }
}
