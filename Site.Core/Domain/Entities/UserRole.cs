using Microsoft.AspNetCore.Identity;
using System;

namespace Site.Core.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual CustomUser User { get; set; }
        public virtual Role Role { get; set; }
    }
}
