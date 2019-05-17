using System;
using System.Linq;
using System.Text;

namespace Site.Web.Infrastructures
{
    public class WriterHelper
    {
        public enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            byte[] bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            byte[] gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            byte[] png = new byte[] { 137, 80, 78, 71 };              // PNG
            byte[] tiff = new byte[] { 73, 73, 42 };                  // TIFF
            byte[] tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            byte[] jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            byte[] jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }
    }
}