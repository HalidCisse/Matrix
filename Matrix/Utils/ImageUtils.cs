using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Matrix.Utils
{

    

    class ImageUtils
    {

        


        #region Image To Bytes

        public static byte[] getPNGFromImageControl ( BitmapImage ImageControl )
        {
            var memStream = new MemoryStream ();
            var encoder = new PngBitmapEncoder ();
            encoder.Frames.Add (BitmapFrame.Create (ImageControl));
            encoder.Save (memStream);
            return memStream.GetBuffer ();
        }

        public Byte[] BitmapToByte2 ( BitmapImage bitmapImage )
        {

            byte[] data;
            var encoder = new JpegBitmapEncoder ();
            encoder.Frames.Add (BitmapFrame.Create (bitmapImage));
            using(var ms = new MemoryStream ())
            {
                encoder.Save (ms);
                data = ms.ToArray ();
            }
            return data;
        }

        public Byte[] BitmapToByte ( BitmapImage Bitmap )
        {
            var stream = Bitmap.StreamSource;
            Byte[] buffer;
            if (stream == null || stream.Length <= 0) return null;
            using(var br = new BinaryReader (stream))
            {
                buffer = br.ReadBytes ((Int32)stream.Length);
            }
            return buffer;
        }

        public static byte[] BitmapToByte ( string ImagePath )
        {
            FileStream fs = new FileStream (ImagePath, FileMode.Open, FileAccess.Read);
            byte[] imgBytes = new byte[fs.Length];
            fs.Read (imgBytes, 0, Convert.ToInt32 (fs.Length));
            string encodeData = Convert.ToBase64String (imgBytes, Base64FormattingOptions.InsertLineBreaks);
            return new[] { Byte.Parse (encodeData) };
        }

        private static byte[] BitmapArrayFromFile ( string ImageFilePath )
        {
            if(!File.Exists (ImageFilePath)) return null;

            var fs = new FileStream (ImageFilePath, FileMode.Open, FileAccess.Read);
            var imgByteArr = new byte[fs.Length];
            fs.Read (imgByteArr, 0, Convert.ToInt32 (fs.Length));
            fs.Close ();
            return imgByteArr;
        }

        public Byte[] BitmapToByte1 ( BitmapImage Bitmap )
        {
            var stream = Bitmap.StreamSource;
            Byte[] buffer;
            if(stream == null || stream.Length <= 0) return null;
            using(var br = new BinaryReader (stream))
            {
                buffer = br.ReadBytes ((Int32)stream.Length);
            }
            return buffer;
        }


        #endregion




        #region Bytes To Image

        public static BitmapImage DecodePhoto ( byte[] byteVal )
        {
            if(byteVal == null) return null;

            try
            {
                var strmImg = new MemoryStream (byteVal);
                var myBitmapImage = new BitmapImage ();
                myBitmapImage.BeginInit ();
                myBitmapImage.StreamSource = strmImg;
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit ();
                return myBitmapImage;
            }
            catch(Exception ex)
            {
                MessageBox.Show (ex.Message);
            }

            return null;
        }
        

        public static BitmapImage GetBitmapFromFile ( string path )
        {
            var img = new BitmapImage ();
            img.BeginInit ();
            img.UriSource = new Uri (path, UriKind.RelativeOrAbsolute);
            img.EndInit ();
            return img;
        }


        #endregion



    }
}
