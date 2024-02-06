using Lemonui_wpf.Helper.Gemometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// PathAnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PathAnimationWindow : Window
    {
        public PathAnimationWindow()
        {
            InitializeComponent();
            Rectangle ell=new Rectangle();
            ell.Width = 60;
            ell.Height = 30;
            ell.Fill = Brushes.Red;
            TranslateTransform translateTransform = new TranslateTransform();
            ell.RenderTransform = translateTransform;
            this.container.Children.Add(ell);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 500;
            doubleAnimation.Duration = TimeSpan.FromSeconds(2);
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard sb = new Storyboard();
            Storyboard.SetTarget(doubleAnimation, ell);
            Storyboard.SetTargetProperty(doubleAnimation,new PropertyPath("(Rectangle.RenderTransform).(TranslateTransform.X)"));
            sb.Children.Add(doubleAnimation);
            sb.Begin(this);

           
        }
    }
}
