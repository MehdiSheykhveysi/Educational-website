using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.RoleManageModel
{
    public class RoleDetailModel : BaseRoleModel
    {
        [Display(Name = "نام نقش")]
        public override string Name { get => base.Name; set => base.Name = value; }

        public string Id { get; set; }
        public List<ClaimDTO> Claims { get; set; } = new List<ClaimDTO>();
    }

    public class ClaimDTO
    {
        public string Value { get; set; }
    }
}
