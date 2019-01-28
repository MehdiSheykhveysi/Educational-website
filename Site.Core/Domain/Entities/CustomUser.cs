using System;
using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class CustomUser:IdentityUser
    {
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal Wallet { get; set; }
    }
}
