using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FFmpeg.NET;
using Microsoft.AspNetCore.Http;
using Site.Core.Infrastructures.Utilities;
using Site.Web.Infrastructures.BusinessObjects;
using Site.Web.Infrastructures.Interfaces;

namespace Site.Web.Infrastructures.ImplementationInterfaces
{
    public class FileHelper : IFileHelper
    {
        public async Task<string> SaveFileAsync(IFormFile file, string pathToUplaod, CancellationToken cancellationToken, string OldProfileImagePath = null)
        {
            string imageUrl = string.Empty;
            if (!Directory.Exists(pathToUplaod))
                Directory.CreateDirectory(pathToUplaod);//Create Path of not exists

            string pathwithfileName = pathToUplaod + "\\" + SetFileName(file);
            using (FileStream fileStream = new FileStream(pathwithfileName, FileMode.Create))
            {
                if (Assert.NotNull(OldProfileImagePath))
                {
                    string OldFileName = GetFileNameFromPath(OldProfileImagePath);
                    DeleteOldFile(OldProfileImagePath, OldFileName);
                }

                string FileName = GetFileNameFromPath(pathwithfileName);
                if (FileName != "index.png")
                {
                    await file.CopyToAsync(fileStream, cancellationToken);
                }
            }
            imageUrl = pathwithfileName;
            return imageUrl;
        }
        public string SaveFile(IFormFile file, string pathToUplaod, string OldProfileImagePath)
        {
            string imageUrl = string.Empty;
            string pathwithfileName = pathToUplaod + "\\" + SetFileName(file);
            using (FileStream fileStream = new FileStream(pathwithfileName, FileMode.Create))
            {
                if (Assert.NotNull(OldProfileImagePath))
                {
                    string OldFileName = GetFileNameFromPath(OldProfileImagePath);
                    DeleteOldFile(OldProfileImagePath, OldFileName);
                }

                string FileName = GetFileNameFromPath(pathwithfileName);
                if (FileName != "index.png")
                {
                    file.CopyTo(fileStream);
                }
            }
            imageUrl = pathwithfileName;
            return imageUrl;
        }
        public string SetFileName(IFormFile file)
        {
            string fileName = string.Empty;
            string fileExtension = GetFileExtension(file);
            string strUniqName = GetUniqueName("img");
            if (fileExtension == string.Empty)
            {
                fileName = "index.png";
            }
            else
            {
                fileName = strUniqName + fileExtension;
            }
            return fileName;
        }
        public string GetUniqueName(string preFix)
        {
            string uName = preFix + DateTime.Now.ToString()
                .Replace("/", "-")
                .Replace(":", "-")
                .Replace(" ", string.Empty)
                .Replace("PM", string.Empty)
                .Replace("AM", string.Empty);
            return uName;
        }
        public string GetFileExtension(IFormFile file)
        {
            string fileExtension;
            fileExtension = (file != null) && file.FileName.LastIndexOf('.') != -1 ?
                file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower()
                : string.Empty;
            return fileExtension;
        }
        public void DeleteOldFile(string OldProfileImageFile, string FileName)
        {

            if (System.IO.File.Exists(OldProfileImageFile) && FileName != "index.png")
            {
                System.IO.File.Delete(OldProfileImageFile);
            }
        }
        public string GetFileNameFromPath(string Path)
        {
            return Path.Substring(Path.LastIndexOf('\\') + 1);
        }
        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CheckIfImageFile(IFormFile file)
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
        public async Task<FileInformation> GetThumonailFromVideoAsync(string VideoPath, string OutputPath, CancellationToken cancellationToken)
        {
            FileInformation fileInformation = new FileInformation();

            MediaFile inputFile = new MediaFile(VideoPath);
            MediaFile outputFile = new MediaFile(OutputPath);

            Engine ffmpeg = new Engine("C:\\ffmpeg\\bin\\ffmpeg.exe");
            // Saves the frame located on the 15th second of the video.
            ConversionOptions options = new ConversionOptions
            {
                Seek = TimeSpan.FromSeconds(15),
            };

            fileInformation.MetaData = await ffmpeg.GetMetaDataAsync(inputFile);

            fileInformation.MediaFile = await ffmpeg.GetThumbnailAsync(inputFile, outputFile, options, cancellationToken);
            return fileInformation;
        }
    }
}