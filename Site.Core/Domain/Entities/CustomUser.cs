using System;
using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class CustomUser:IdentityUser
    {
        public string PhoneNunber { get; set; }
        public string Avatar { get; set; }
    }
}
