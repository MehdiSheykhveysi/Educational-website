using Microsoft.EntityFrameworkCore;
using Site.Core.DataBase.Context;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using Site.Core.Infrastructures.Utilities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public class UserRepository : GenericRepositories<CustomUser>, IUserRepository
    {

        public UserRepository(LearningSiteDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<CustomUser>> GetPagedUserAsync(string UserName, int Count, int CurrentNumber,CancellationToken cancellationToken)
        {
            PagedResult<CustomUser> paged = new PagedResult<CustomUser>();

            int ListCount = await NoTrackEntities.CountAsync();

            paged.ListItem = await NoTrackEntities.Where(c => !Assert.NotNull(UserName) || c.UserName.IndexOf(UserName, StringComparison.CurrentCultureIgnoreCase) != -1).OrderBy(u => u.UserName).Skip((CurrentNumber - 1) * Count).Take(Count).AsNoTracking().ToListAsync(cancellationToken);

            paged.PageData.CurentItem = CurrentNumber;
            paged.PageData.TotalItem = ListCount;
            paged.PageData.ItemPerPage = Count;
            return paged;
        }
    }
}
