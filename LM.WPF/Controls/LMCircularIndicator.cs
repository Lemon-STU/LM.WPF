﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LM.WPF.Controls
{
    public class LMCircularIndicator:Control
    {

        static LMCircularIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LMCircularIndicator), new FrameworkPropertyMetadata(typeof(LMCircularIndicator)));
           
        }

        public LMCircularIndicator()
        {
           
        }



        public string IndicatorText
        {
            get { return (string)GetValue(IndicatorTextProperty); }
            set { SetValue(IndicatorTextProperty, value); }
        }

        public static readonly DependencyProperty IndicatorTextProperty =
            DependencyProperty.Register("IndicatorText", typeof(string), typeof(LMCircularIndicator), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender));


        public double IndicatorValue
        {
            get { return (double)GetValue(IndicatorValueProperty); }
            set { SetValue(IndicatorValueProperty, value); }
        }
        public static readonly DependencyProperty IndicatorValueProperty =
            DependencyProperty.Register("IndicatorValue", typeof(double), typeof(LMCircularIndicator), new FrameworkPropertyMetadata(0.0,FrameworkPropertyMetadataOptions.AffectsRender));


    }
}
