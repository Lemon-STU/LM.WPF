using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Lemonui_wpf.Controls
{
    public class DropDownButton:ContentControl
    {
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton),new FrameworkPropertyMetadata(typeof(DropDownButton)));
           
        }
        public DropDownButton()
        {
            this.Loaded += DropDownButton_Loaded;
        }



        public bool IsDropDownOpened
        {
            get { return (bool)GetValue(IsDropDownOpenedProperty); }
            set { SetValue(IsDropDownOpenedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpened.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenedProperty =
            DependencyProperty.Register("IsDropDownOpened", typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));


        private void DropDownButton_Loaded(object sender, RoutedEventArgs e)
        {
            var control = DropDownContent as FrameworkElement;
            if(control != null )
            {
                control.PreviewMouseDown += Control_MouseDown;
            }
        }

        private void Control_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsDropDownOpened = false;
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(DropDownButton), new FrameworkPropertyMetadata(new CornerRadius(0.0),FrameworkPropertyMetadataOptions.AffectsRender));




        public object DropDownContent
        {
            get { return (object)GetValue(DropDownContentProperty); }
            set { SetValue(DropDownContentProperty, value); }
        }
        public static readonly DependencyProperty DropDownContentProperty =
            DependencyProperty.Register("DropDownContent", typeof(object), typeof(DropDownButton), new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.AffectsRender));



    }
}
