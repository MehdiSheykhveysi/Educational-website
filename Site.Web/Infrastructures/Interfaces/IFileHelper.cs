using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.BusinessObjects;

namespace Site.Web.Infrastructures.Interfaces
{
    public interface IFileHelper
    {
        bool CheckIfImageFile(IFormFile file);
        void DeleteOldFile(string OldProfileImageFile, string FileName);
        string GetFileExtension(IFormFile file);
        string GetFileNameFromPath(string Path);
        Task<FileInformation> GetThumonailFromVideoAsync(string VideoPath, string OutputPath, CancellationToken cancellationToken);
        string GetUniqueName(string preFix);
        string SaveFile(IFormFile file, string pathToUplaod, string OldProfileImagePath);
        Task<string> SaveFileAsync(IFormFile file, string pathToUplaod, CancellationToken cancellationToken, string OldProfileImagePath = null);
        string SetFileName(IFormFile file);
    }
}