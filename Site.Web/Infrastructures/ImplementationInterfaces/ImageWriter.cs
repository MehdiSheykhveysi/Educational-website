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
        /// <summary>
        /// Copy Image In Your specified Path By Using File 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="PathToUploadFile">New Path For Upload File in </param>
        /// <param name="cancellationToken"></param>
        /// <param name="OldProfileImagePath">Old Path For Delete Old File . Default Id Null</param>
        /// <returns></returns>
        public async Task<string> UploadImageAsync(IFormFile file, string PathToUploadFile, CancellationToken cancellationToken, string OldProfileImagePath = null)
        {
            if (CheckIfImageFile(file))
            {
                FileUploadHelper objFile = new FileUploadHelper();
                string strFilePath = await objFile.SaveFileAsync(file, PathToUploadFile, cancellationToken, OldProfileImagePath);
                strFilePath = strFilePath
                    .Replace(_hostingEnvironment.WebRootPath + "\\images\\UserProfile\\", string.Empty)
                    .Replace("\\", "/");//Relative Path can be stored in database or do logically what is needed.
                return strFilePath;
            }
            return "index.png";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            if (file != null)
            {
                byte[] fileBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
            }
            return false;
        }
    }
}