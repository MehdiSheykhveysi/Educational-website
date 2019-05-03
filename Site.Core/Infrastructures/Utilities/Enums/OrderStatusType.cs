using System.ComponentModel.DataAnnotations;

namespace Site.Core.Infrastructures.Utilities.Enums
{
    public enum OrderStatusType
    {
        [Display(Name = "پیشفرض")]
        Default = 0,

        [Display(Name = "قیمت")]
        Price,

        [Display(Name = "تاریخ تولید")]
        Date
    }
}
