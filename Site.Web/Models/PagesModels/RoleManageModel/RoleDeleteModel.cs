using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.RoleManageModel
{
    public class RoleDeleteModel : BaseRoleModel
    {
        [Display(Name = "نام نقش")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Id { get; set; }
    }
}
