using System;
using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class CustomUser : IdentityUser<int>, IEntity
    {
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal AccountBalance { get; set; }

        //Navigation Peroperties

        public int WalletID { get; set; }
        public Wallet Wallet { get; set; }
    }
}
