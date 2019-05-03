using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories.CustomizeIdentity
{
    public class CustomUserManager : UserManager<CustomUser>
    {

        public CustomUserManager(IUserStore<CustomUser> UserStore, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<CustomUser> passwordHasher,
        IEnumerable<IUserValidator<CustomUser>> userValidators,
        IEnumerable<IPasswordValidator<CustomUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<CustomUserManager> logger) :
        base(UserStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
            services, logger)
        {
        }

        public async Task<PagedResult<CustomUser>> GetPagedUserAsync(string Email, bool IsDeleted, int Count, int CurrentNumber, CancellationToken cancellationToken)
        {
            PagedResult<CustomUser> paged = new PagedResult<CustomUser>();

            int ListCount = await Users.CountAsync();

            paged.ListItem = await base.Users.Where(u => (string.IsNullOrEmpty(Email) || EF.Functions.Like(u.Email, $"%{Email}%")) && u.IsDeleted == IsDeleted).OrderBy(u => u.UserName).Skip((CurrentNumber - 1) * Count).Take(Count).AsNoTracking().ToListAsync(cancellationToken);
            paged.PageData.CurentItem = CurrentNumber;
            paged.PageData.TotalItem = ListCount;
            paged.PageData.ItemPerPage = Count;
            return paged;
        }
    }
}
