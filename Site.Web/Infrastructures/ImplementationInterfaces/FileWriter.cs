using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.Interfaces;

namespace Site.Web.Infrastructures.ImplementationInterfaces
{
    public class FileWriter : IFileWriter
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IImageResizer imageResizer;
        private readonly FileUploadHelper fileUploadHelper;

        public FileWriter(IHostingEnvironment HostingEnvironment, IImageResizer ImageResizer)
        {
            this.hostingEnvironment = HostingEnvironment;
            this.imageResizer = ImageResizer;
            fileUploadHelper = new FileUploadHelper();
        }
        /// <summary>
        /// Copy Image In Your specified Path By Using File 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="PathToUploadFile">New Path For Upload File in </param>
        /// <param name="cancellationToken"></param>
        /// <param name="OldPath">Old Path For Delete Old File . Default Id Null</param>
        /// <returns></returns>
        public async Task<string> UploadImageAsync(IFormFile file, string PathToUploadFile, string AdditionalPathsOnTheRoot, FileUploadedType fileUploadedType, CancellationToken cancellationToken, string OldPath = null)
        {
            bool whatResult = fileUploadedType == FileUploadedType.Image ? CheckIfImageFile(file) : true;

            if (whatResult)
            {
                string strFilePath = await fileUploadHelper.SaveFileAsync(file, PathToUploadFile, cancellationToken, OldPath);
                strFilePath = strFilePath
                    .Replace(hostingEnvironment.WebRootPath + AdditionalPathsOnTheRoot, string.Empty)
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

        public void CreateImageThumb(string FilePathResizing, string SavePathAfterResize, int newWidth)
        {
            imageResizer.Resizing(FilePathResizing, SavePathAfterResize, newWidth);
        }

        public void DeleteOldImageThumb(string ImageThumppath, string Filename)
        {
            fileUploadHelper.DeleteOldFile(ImageThumppath, Filename);
        }
    }
}