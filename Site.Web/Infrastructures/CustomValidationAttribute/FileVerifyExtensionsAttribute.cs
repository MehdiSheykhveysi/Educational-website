using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Site.Web.Infrastructures.CustomValidationAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileVerifyExtensionsAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public FileVerifyExtensionsAttribute(string fileExtensions) => AllowedExtensions = fileExtensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        public override bool IsValid(object value)
        {

            if (value is IFormFile file)
            {
                string fileName = file.FileName;

                return AllowedExtensions.Any(y => fileName.EndsWith(y));
            }
            return false;
        }
    }
}
