using Microsoft.AspNetCore.Identity;
using System;

namespace Site.Core.Domain.Entities
{
    public class Role : IdentityRole<Guid>, IEntity
    {
        public Role()
        {
            IsDeleted = false;
        }
        public bool IsDeleted { get; set; }
    }
}