using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace LM.WPF.Helper.Gemometry
{
    internal class GeometryHelper
    {
        public static StreamGeometry GenerateBorderGeometry(FrameworkElement element, CornerRadius CornerRadius)
        {
            var backgroundGeometry = new StreamGeometry();
            using (StreamGeometryContext ctx = backgroundGeometry.Open())
            {
                var radii = CornerRadius;
                var rect = new Rect(0, 0, element.ActualWidth, element.ActualHeight);
                //  compute the coordinates of the key points
                Point topLeft = new Point(radii.TopLeft, 0);
                Point topRight = new Point(rect.Width - radii.TopRight, 0);
                Point rightTop = new Point(rect.Width, radii.TopRight);
                Point rightBottom = new Point(rect.Width, rect.Height - radii.BottomRight);
                Point bottomRight = new Point(rect.Width - radii.BottomRight, rect.Height);
                Point bottomLeft = new Point(radii.BottomLeft, rect.Height);
                Point leftBottom = new Point(0, rect.Height - radii.BottomLeft);
                Point leftTop = new Point(0, radii.TopLeft);
                //  check keypoints for overlap and resolve by partitioning radii according to
                //  the percentage of each one.  
                //
                //  top edge is handled here
                if (topLeft.X > topRight.X)
                {
                    double v = (radii.TopLeft) / (radii.TopLeft + radii.TopRight) * rect.Width;
                    topLeft.X = v;
                    topRight.X = v;
                }
                //  right edge
                if (rightTop.Y > rightBottom.Y)
                {
                    double v = (radii.TopRight) / (radii.TopRight + radii.BottomRight) * rect.Height;
                    rightTop.Y = v;
                    rightBottom.Y = v;
                }
                //  bottom edge
                if (bottomRight.X < bottomLeft.X)
                {
                    double v = (radii.BottomLeft) / (radii.BottomLeft + radii.BottomRight) * rect.Width;
                    bottomRight.X = v;
                    bottomLeft.X = v;
                }
                // left edge
                if (leftBottom.Y < leftTop.Y)
                {
                    double v = (radii.TopLeft) / (radii.TopLeft + radii.BottomLeft) * rect.Height;
                    leftBottom.Y = v;
                    leftTop.Y = v;
                }
                //  add on offsets
                Vector offset = new Vector(rect.TopLeft.X, rect.TopLeft.Y);
                topLeft += offset;
                topRight += offset;
                rightTop += offset;
                rightBottom += offset;
                bottomRight += offset;
                bottomLeft += offset;
                leftBottom += offset;
                leftTop += offset;
                //  create the border geometry
                ctx.BeginFigure(topLeft, true /* is filled */, true /* is closed */);
                // Top line
                ctx.LineTo(topRight, true /* is stroked */, false /* is smooth join */);
                // Upper-right corner
                double radiusX = rect.TopRight.X - topRight.X;
                double radiusY = rightTop.Y - rect.TopRight.Y;
                if (radiusX != 0 || radiusY != 0)
                {
                    ctx.ArcTo(rightTop, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
                }
                // Right line
                ctx.LineTo(rightBottom, true /* is stroked */, false /* is smooth join */);
                // Lower-right corner
                radiusX = rect.BottomRight.X - bottomRight.X;
                radiusY = rect.BottomRight.Y - rightBottom.Y;
                if (radiusX != 0 || radiusY != 0)
                {
                    ctx.ArcTo(bottomRight, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
                }
                // Bottom line
                ctx.LineTo(bottomLeft, true /* is stroked */, false /* is smooth join */);
                // Lower-left corner
                radiusX = bottomLeft.X - rect.BottomLeft.X;
                radiusY = rect.BottomLeft.Y - leftBottom.Y;
                if (radiusX != 0 || radiusY != 0)
                {
                    ctx.ArcTo(leftBottom, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
                }
                // Left line
                ctx.LineTo(leftTop, true /* is stroked */, false /* is smooth join */);
                // Upper-left corner
                radiusX = topLeft.X - rect.TopLeft.X;
                radiusY = leftTop.Y - rect.TopLeft.Y;
                if (radiusX != 0 || radiusY != 0)
                {
                    ctx.ArcTo(topLeft, new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true, false);
                }
                return backgroundGeometry;
            }
        }
    }
}
