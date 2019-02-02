using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.Interfaces;
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

        public async Task<string> UploadImage(IFormFile file,string PathToUploadFile,string OldProfileImagePath)
        {
            string result = await _imageWriter.UploadImage(file,PathToUploadFile,OldProfileImagePath);
            return result;
        }
    }
}
