using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Site.Web.Infrastructures
{
    public class FileUploadHelper
    {
        public async Task<string> SaveFileAsync(IFormFile file, string pathToUplaod, string OldProfileImagePath)
        {
            string imageUrl = string.Empty;
            if (!Directory.Exists(pathToUplaod))
                System.IO.Directory.CreateDirectory(pathToUplaod);//Create Path of not exists

            string pathwithfileName = pathToUplaod + "\\" + SetFileName(file);
            using (FileStream fileStream = new FileStream(pathwithfileName, FileMode.Create))
            {
                string FileName = GetFileNameFromPath(OldProfileImagePath);
                DeleteOldFile(OldProfileImagePath, FileName);
                if (FileName != "index.png")
                {
                    await file.CopyToAsync(fileStream);
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
                string FileName = GetFileNameFromPath(OldProfileImagePath);
                DeleteOldFile(OldProfileImagePath, FileName);
                if (FileName != "index.png")
                {
                    file.CopyToAsync(fileStream);
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
    }
}