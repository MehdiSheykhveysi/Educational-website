using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Content { get; set; }
        public string Url { get; set; }
        public bool IsDelete { get; set; }

        public int? ParentMenuId { get; set; }

        //navogation propperties => Self Relation
        public Menu ParentMenu { get; set; }
        public ICollection<Menu> MenuItems { get; set; }
    }
}
