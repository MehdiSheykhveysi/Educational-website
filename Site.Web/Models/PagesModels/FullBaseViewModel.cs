using System.Collections.Generic;

namespace Site.Web.Models.PagesModels
{
    public class FullBaseViewModel : WithFileBaseViewModel
    {
        public List<RoleModel> SelectedRoles { get; set; } = new List<RoleModel>();
    }

    public class RoleModel
    {
        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}