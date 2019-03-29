using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Web.ViewComponents
{
    [ViewComponent]
    public class MenuMasterViewComponent : ViewComponent
    {
        public MenuMasterViewComponent(IMenuRepository MenuRepository)
        {
            this.menuRepository = MenuRepository;
        }
        private readonly IMenuRepository menuRepository;

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            List<Menu> menuList = await menuRepository.Entities.Include(c=>c.MenuItems).ToListAsync(cancellationToken);
            
            return View("Default", menuList);
        }
    }
}
