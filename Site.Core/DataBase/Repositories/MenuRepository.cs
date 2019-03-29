using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class MenuRepository : GenericRepositories<Menu>, IMenuRepository
    {
        public MenuRepository(LearningSiteDbContext Context) : base(Context)
        {
        }
    }
}
