using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Lemonui_wpf.Helper.Image
{
    public class ImageHelper
    {
        public void GetFrames(Uri uri)
        {
            GifBitmapDecoder gifDecodeer= new GifBitmapDecoder(uri,BitmapCreateOptions.PreservePixelFormat,BitmapCacheOption.OnDemand);
            //gifDecodeer.Frames
            //System.Windows.Media.Imaging.
        }

    }
}
