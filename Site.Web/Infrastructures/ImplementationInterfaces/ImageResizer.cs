using Site.Web.Infrastructures.Interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Site.Web.Infrastructures.ImplementationInterfaces
{
    public class ImageResizer : IImageResizer
    {
        public void Resizing(string FilePathResizing, string SavePathAfterResize, int newWidth)
        {
            const long quality = 50L;
            Bitmap source_Bitmap = new Bitmap(FilePathResizing);

            double dblWidth_origial = source_Bitmap.Width;

            double dblHeigth_origial = source_Bitmap.Height;

            double relation_heigth_width = dblHeigth_origial / dblWidth_origial;

            int new_Height = (int)(newWidth * relation_heigth_width);

            //< create Empty Drawarea >

            Bitmap new_DrawArea = new Bitmap(newWidth, new_Height);

            //</ create Empty Drawarea >

            using (var graphic_of_DrawArea = Graphics.FromImage(new_DrawArea))
            {
                //< setup >

                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;

                //</ setup >

                //< draw into placeholder >

                //*imports the image into the drawarea

                graphic_of_DrawArea.DrawImage(source_Bitmap, 0, 0, newWidth, new_Height);

                //</ draw into placeholder >

                //--< Output as .Jpg >--

                using (var output = System.IO.File.Open(SavePathAfterResize, FileMode.Create))
                {
                    //< setup jpg >

                    Encoder qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    EncoderParameters encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    //</ setup jpg >

                    //< save Bitmap as Jpg >

                    ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    new_DrawArea.Save(output, codec, encoderParameters);

                    //resized_Bitmap.Dispose ();

                    output.Close();
                    //</ save Bitmap as Jpg >
                }

                //--</ Output as .Jpg >--

                graphic_of_DrawArea.Dispose();

            }
            source_Bitmap.Dispose();
            //---------------</ Image_resize() >---------------
        }
    }
}
