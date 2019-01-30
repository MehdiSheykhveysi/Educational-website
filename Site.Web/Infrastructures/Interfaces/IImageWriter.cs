﻿using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Site.Web.Infrastructures.Interfaces
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file,string PathToUploadFile);
    }
}