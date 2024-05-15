using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Lemonui_wpf_test
{
    public class TextConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list= new List<string>() {
                "EQ(==)", "EQT(== +/-)", "NE(!=)", "GT(>)", "LT(<)", "GE(>=)","LE(<=)","GTLT(> <)",
                "GELE(>= <=)", "GELT(>= <)",
                "GTLE(> <=)","LTGT(< >)","LEGE(<= >=)","LEGT(<= >)","LTGE(< >=)","LOG(No Comparison)"};
            string val=value as string;
            if (val != null)
                val =list.FirstOrDefault(p=>p.StartsWith(val));
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            int index = val.IndexOf("(");
            return val.Substring(0,index);
        }
    }
}
