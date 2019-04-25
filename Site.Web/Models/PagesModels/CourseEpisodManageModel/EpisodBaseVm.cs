using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.CourseEpisodManageModel
{
    public class EpisodBaseVm
    {
        [UIHint("Id")]
        public int CourseId { get; set; }
    }

    public class EpisodFullBaseVm : EpisodBaseVm
    {
        [UIHint("Id")]
        public int Id { get; set; }

        [Display(Name = "عنوان این قسمت")]
        [Required(ErrorMessage ="وارد کردن عنوان دوره اجباری است")]
        public string Title { get; set; }

        [Display(Name = "تصویر فعلی این قسمت")]
        public string FileName { get; set; }

        [Display(Name = "مدت زمان")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }
    }
}
