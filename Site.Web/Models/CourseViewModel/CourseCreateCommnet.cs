using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.CourseViewModel
{
    public class CourseCreateCommnet
    {
        [Required(ErrorMessage = "متن نظر نمیتواند خالی باشد")]
        [MaxLength(250, ErrorMessage = "طول نظر بیشتر از حد مجاز است .حداکثر250 کاراکتر")]
        public string Body { get; set; }

        [Required(ErrorMessage = "اطلاعات وارده درست نیس")]
        public int CourseId { get; set; }

        [Range(1, int.MaxValue)]
        public int CurrentPageNumber { get; set; }
    }
}
