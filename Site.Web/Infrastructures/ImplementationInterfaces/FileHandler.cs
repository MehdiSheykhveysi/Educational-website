using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.ImplementationInterfaces
{
    public class FileHandler : IFileHandler
    {
        private readonly IFileWriter fileWriter;
        public FileHandler(IFileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
        }

        public void CreateImageThumb(string FilePathResizing, string SavePathAfterResize, int newWidth)
        {
            fileWriter.CreateImageThumb(FilePathResizing,SavePathAfterResize,newWidth);
        }

        public void DeleteOldImageThumb(string ImageThumppath, string Filename)
        {
            fileWriter.DeleteOldImageThumb(ImageThumppath, Filename);
        }

        public async Task<string> UploadImageAsync(IFormFile file, string PathToUploadFile, string AdditionalPathsOnTheRoot, FileUploadedType fileUploadedType, CancellationToken cancellationToken, string OldPath = null)
        {
            string result = await fileWriter.UploadImageAsync(file, PathToUploadFile, AdditionalPathsOnTheRoot, fileUploadedType, cancellationToken, OldPath);
            return result;
        }
    }
}
