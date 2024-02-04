using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lemonui_wpf.Common
{
    public class RenderHelper
    {
        public FormattedText BuildFormattedText(string text,Brush foreGround,double fontSize,string fontFamily)
        {
            var typeface = new Typeface(fontFamily);
            var formattedText=new FormattedText(text,CultureInfo.CurrentCulture,FlowDirection.LeftToRight,typeface,fontSize,foreGround,96);
            return formattedText;
        }

        public FormattedText BuildFormattedText(string text,Brush foreGround,double fontSize)
        {
            return BuildFormattedText(text, foreGround, fontSize, "Arial");
        }

        public FormattedText BuildFormattedText(string text)
        {
            return BuildFormattedText(text, Brushes.Black, 12);
        }


        
    }
}
