using Site.Core.Infrastructures.Utilities.Enums;
using System.Collections.Generic;

namespace Site.Web.Models.PagesModels.RoleManageModel
{
    public class RoleCreateModel : BaseRoleModel
    {
        public List<ClaimModel> CustomClaims { get; set; } = new List<ClaimModel>();
    }
    public class ClaimModel
    {
        public ClaimModel(string name, CustomClaimType claimTypes, bool check = false)
        {
            Name = name;
            Checked = check;
            CustomClaim = claimTypes;
        }
        public ClaimModel()
        {

        }
        public string Name { get; set; }
        public bool Checked { get; set; } = false;
        public CustomClaimType CustomClaim { get; set; }
    }
}