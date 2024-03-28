using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Uri uri = new Uri("Resources/demo1.jpg", UriKind.RelativeOrAbsolute);//这个就是所以的pack uri。
            //StreamResourceInfo info = Application.GetResourceStream(uri);

            //var uri = new Uri("pack://application:,,,/Resources/demo1.jpg", UriKind.RelativeOrAbsolute);
            //var assembly = Assembly.GetEntryAssembly();
            //var names = assembly.GetManifestResourceNames();
            //var info = assembly.GetManifestResourceInfo(names[0]);
            //var stream = assembly.GetManifestResourceStream(names[0]);
            //ResourceSet resourceSet = new ResourceSet(stream);
            
            //var iter=resourceSet.GetEnumerator();
            //while(iter.MoveNext())
            //{
            //    var key=iter.Key;
            //    var obj=iter.Value;
            //    Console.WriteLine($"{key}");
            //}

            //var mystream= resourceSet.GetObject("resources/demo12.jpg") as UnmanagedMemoryStream;
            base.OnStartup(e);
        }
    }
}
