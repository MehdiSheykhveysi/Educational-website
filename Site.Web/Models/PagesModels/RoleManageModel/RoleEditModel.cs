using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.RoleManageModel
{
    public class RoleEditModel : BaseRoleModel
    {
        [Display(Name = "نام نقش")]
        [Required(ErrorMessage ="{0} را وارد کنید")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Id { get; set; }

        public List<ClaimDTO> Claims { get; set; } = new List<ClaimDTO>();
    }
}
