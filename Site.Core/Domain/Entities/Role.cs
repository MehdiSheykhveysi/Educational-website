using Microsoft.AspNetCore.Identity;
using System;

namespace Site.Core.Domain.Entities
{
    public class Role : IdentityRole<Guid>, IEntity
    {
    }
}