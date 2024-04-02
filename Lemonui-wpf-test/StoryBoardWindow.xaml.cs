using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for StoryBoardWindow.xaml
    /// </summary>
    public partial class StoryBoardWindow : Window
    {
        public StoryBoardWindow()
        {
            InitializeComponent();

            this.Loaded += StoryBoardWindow_Loaded;
        }
        Storyboard storyboard = null;
        private void StoryBoardWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if(storyboard==null)
            {
                storyboard = new Storyboard();
            }
           
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(1000);
            doubleAnimation.From = 50;
            doubleAnimation.To = 500;
            storyboard.Children.Add(doubleAnimation);

            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                Button.RenderTransformProperty,
                TransformGroup.ChildrenProperty,
                TranslateTransform.XProperty
            };
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(doubleAnimation, btn);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(0).(1)[0].(2)", propertyChain));
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn.Content.Equals("begin"))
            {
                storyboard.Begin();
                btn.Content = "stop";
            }
            else
            {
                storyboard.Stop();
                btn.Content = "begin";
            }
        }
    }
}
