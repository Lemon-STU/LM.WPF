using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Lemonui_wpf_test
{
    public class ConnectStatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            Brush brush = Brushes.Green;
            if (str != null)
            {
                if (str.Contains("disconnect"))
                    brush = Brushes.Red;
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
