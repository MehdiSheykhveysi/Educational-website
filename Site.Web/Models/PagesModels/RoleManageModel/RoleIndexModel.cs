using System.Collections.Generic;

namespace Site.Web.Models.PagesModels.RoleManageModel
{
    public class RoleIndexModel : BaseRoleModel
    {
        public List<RoleManageModel> Roles { get; set; } = new List<RoleManageModel>();
        public string SearchRoleKey { get; set; }
    }
    public class RoleManageModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
