using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace LM.WPF.Helper.Gemometry
{
    public static class GeometryExtension
    {
        /// <summary>
        /// get geometry's length,StokeThickness may affect the ressult
        /// </summary>
        /// <param name="geo"></param>
        /// <returns></returns>
        public static double GetLength(this Geometry geo)
        {
            PathGeometry path = geo.GetFlattenedPathGeometry();
            double length = 0.0;
            foreach (PathFigure pf in path.Figures)
            {
                Point start = pf.StartPoint;


                foreach (var seg in pf.Segments)
                {
                    if (seg is LineSegment lineSegment)
                    {
                        length += Distance(start, lineSegment.Point);
                        start = lineSegment.Point;
                    }
                    else if (seg is PolyLineSegment polyLineSegment)
                    {
                        foreach (Point point in polyLineSegment.Points)
                        {
                            length += Distance(start, point);
                            start = point;
                        }
                    }
                }
            }
            return length;
        }

        private static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
