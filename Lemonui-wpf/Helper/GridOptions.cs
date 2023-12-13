using System;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lemonui_wpf.Helper
{
    public class GridOptions
    {
        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(GridOptions), new PropertyMetadata(new CornerRadius(), (d, e) => {
                var grid = d as Grid;
                var CornerRadius = (CornerRadius)e.NewValue;
                var childs = grid.Children;
                var linewidth = GetLineThickness(grid);
                var borderThickness = new Thickness(0, 0, linewidth, linewidth);
                var pading=GetPadding(grid);

                foreach (Border child in childs)
                {
                    child.CornerRadius = CornerRadius;                    
                    var border = borderThickness;
                    if (CornerRadius.TopLeft != 0 || CornerRadius.BottomLeft != 0 || pading.Left!=0 || pading.Top!=0)
                    {
                        border.Top = linewidth;
                        border.Left = linewidth;
                        child.BorderThickness = border;
                    }
                    else
                    {
                        if (Grid.GetRow(child) == 0)
                            border.Top = linewidth;
                        if (Grid.GetColumn(child) == 0)
                            border.Left = linewidth;
                        child.BorderThickness = border;
                    }
                }
            }));



        public static Thickness GetPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PaddingProperty);
        }

        public static void SetPadding(DependencyObject obj,Thickness value)
        {
            obj.SetValue(PaddingProperty, value);
        }
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.RegisterAttached("Padding", typeof(Thickness), typeof(GridOptions), new PropertyMetadata(new Thickness(0), (d, e) => {
                var grid = d as Grid;
                var pading = (Thickness)e.NewValue;
                var childs = grid.Children;
                var corner = GetCornerRadius(d);
                var linewidth = GetLineThickness(grid);
                var borderThickness = new Thickness(0, 0, linewidth, linewidth);

                
                foreach (Border child in childs)
                {
                    child.Margin = pading;
                    var border = borderThickness;
                    if (corner.TopLeft != 0 || corner.BottomLeft != 0 || pading.Left!=0 || pading.Right!=0)
                    {
                        border.Top = linewidth;
                        border.Left = linewidth;
                        child.BorderThickness = border;
                    }
                    else
                    {
                        if (Grid.GetRow(child) == 0)
                            border.Top = linewidth;
                        if (Grid.GetColumn(child) == 0)
                            border.Left = linewidth;
                        child.BorderThickness = border;

                    }
                }
            }));



        #region 显示边框信息
        public static readonly DependencyProperty ShowBorderProperty =
            DependencyProperty.RegisterAttached("ShowBorder", typeof(bool), typeof(GridOptions)
                , new PropertyMetadata(false,OnShowBorderChanged));
        public static bool GetShowBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowBorderProperty);
        }

        public static void SetShowBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowBorderProperty, value);
        }

        public static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            if (!grid.IsLoaded)
                grid.Loaded += new RoutedEventHandler(GridLoaded);
            if ((bool)e.NewValue)
            {
                GridBorderShowHide(grid, true);
            }
            else
            {
                GridBorderShowHide(grid, false);
            }
        }
        #endregion
        #region 线宽信息
        public static readonly DependencyProperty LineThicknessProperty =
           DependencyProperty.RegisterAttached("LineThickness", typeof(double), typeof(GridOptions),
               new PropertyMetadata(1d, OnGridLineThicknessChanged));
        public static double GetLineThickness(DependencyObject obj)
        {
            return (double)obj.GetValue(LineThicknessProperty);
        }

        public static void SetLineThickness(DependencyObject obj, double value)
        {
            obj.SetValue(LineThicknessProperty, value);
        }


        public static void OnGridLineThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            var linethickness= (double)e.NewValue;
            var childs = grid.Children;
            foreach (Border child in childs )
            {
                var oldborder = child.BorderThickness;
                var newborder = new Thickness(linethickness);
                if(oldborder.Left==0)
                    newborder.Left = 0;
                if(oldborder.Top==0)
                    newborder.Top = 0;
                if(oldborder.Right==0)
                    newborder.Right = 0;
                if(oldborder.Bottom==0)
                    newborder.Bottom = 0;
                child.BorderThickness=newborder;
            }
        }
        #endregion

        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.RegisterAttached("LineBrush", typeof(Brush), typeof(GridOptions),
                new PropertyMetadata(Brushes.Gray, OnGridLineBrushChanged));

        #region 线画刷问题
        public static Brush GetLineBrush(DependencyObject obj)
        {
            var brush = (Brush)obj.GetValue(LineBrushProperty);
            return brush ?? Brushes.LightGray;
        }

        public static void SetLineBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(LineBrushProperty, value);
        }

        public static void OnGridLineBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            var borderBrush = (Brush)e.NewValue;
            var childs = grid.Children;
            foreach (Border child in childs)
            {
                child.BorderBrush =borderBrush;
            }
        }
        #endregion

        private static void GridLoaded(object sender, RoutedEventArgs e)
        {
            #region 思路
            /*
             * 1、覆盖所有单元格都要包围上边框。
             * 2、边框线不能存在重复。每个单元格绘制【右下】部分，主体绘制右上部分
             */
            #endregion
            var grid = sender as Grid;
            var rowCount = Math.Max(1, grid.RowDefinitions.Count);
            var columnCount = Math.Max(1, grid.ColumnDefinitions.Count);
            #region 初始化标准数组
            int[,] flagArray = new int[rowCount, columnCount];
            #endregion
            var controls = grid.Children;
            var count = controls.Count;
            var settingThickness = GetLineThickness(grid);
            for (int i = 0; i < count; i++)
            {
                var item = controls[i] as FrameworkElement;
                var row = Grid.GetRow((item));
                var column = Grid.GetColumn(item);
                var rowSpan = Grid.GetRowSpan(item);
                var columnSpan = Grid.GetColumnSpan(item);
                for (int rowTemp = 0; rowTemp < rowSpan; rowTemp++)
                {
                    for (int colTemp = 0; colTemp < columnSpan; colTemp++)
                    {
                        flagArray[rowTemp + row, colTemp + column] = 1;
                    }
                }

                var border = CreateBorder(grid,row, column, rowSpan, columnSpan, settingThickness);

                grid.Children.RemoveAt(i);
                border.Child = item;
                grid.Children.Insert(i, border);
            }

            #region 整理为填充单元格
            for (int i = 0; i < rowCount; i++)
            {
                for (int k = 0; k < columnCount; k++)
                {
                    if (flagArray[i, k] == 0)
                    {
                        var border = CreateBorder(grid,i, k, 1, 1, settingThickness);
                        grid.Children.Add(border);
                    }
                }
            }
            #endregion
        }

        private static Border CreateBorder(Grid grid,int row, int column, int rowSpan, int columnSpan, double thickness)
        {
            var padding= GetPadding(grid);
            var cornerRadius = GetCornerRadius(grid);
            var useThickness = new Thickness(0, 0, thickness, thickness);
            if(cornerRadius.TopLeft != 0 || cornerRadius.BottomLeft != 0 || padding.Left!=0 || padding.Top!=0)
                useThickness = new Thickness(thickness);

            if (row == 0)
                useThickness.Top = thickness;
            if (column == 0)
                useThickness.Left = thickness;
    
            var borderBrush = GetLineBrush(grid);
            var border = new Border()
            {
                BorderThickness = useThickness,
                BorderBrush = borderBrush,
                CornerRadius=cornerRadius,
                Margin=padding,
                ClipToBounds=true
            };
            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            Grid.SetRowSpan(border, rowSpan);
            Grid.SetColumnSpan(border, columnSpan);
            return border;
        }

        private static void GridBorderShowHide(Grid grid,bool show)
        {
            var childs=grid.Children;
            foreach (Border item in childs)
            {
                if (!show)
                    item.BorderThickness = new Thickness(0);
                else
                {
                    var thickness = GetLineThickness(grid);
                    item.BorderThickness = new Thickness(thickness);
                }
            }
        }
    }

}
