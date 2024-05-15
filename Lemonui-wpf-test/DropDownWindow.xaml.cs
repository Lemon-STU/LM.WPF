using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemonui_wpf_test
{
    /// <summary>
    /// Interaction logic for DropDownWindow.xaml
    /// </summary>
    public partial class DropDownWindow : Window
    {
        public DropDownWindow()
        {
            InitializeComponent();
        }

        private void log(string tag = "tag1",string msg="")
        {
            string logmsg = $"Thread:[{Thread.CurrentThread.ManagedThreadId}] [{DateTime.Now.ToString("HH::mm::ss,fff")}] [{tag}]:{msg}...";
            Console.WriteLine(logmsg);
            this.Title = logmsg;
        }
        public async void test(string tag="tag1")
        {
            log(tag,$"entry the maintest,delay 5s...");
            await Task.Delay(5000);

            log(tag,"after the delay....");

             anothertest(tag);

            log(tag,"exit the maintest....");
        }


        public async void anothertest(string tag)
        {
            log(tag, "entry anothertest,delay 3s....");
            await Task.Delay(3000);
            log(tag, "exit anothertest....");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
               // test(btn.Content.ToString());
            }

            Task.Run(async () => {
                await Task.Delay(1000);
                
                if(this.CheckAccess())
                {
                    this.Title = "test in task....";
                    await Console.Out.WriteLineAsync("after modify the title");
                }
                else
                {
                    await Console.Out.WriteLineAsync("Not the UI thread");
                    await Console.Out.WriteLineAsync($"Thread:{Thread.CurrentThread.ManagedThreadId}");
                }
            });
        }
    }
}
