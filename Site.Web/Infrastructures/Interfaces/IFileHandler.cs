using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.BusinessObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.Interfaces
{
    public interface IFileHandler
    {
        Task<string> UploadFileAsync(IFormFile file, string PathToUploadFile, CancellationToken cancellationToken, string OldPath = null);
        void CreateImageThumb(string FilePathResizing, string SavePathAfterResize, int newWidth);
        void DeleteOldImageThumb(string ImageThumpPath, string Filename);
        Task<FileInformation> GetThumonailFromVideoAsync(string VideoPath, string OutputPath, CancellationToken cancellationToken);
    }
}