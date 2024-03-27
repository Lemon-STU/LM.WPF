using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var uri = new Uri("pack://application:,,,/Resources/demo1.jpg", UriKind.RelativeOrAbsolute);
            var bitmapImage= new BitmapImage(uri); 
            var path = uri.ToString();
            base.OnStartup(e);
        }
    }
}
