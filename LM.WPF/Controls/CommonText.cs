using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace LM.WPF.Controls
{
    public class ComparisonConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = new List<string>() {
                "EQ(==)", "EQT(== +/-)", "NE(!=)", "GT(>)", "LT(<)", "GE(>=)","LE(<=)","GTLT(> <)",
                "GELE(>= <=)", "GELT(>= <)",
                "GTLE(> <=)","LTGT(< >)","LEGE(<= >=)","LEGT(<= >)","LTGE(< >=)","LOG(No Comparison)"};
            string val = value as string;
            if (val != null)
                val = list.FirstOrDefault(p => p.StartsWith(val));
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            int index = val.IndexOf("(");
            return val.Substring(0, index);
        }
    }
    public class CommonText:UserControl
    {
        static CommonText()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ComposedControl), new FrameworkPropertyMetadata(typeof(ComposedControl)));
        }

        public static List<string> ComparisonTypes= new List<string>() {
                "EQ(==)", "EQT(== +/-)", "NE(!=)", "GT(>)", "LT(<)", "GE(>=)","LE(<=)","GTLT(> <)",
                "GELE(>= <=)", "GELT(>= <)",
                "GTLE(> <=)","LTGT(< >)","LEGE(<= >=)","LEGT(<= >)","LTGE(< >=)","LOG(No Comparison)"};

        public static List<bool> BoolTypes = new List<bool> { true, false };
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CommonText), new PropertyMetadata("", (d, e) => {
                Console.WriteLine(e.NewValue);
            }));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }       
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CommonText));


        public string ControlType
        {
            get { return (string)GetValue(ControlTypeProperty); }
            set { SetValue(ControlTypeProperty, value); }
        }

        
        public static readonly DependencyProperty ControlTypeProperty =
            DependencyProperty.Register("ControlType", typeof(string), typeof(CommonText), new PropertyMetadata("", (d, e) => { 
                var control=d as CommonText;
                var type=e.NewValue as string;
                if (control != null)
                {
                    control.UpdateControl(type);
                }
            }));


        private void UpdateControl(string type)
        {
            type = type.ToLower();
            switch (type)
            {
                case "combobox":
                    BuildComboBox();
                    break;
                case "textbox":
                    BuildTextBox();
                    break;
                case "textblock":
                    BuildTextBlock();
                    break;
            }
        }

        private void BuildComboBox()
        {
            var comboBox = new ComboBox();
            comboBox.Width = this.Width;
            comboBox.Height=this.Height;
            comboBox.VerticalContentAlignment=VerticalAlignment.Center;
            comboBox.HorizontalContentAlignment = HorizontalAlignment.Left;
            comboBox.ItemsSource = this.ItemsSource;

            Binding binding = new Binding("Text");
            binding.Source =this;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(comboBox, ComboBox.TextProperty, binding);
            this.Content= comboBox;
        }
        private void BuildTextBox()
        {
            var textbox=new TextBox();
            textbox.Foreground = this.Foreground;
            textbox.Background = this.Background;
            textbox.HorizontalContentAlignment = this.HorizontalContentAlignment;
            textbox.VerticalContentAlignment= this.VerticalContentAlignment;
            textbox.HorizontalAlignment = HorizontalAlignment.Stretch;
            textbox.VerticalAlignment = VerticalAlignment.Stretch;
            textbox.Width=this.Width;
            textbox.Height=this.Height;
            Binding binding=new Binding("Text");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(textbox,TextBox.TextProperty, binding);
            this.Content=textbox;
        }

        private void BuildTextBlock()
        {
            var textblock=new TextBlock();
            textblock.Foreground = this.Foreground;
            textblock.Background = Brushes.Transparent;
            textblock.HorizontalAlignment = HorizontalAlignment.Stretch;
            textblock.VerticalAlignment = VerticalAlignment.Stretch;
            Binding binding = new Binding("Text");
            binding.Source = this;
            binding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(textblock, TextBox.TextProperty, binding);
            this.Content = textblock;
            this.Content = textblock;
        }
        public CommonText()
        {
            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
