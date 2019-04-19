using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.Interfaces
{
    public interface IFileHandler
    {
        Task<string> UploadImageAsync(IFormFile file, string PathToUploadFile, string AdditionalPathsOnTheRoot, FileUploadedType fileUploadedType, CancellationToken cancellationToken, string OldPath = null);
        void CreateImageThumb(string FilePathResizing, string SavePathAfterResize, int newWidth);
        void DeleteOldImageThumb(string ImageThumppath, string Filename);
    }
}