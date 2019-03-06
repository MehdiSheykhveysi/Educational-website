using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.Interfaces
{
    public interface IImageHandler
    {
        Task<string> UploadImageAsync(IFormFile file,string PathToUploadFile,CancellationToken cancellationToken,string OldProfileImagePath = null);
    }
}