using Lemonui_wpf.Helper.Gemometry;
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
    /// PathAnimationWindow2.xaml 的交互逻辑
    /// </summary>
    public partial class PathAnimationWindow2 : Window
    {
        public PathAnimationWindow2()
        {
            InitializeComponent();
            this.SizeChanged += PathAnimationWindow2_SizeChanged;
        }

        private void PathAnimationWindow2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var w= this.cvsMain.ActualWidth;
            var h = this.cvsMain.ActualHeight;
            var pw = this.path1.StrokeThickness / 2;
            //sb.Append($"M{pw},{pw} ");
            //sb.Append($"{w - pw},{pw} ");
            //sb.Append($"{w - pw},{h - pw} ");
            //sb.Append($"{pw},{h-pw} ");
            //sb.Append("z");

            var margin = (w - 100) / 2;
            var vmargin = (h) / 2;
            sb.Append($"M0,{vmargin} ");
            sb.Append($"h{margin} ");
            sb.Append($"A50,50 0 1 1 {margin + 100},{vmargin} ");
            sb.Append($"A50,50 0 0 1 {margin},{vmargin} ");
            sb.Append($"A50,50 0 0 1 {margin+100},{vmargin} ");
            sb.Append($"L{margin + 100},{vmargin} ");
            sb.Append($"h{margin} ");

            //sb.Append($"M{pw},{h-pw} ");
            //sb.Append($"{w-pw},{h-pw} ");
            //sb.Append($"{w-pw},{pw} ");
            this.path1.Data=Geometry.Parse(sb.ToString());

            AnimationByPath(cvsMain, path1, path1.StrokeThickness,5);
        }

        private void btnAnimo_Click(object sender, RoutedEventArgs e)
        {
            AnimationByPath(cvsMain, path1, path1.StrokeThickness);
            //AnimationByPath(cvsMain, path1, 20);
        }


        Storyboard story = new Storyboard();
        MatrixTransform matrix = new MatrixTransform();
        TransformGroup groups = new TransformGroup();
        //Polygon target = new Polygon();
        Ellipse target=new Ellipse();
        MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath();
        DoubleAnimation lineAnimation = new DoubleAnimation();
        /// <summary>
        /// 路径动画
        /// </summary>
        /// <param name="cvs">画板</param>
        /// <param name="path">路径</param>
        /// <param name="target">动画对象</param>
        /// <param name="duration">时间</param>
        private void AnimationByPath(Canvas cvs, Path path, double targetWidth, int duration = 5)
        {
            targetWidth = 10;
            #region 创建动画对象                   
            if(cvs.Children.Count ==1 ) {
             //   target.Points = new PointCollection()
             //{
             //    new Point(0,0),
             //    new Point(targetWidth/2,0),
             //    new Point(targetWidth,targetWidth/2),
             //    new Point(targetWidth/2,targetWidth),
             //    new Point(0,targetWidth),
             //    new Point(targetWidth/2,targetWidth/2)
             //};
             
                target.Width = targetWidth;
                target.Height = targetWidth;
                target.Fill = new SolidColorBrush(Colors.Orange);
                cvs.Children.Add(target);
                Canvas.SetLeft(target, -target.Width / 2);
                Canvas.SetTop(target, -target.Height / 2);
                target.RenderTransformOrigin = new Point(0.5, 0.5);

                groups.Children.Add(matrix);
                target.RenderTransform = groups;
                string registname = "matrix" + Guid.NewGuid().ToString().Replace("-", "");
                this.RegisterName(registname, matrix);

                story.Children.Add(matrixAnimation);
                Storyboard.SetTargetName(matrixAnimation, registname);
                Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));

                story.FillBehavior = FillBehavior.Stop;

                matrixAnimation.Duration = new Duration(TimeSpan.FromSeconds(duration));
                matrixAnimation.DoesRotateWithTangent = true;//跟随路径旋转
                matrixAnimation.RepeatBehavior = RepeatBehavior.Forever;//循环

                lineAnimation.Duration= new Duration(TimeSpan.FromSeconds(duration));
                lineAnimation.RepeatBehavior=RepeatBehavior.Forever;
                lineAnimation.To = 0;
                //var lineAnimationName = "line" + Guid.NewGuid().ToString().Replace("-","");
                Storyboard.SetTargetName(lineAnimation,"path1");
                Storyboard.SetTargetProperty(lineAnimation,new PropertyPath(Path.StrokeDashOffsetProperty));

                story.Children.Add(lineAnimation);
            }
            #endregion

            var len = path.Data.GetLength()/path.StrokeThickness;
            path.StrokeDashArray = new DoubleCollection {len};
            lineAnimation.From = len;
            //path.StrokeDashOffset =len ;
            matrixAnimation.PathGeometry = PathGeometry.CreateFromGeometry(Geometry.Parse(path.Data.ToString()));                                 
            story.Begin(this, true);       
        }
    }
}
