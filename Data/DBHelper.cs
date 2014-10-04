using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DataAccess
{
    public class DBHelper
    {
        public static byte[] ImageToByteArray ( Image imageIn )
        {
            MemoryStream ms = new MemoryStream ();
            imageIn.Save (ms, ImageFormat.Png);
            return ms.ToArray ();
        }

        public static Image ByteArrayToImage ( byte[] byteArrayIn )
        {
            MemoryStream ms = new MemoryStream (byteArrayIn);
            Image returnImage = Image.FromStream (ms);
            return returnImage;

        }

        

    }
}

//public static BitmapImage byteArrayToImage ( byte[] byteArrayIn )
//{
//    if(byteArrayIn == null || byteArrayIn.Length == 0) return null;
//    var image = new BitmapImage ();
//    using(var mem = new MemoryStream (byteArrayIn))
//    {
//        mem.Position = 0;
//        image.BeginInit ();
//        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
//        image.CacheOption = BitmapCacheOption.OnLoad;
//        image.UriSource = null;
//        image.StreamSource = mem;
//        image.EndInit ();
//    }
//    image.Freeze ();
//    return image;
//}     //Bin To Bitmap

//public static byte[] ImageToByte ( Image img )
//        {
//            ImageConverter converter = new ImageConverter ();
//            return (byte[])converter.ConvertTo (img, typeof (byte[]));
//        }