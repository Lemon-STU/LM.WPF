﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace LM.WPF.Converter
{
    public class IndentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double colunwidth = 10;
            double left = 0.0;

            UIElement element = value as TreeViewItem;
            if (element != null)
            {
                while (element.GetType() != typeof(TreeView))
                {
                    element = (UIElement)VisualTreeHelper.GetParent(element);
                    if (element.GetType() == typeof(TreeViewItem))
                        left += colunwidth;
                }
            }

            return new Thickness(left, 0, 20, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
