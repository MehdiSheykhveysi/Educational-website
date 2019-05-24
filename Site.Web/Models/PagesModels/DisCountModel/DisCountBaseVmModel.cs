using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.DisCountModel
{
    public class DisCountBaseVmModel
    {
        [Display(Name = "نام کد تخفیف")]
        [Required(ErrorMessage = "واردکردن نام برای کد تخفیف اجباری است")]
        [StringLength(10, ErrorMessage = "حداکثر طول مجاز 10 کاراکتر و حداقل 3", MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "تعداد مجاز برای کد تخفیف")]
        [Required(ErrorMessage = "وارد کردن فیلد تعداد اجباری است")]
        public int Count { get; set; }

        [Display(Name = "میزان درصد تخفیف")]
        [Required(ErrorMessage = "وارد کردن فیلد میزان درصد تخفیف اجباری است")]
        public int DisCountPercent { get; set; }

        [Display(Name = "تاریخ شروع اعتبار")]
        [Required(ErrorMessage = "وارد کردن فیلد تاریخ شروع کد اجباری است")]
        [RegularExpression(@"(?:1[23]\d{2})\/(?:0?[1-9]|1[0-2])\/(?:0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "فرمت تاریخ وارد شده صحیح نیست")]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان اعتبار")]
        [Required(ErrorMessage = "وارد کردن فیلد تاریخ انقضاءاجباری است")]
        [RegularExpression(@"(?:1[23]\d{2})\/(?:0?[1-9]|1[0-2])\/(?:0?[1-9]|[12][0-9]|3[01])$", ErrorMessage = "فرمت تاریخ وارد شده صحیح نیست")]
        public string MaxDate { get; set; }
    }
}
