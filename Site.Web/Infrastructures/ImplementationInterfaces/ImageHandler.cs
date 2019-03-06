using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.ImplementationInterfaces
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string PathToUploadFile,CancellationToken cancellationToken, string OldProfileImagePath = null)
        {
            string result = await _imageWriter.UploadImageAsync(file, PathToUploadFile, cancellationToken, OldProfileImagePath);
            return result;
        }
    }
}
