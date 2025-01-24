using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LM.WPF.Common
{
    public static class DrawingContextExtension
    {
        public static void PushTransformThenDraw(this DrawingContext dc,Transform transform,Action<DrawingContext> action)
        {
            try
            {
                dc.PushTransform(transform);
                action?.Invoke(dc);
            }
            finally
            {
                dc.Pop();
            }
        }
        public static void PushClipThenDraw(this DrawingContext dc, Geometry clipGeometry, Action<DrawingContext> action)
        {
            try
            {
                dc.PushClip(clipGeometry);
                action?.Invoke(dc);
            }
            finally
            {
                dc.Pop();
            }
        }
        public static void PushOpacityThenDraw(this DrawingContext dc, double opacity, Action<DrawingContext> action)
        {
            try
            {
                dc.PushOpacity(opacity);
                action?.Invoke(dc);
            }
            finally
            {
                dc.Pop();
            }
        }

        public static void PushOpacityMaskThenDraw(this DrawingContext dc, Brush opacityMask, Action<DrawingContext> action)
        {
            try
            {
                dc.PushOpacityMask(opacityMask);
                action?.Invoke(dc);
            }
            finally
            {
                dc.Pop();
            }
        }
    }
}
