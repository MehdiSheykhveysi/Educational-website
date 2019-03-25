using System.ComponentModel.DataAnnotations;

namespace Site.Core.Infrastructures.Utilities.Enums
{
    public enum CustomClaimTypes
    {
        [Display(Name = "کاربر عادی")]
        None =0,

        [Display(Name = "افزودن کاربر")]
        AddUser,

        [Display(Name = "ویرایش کاربر")]
        EditUser,

        [Display(Name = "حذف کاربر")]
        DeleteUser,

        [Display(Name = "مدیریت کاربران")]
        UserManagment ,

        [Display(Name = "افزودن نقش")]
        AddRole,

        [Display(Name = "ویرایش نقش")]
        EditRole,

        [Display(Name = "حذف نقش")]
        DeleteRole,

        [Display(Name = "مدیریت نقش ها")]
        RoleManagment ,

        [Display(Name = "مدیریت سیستم")]
        AdminSystem 
    }
}
