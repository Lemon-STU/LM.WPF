using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Lemonui_wpf.Controls
{
    public class CircularProgress:UserControl
    {
        static CircularProgress()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularProgress), new FrameworkPropertyMetadata(typeof(CircularProgress)));
        }
        public CircularProgress()
        {
            this.grid = new Grid();
            this.path = new Path();
            this.pathFigure = new PathFigure();
            this.arcSegment = new ArcSegment() { SweepDirection = SweepDirection.Clockwise, RotationAngle = 0, IsLargeArc = true };
            this.pathGeometry = new PathGeometry();
            this.grid.Children.Add(this.path);
            this.path.Data = this.pathGeometry;
            this.pathGeometry.Figures.Add(this.pathFigure);
            this.pathFigure.Segments.Add(this.arcSegment);
            this.Content = this.grid;
            this.SizeChanged += delegate {
                changeValue(this);
            };
        }


        /// <summary>
        /// to control the tick panel display or not
        /// </summary>
        public Visibility TickPanelVisibility
        {
            get { return (Visibility)GetValue(TickPanelVisibilityProperty); }
            set { SetValue(TickPanelVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TickPanelVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickPanelVisibilityProperty =
            DependencyProperty.Register("TickPanelVisibility", typeof(Visibility), typeof(CircularProgress), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.AffectsRender));


        /// <summary>
        /// to set the Arc padding
        /// </summary>
        public double CircularPadding
        {
            get { return (double)GetValue(CircularPaddingProperty); }
            set { SetValue(CircularPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CircularPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircularPaddingProperty =
            DependencyProperty.Register("CircularPadding", typeof(double), typeof(CircularProgress), new PropertyMetadata(5.0));

        /// <summary>
        /// the Arc color
        /// </summary>
        public Brush CircularColor
        {
            get { return (Brush)GetValue(CircularColorProperty); }
            set { SetValue(CircularColorProperty, value); }
        }
        public static readonly DependencyProperty CircularColorProperty =
            DependencyProperty.Register("CircularColor", typeof(Brush), typeof(CircularProgress), new PropertyMetadata(Brushes.Red, (d, e) => {
                var color = e.NewValue as Brush;
                CircularProgress control = d as CircularProgress;
                if (color != null && control != null)
                {
                    control.path.Stroke = color;
                }
            }));


        /// <summary>
        /// Arc Thickness
        /// </summary>
        public double CircularThickness
        {
            get { return (double)GetValue(CircularThicknessProperty); }
            set { SetValue(CircularThicknessProperty, value); }
        }




        public Brush CircularOrbitColor
        {
            get { return (Brush)GetValue(CircularOrbitColorProperty); }
            set { SetValue(CircularOrbitColorProperty, value); }
        }



        public double CircularOrbitThickness
        {
            get { return (double)GetValue(CircularOrbitThicknessProperty); }
            set { SetValue(CircularOrbitThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CircularOrbitThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircularOrbitThicknessProperty =
            DependencyProperty.Register("CircularOrbitThickness", typeof(double), typeof(CircularProgress), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));



        // Using a DependencyProperty as the backing store for CircularOrbitColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircularOrbitColorProperty =
            DependencyProperty.Register("CircularOrbitColor", typeof(Brush), typeof(CircularProgress), new FrameworkPropertyMetadata(Brushes.Gray,FrameworkPropertyMetadataOptions.AffectsRender));




        public static readonly DependencyProperty CircularThicknessProperty =
            DependencyProperty.Register("CircularThickness", typeof(double), typeof(CircularProgress), new PropertyMetadata(0.0, (d, e) => {
                var thickness = (double)e.NewValue;
                CircularProgress control = d as CircularProgress;
                if (control != null)
                {
                    control.path.StrokeThickness = thickness;
                }
            }));


        /// <summary>
        /// the progress of event
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(CircularProgress), new PropertyMetadata(0.0, valueChangedCallback));

        private Path path = null;
        private PathFigure pathFigure;
        private ArcSegment arcSegment;
        private PathGeometry pathGeometry;
        private Grid grid;

        static void valueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CircularProgress;
            if (control != null && !control.IsLoaded)
            {
                control.Loaded += (s, ex) => {
                    changeValue(control);
                };
            }
            else
            {
                changeValue(control);
            }
        }
        static void changeValue(CircularProgress control)
        {
            Point startPoint = new Point();
            Point endPoint = new Point();
            var w = control.ActualWidth;
            var h = control.ActualHeight;
            var halfw = w / 2;
            var halfh = h / 2;

            var rawRadius = halfw;
            if (h < w)
                rawRadius = halfh;

            var radius = rawRadius - control.CircularPadding;
            radius -= control.path.StrokeThickness;
            startPoint.X = halfw;
            startPoint.Y = halfh - radius;
            if (control.Progress >= 0.99999999)
                control.Progress = 0.999999;
            endPoint.X = halfw + radius * Math.Sin(control.Progress * 2 * Math.PI);
            endPoint.Y = halfh - radius * Math.Cos(control.Progress * 2 * Math.PI);

            control.pathFigure.StartPoint = startPoint;
            control.arcSegment.Size = new Size(radius, radius);
            control.arcSegment.Point = endPoint;
            if (control.Progress >= 0.5)
                control.arcSegment.IsLargeArc = true;
            else
                control.arcSegment.IsLargeArc = false;
        }


        private Pen strokeGreenPen = new Pen(Brushes.White, 1);
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var hw = this.ActualWidth / 2;
            var hh = this.ActualHeight / 2;
            if (this.TickPanelVisibility == Visibility.Visible)
            {               
                TranslateTransform translateTransform = new TranslateTransform(hw, hh);
                dc.PushTransform(translateTransform);

                for (int i = 0; i < 60; i++)
                {
                    RotateTransform rotateTransform = new RotateTransform();
                    dc.PushTransform(rotateTransform);
                    rotateTransform.Angle = i * 6;
                    if (i % 5 == 0)
                        dc.DrawLine(strokeGreenPen, new Point(hw - 15, 0), new Point(hw - 5, 0));
                    else
                        dc.DrawLine(strokeGreenPen, new Point(hw - 15, 0), new Point(hw - 10, 0));
                    dc.Pop();
                }
                dc.Pop();
            }

            dc.DrawEllipse(Brushes.Transparent, new Pen(CircularOrbitColor, this.CircularOrbitThickness), new Point(hw, hh), hw-CircularPadding-this.CircularThickness, hh - CircularPadding - this.CircularThickness);
        }
    }
}
