using System.ComponentModel.DataAnnotations;

namespace Site.Core.Infrastructures.Utilities.Enums
{
    public enum PriceStatusType
    {
        [Display(Name = "همه")]
        All = 0,

        [Display(Name = "رایگان")]
        Free,

        [Display(Name = "نقدی")]
        Cash
    }
}