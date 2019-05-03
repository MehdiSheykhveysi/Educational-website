using Microsoft.AspNetCore.Http;
using Site.Web.Infrastructures.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Site.Web.Infrastructures.CustomValidationAttribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CheckFileAttribute : ValidationAttribute
    {
        private readonly List<string> mimeTypes;
        private readonly FileUploadedType uploadedType;
        private IFileHelper fileHelper;

        public CheckFileAttribute(string MimeTypes, FileUploadedType UploadedType)
        {
            this.uploadedType = UploadedType;
            this.mimeTypes = MimeTypes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            IFormFile file = value as IFormFile;
            fileHelper = (IFileHelper)validationContext.GetService(typeof(IFileHelper));

            bool IsImage = uploadedType == FileUploadedType.Image ? true : false;
            if (IsImage)
            {
                if (!fileHelper.CheckIfImageFile(file))
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else if (!mimeTypes.Contains(file?.ContentType))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
