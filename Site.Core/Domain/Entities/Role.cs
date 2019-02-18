using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class Role : IdentityRole<int>, IEntity
    {
    }
}
