using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lemonui_wpf.Helper
{
    public class TextBoxOption
    {
        public static double GetMaskOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(MaskOpacityProperty);
        }

        public static void SetMaskOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(MaskOpacityProperty, value);
        }
        public static readonly DependencyProperty MaskOpacityProperty =
            DependencyProperty.RegisterAttached("MaskOpacity", typeof(double), typeof(TextBoxOption), new PropertyMetadata(1.0, (d, e) => {
                var textbox = d as TextBox;
                if(d!=null)
                {
                    var backBrush= textbox.Background as VisualBrush;
                    if(backBrush!=null)
                    {
                        var textblock= backBrush.Visual as TextBlock;
                        if(textblock!=null)
                        {
                            var opacity = (double)e.NewValue;
                            textblock.Opacity = opacity;
                        }
                    }
                }
            }));



        public static Brush GetMaskColor(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MaskColorProperty);
        }

        public static void SetMaskColor(DependencyObject obj, Brush value)
        {
            obj.SetValue(MaskColorProperty, value);
        }
        public static readonly DependencyProperty MaskColorProperty =
            DependencyProperty.RegisterAttached("MaskColor", typeof(Brush), typeof(TextBoxOption), new PropertyMetadata(Brushes.Black, (d, e) => {
                var textbox = d as TextBox;
                if(textbox!=null)
                {
                    var brushes = textbox.Background as VisualBrush;
                    if(brushes!=null)
                    {
                        var textblock= brushes.Visual as TextBlock;
                        if (textblock!=null)
                        {
                            var clolor = (Brush)e.NewValue;
                            textblock.Foreground = clolor;
                        }
                    }
                }
            }));


        public static string GetMaskText(DependencyObject obj)
        {
            return (string)obj.GetValue(MaskTextProperty);
        }

        public static void SetMaskText(DependencyObject obj, string value)
        {
            obj.SetValue(MaskTextProperty, value);
        }

        public static readonly DependencyProperty MaskTextProperty =
            DependencyProperty.RegisterAttached("MaskText", typeof(string), typeof(TextBoxOption), new PropertyMetadata("", (d, e) => {
                var textbox = d as TextBox;
                if(textbox != null)
                {
                    string masktext=e.NewValue as string;
                    if(!textbox.IsLoaded)
                    {
                        textbox.TextChanged += delegate {SetMaskStyle(textbox, masktext);};
                    }
                    SetMaskStyle(textbox, masktext);
                }            
            }));

        private static void SetMaskStyle(TextBox textbox,string masktext)
        {
            TextBlock maskTextblock = new TextBlock();
            maskTextblock.Text = GetMaskText(textbox);
            maskTextblock.Foreground = GetMaskColor(textbox);
            maskTextblock.Opacity = GetMaskOpacity(textbox);
            maskTextblock.FontSize=textbox.FontSize;
            maskTextblock.FontFamily=textbox.FontFamily;
            maskTextblock.VerticalAlignment = textbox.VerticalContentAlignment;
            VisualBrush backBrush = new VisualBrush();
            backBrush.Visual = maskTextblock;
            backBrush.AlignmentX = AlignmentX.Left;
            backBrush.Stretch = Stretch.None;
         
            if (textbox != null)
            {
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    textbox.Background = backBrush;
                }
                else
                {
                    textbox.Background = Brushes.Transparent;
                }
            }
        }
    }
}
