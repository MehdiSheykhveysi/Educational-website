using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.BusinessObjects;
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
            fileWriter.CreateImageThumb(FilePathResizing, SavePathAfterResize, newWidth);
        }

        public void DeleteOldImageThumb(string ImageThumpPath, string Filename)
        {
            fileWriter.DeleteOldImageThumb(ImageThumpPath, Filename);
        }

        public async Task<FileInformation> GetThumonailFromVideoAsync(string VideoPath, string OutputPath, CancellationToken cancellationToken)
        {
            return await fileWriter.GetThumonailFromVideoAsync(VideoPath, OutputPath, cancellationToken);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string PathToUploadFile, CancellationToken cancellationToken, string OldPath = null)
        {
            return await fileWriter.UploadFileAsync(file, PathToUploadFile, cancellationToken, OldPath);
        }
    }
}
