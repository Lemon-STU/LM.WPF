using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MediaPlayerWindow.xaml
    /// </summary>
    public partial class MediaPlayerWindow : Window
    {
        public MediaPlayerWindow()
        {
            InitializeComponent();
            this.Loaded += MediaPlayerWindow_Loaded;

           // MediaElement
        }

        private void MediaPlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
           // mediaPlayer.Open(new Uri(@"D:\System\Videos\demo.mp4", UriKind.RelativeOrAbsolute));
            this.InvalidateVisual();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Open(new Uri(@"D:\System\Videos\demo.mp4", UriKind.RelativeOrAbsolute));
        }

        MediaPlayer mediaPlayer = new MediaPlayer();
        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);
           // drawingContext.DrawVideo(mediaPlayer, new Rect(0, 0, 200, 100));

            drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Black, 2), new Rect(0, 0,this.ActualWidth, this.ActualHeight));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //player.Position = TimeSpan.Zero;
           // player.Play();
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
           // player.Position = TimeSpan.Zero;
           // player.Play();
        }
    }
}
