using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.Interfaces;

namespace Site.Web.Infrastructures.ImplementationInterfaces
{
    public class ImageWriter : IImageWriter
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageWriter(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> UploadImageAsync(IFormFile file, string PathToUploadFile,CancellationToken cancellationToken, string OldProfileImagePath = null)
        {
            if (CheckIfImageFile(file))
            {
                FileUploadHelper objFile = new FileUploadHelper();
                string strFilePath = await objFile.SaveFileAsync(file, PathToUploadFile,cancellationToken, OldProfileImagePath);
                strFilePath = strFilePath
                    .Replace(_hostingEnvironment.WebRootPath + "\\images\\UserProfile\\", string.Empty)
                    .Replace("\\", "/");//Relative Path can be stored in database or do logically what is needed.
                return strFilePath;
            }
            return "index.jpg";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }
    }
}