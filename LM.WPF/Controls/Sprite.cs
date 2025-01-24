using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace LM.WPF.Controls
{
    public class Sprite:FrameworkElement
    {
        private static Dictionary<string, ReadOnlyCollection<BitmapFrame>> cachedBitmapFrames=new Dictionary<string, ReadOnlyCollection<BitmapFrame>>();
        private  ReadOnlyCollection<BitmapFrame> bitmapFrames;
        private int cnt = 0;
        private int framecnt = 1;
        private int frameMax=1;
        private bool isRenderOpened = false;
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if ((framecnt++) == frameMax)
            {
                this.InvalidateVisual();
                framecnt = 1;
            }
        }
        [Category("Sprite")]
        public bool AutoStart
        {
            get { return (bool)GetValue(AutoStartProperty); }
            set { SetValue(AutoStartProperty, value); }
        }
        public static readonly DependencyProperty AutoStartProperty =
            DependencyProperty.Register("AutoStart", typeof(bool), typeof(Sprite), new PropertyMetadata((d, e) =>
            {
                bool isAutoStart = (bool)e.NewValue;
                var sprite = d as Sprite;
                RenderOpen(sprite, isAutoStart);
            }));

        [Category("Sprite")]
        public int DesireFrameRate
        {
            get { return (int)GetValue(DesireFrameRateProperty); }
            set { SetValue(DesireFrameRateProperty, value); }
        }
        public static readonly DependencyProperty DesireFrameRateProperty =
            DependencyProperty.Register("DesireFrameRate", typeof(int), typeof(Sprite),new PropertyMetadata(20, (d, e) => { 
                var sprite = d as Sprite;
                if(sprite != null)
                {
                    var desirefps = (int)e.NewValue;
                    sprite.frameMax = 60 / desirefps;
                }            
            }));

        [Category("Sprite")]
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(Sprite), new PropertyMetadata((d, e) => { 
                string uri=e.NewValue as string;
                var sprite=d as Sprite;
                if(uri != null)
                {
                    uri = uri.ToLower();
                    if (!cachedBitmapFrames.ContainsKey(uri))
                    {
                        Stream stream = null;
                        if(File.Exists(uri))
                        {
                            stream = File.OpenRead(uri);
                        }
                        else
                        {
                            var assembly = Assembly.GetEntryAssembly();
                            var resNames = assembly.GetManifestResourceNames();
                            if (resNames.Length > 0)
                            {
                                var resStream = assembly.GetManifestResourceStream(resNames[0]);
                                ResourceSet resourceSet = new ResourceSet(resStream);
                                var iter = resourceSet.GetEnumerator();
                                while (iter.MoveNext())
                                {
                                    var key = iter.Key as string;
                                    if (key != null)
                                    {
                                        if (uri.Contains(key) || key.Contains(uri))
                                        {
                                            stream = iter.Value as UnmanagedMemoryStream;
                                            
                                        }
                                    }
                                }
                            }
                        }
                        if (stream != null)
                        {
                            var encoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                            cachedBitmapFrames.Add(uri.ToString(), encoder.Frames);
                            stream.Close();
                        }
                    }
                    if (cachedBitmapFrames.ContainsKey(uri))
                    {
                        sprite.bitmapFrames = cachedBitmapFrames[uri.ToString()];
                        RenderOpen(sprite, sprite.AutoStart);
                        sprite.InvalidateVisual();
                    }
                }            
            }));




        protected override void OnRender(DrawingContext drawingContext)
        {
            if (bitmapFrames != null && bitmapFrames.Count > 0)
            {
                drawingContext.DrawImage(bitmapFrames[cnt++], new Rect(0, 0, this.ActualWidth, this.ActualHeight));
                if (cnt >= bitmapFrames.Count)
                    cnt = 0;
            }
            else
            {
                if (DesignerProperties.GetIsInDesignMode(this))
                { 
                    drawingContext.DrawRectangle(Brushes.Gray, new Pen(Brushes.Black, 2), new Rect(0, 0, this.ActualWidth, this.ActualHeight));
                    drawingContext.DrawLine(new Pen(Brushes.White, 1), new Point(2, 2), new Point(this.ActualWidth-2, this.ActualHeight-2));
                    drawingContext.DrawLine(new Pen(Brushes.White, 1), new Point(2, this.ActualHeight-2), new Point(this.ActualWidth-2, 2));
                }
            }
        }

        private static void RenderOpen(Sprite sprite,bool isopen)
        {
            if (sprite != null && sprite.bitmapFrames!=null)
            {
                if (sprite.bitmapFrames.Count > 1)
                {
                    if (isopen && !sprite.isRenderOpened)
                    {
                        CompositionTarget.Rendering += sprite.CompositionTarget_Rendering;
                    }
                    else
                    {
                        CompositionTarget.Rendering -= sprite.CompositionTarget_Rendering;
                    }
                }
            }
        }
    }
}
