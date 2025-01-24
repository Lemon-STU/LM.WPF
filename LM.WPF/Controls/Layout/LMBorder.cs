using LM.WPF.Helper.Gemometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LM.WPF.Controls
{
    public class LMBorder:Border
    {
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
        }
    }
}
