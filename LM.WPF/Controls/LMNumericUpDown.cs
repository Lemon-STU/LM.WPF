using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LM.WPF.Controls
{
    public class LMNumericUpDown:Slider
    {
        static LMNumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LMNumericUpDown), new FrameworkPropertyMetadata(typeof(LMNumericUpDown)));
        }
    }
}
