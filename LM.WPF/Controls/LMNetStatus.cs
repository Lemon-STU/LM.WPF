using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace LM.WPF.Controls
{
    public class LMNetStatus:UserControl
    {
        private Path path;
        static LMNetStatus()
        {
           // DefaultStyleKeyProperty.OverrideMetadata(typeof(LMNetStatus),new FrameworkPropertyMetadata(typeof(LMNetStatus)));
        }

        public LMNetStatus()
        {
            path = new Path();
            path.Data = Geometry.Parse("M0,80 h200 h-80 v-20 h-40 v-60 h80 v60 h-40 v20 h-50 v20 h-40 v60 h80 v-60 h-40 v-20 h100 v20 h-40 v60 h80 v-60 h-40 v-20");
            path.Stroke =this.StrokeColor;
            path.StrokeThickness = this.StrokeThickness;
            path.Fill = DisConnectColor;
            Viewbox viewbox = new Viewbox();
            this.AddChild(viewbox);
            viewbox.Child = path;
        }


        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }
        public static readonly DependencyProperty IsConnectedProperty =
            DependencyProperty.Register("IsConnected", typeof(bool), typeof(LMNetStatus), new PropertyMetadata(false, (d, e) => { 
                var control=d as LMNetStatus;
                if (control != null)
                {
                    var isconnected = (bool)e.NewValue;
                    control.Animate(isconnected);
                }
            }));

        public Brush DisConnectColor
        {
            get { return (Brush)GetValue(DisConnectColorProperty); }
            set { SetValue(DisConnectColorProperty, value); }
        }        
        public static readonly DependencyProperty DisConnectColorProperty =
            DependencyProperty.Register("DisConnectColor", typeof(Brush), typeof(LMNetStatus),new PropertyMetadata(Brushes.Gray, (d, e) => { 
                var control= d as LMNetStatus;
                if(d != null)
                {
                    control.path.Fill = (Brush)e.NewValue;
                }
            }));


        public Color ConnectColor1
        {
            get { return (Color)GetValue(ConnectColor1Property); }
            set { SetValue(ConnectColor1Property, value); }
        }        
        public static readonly DependencyProperty ConnectColor1Property =
            DependencyProperty.Register("ConnectColor1", typeof(Color), typeof(LMNetStatus), new PropertyMetadata(Colors.LightGreen, (d, e) => { 
                var control = d as LMNetStatus; 
                if(control != null)
                {
                    control.Animate(true);
                }
            }));

        public Color ConnectColor2
        {
            get { return (Color)GetValue(ConnectColor2Property); }
            set { SetValue(ConnectColor2Property, value); }
        }      
        public static readonly DependencyProperty ConnectColor2Property =
            DependencyProperty.Register("ConnectColor2", typeof(Color), typeof(LMNetStatus),new PropertyMetadata(Colors.DarkGreen, (d, e) => {
                var control = d as LMNetStatus;
                if (control != null)
                {
                    control.Animate(true);
                }
            }));


        public Brush StrokeColor
        {
            get { return (Brush)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

       
        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register("StrokeColor", typeof(Brush), typeof(LMNetStatus), new PropertyMetadata(Brushes.Black, (d, e) => { 
                var control=d as LMNetStatus;
                if (control != null)
                {
                    control.path.Stroke = (Brush)e.NewValue;
                }
            }));



        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(LMNetStatus), new PropertyMetadata(1.0, (d, e) => {
                var control = d as LMNetStatus;
                if (control != null)
                {
                    control.path.StrokeThickness = (double)e.NewValue;
                }
            }));




        private Storyboard storyboard=new Storyboard();
        private ColorAnimation colorAnimation=new ColorAnimation();
        private void Animate(bool isCanStart)
        {
            if (isCanStart)
            {
                colorAnimation.From = ConnectColor1;
                colorAnimation.To = ConnectColor2;
                if (!storyboard.Children.Contains(colorAnimation))
                {
                    colorAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    storyboard.Children.Add(colorAnimation);
                    Storyboard.SetTarget(colorAnimation, path);
                    Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Path.Fill).(SolidColorBrush.Color)"));
                }
                storyboard.Begin(this, true);
            }
            else
            {
               storyboard.Stop(this);
            }
        }

    }
}
