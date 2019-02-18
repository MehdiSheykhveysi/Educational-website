using System;
using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class CustomUser : IdentityUser<Guid>, IEntity
    {
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal AccountBalance { get; set; }

        //Navigation Peroperties

        public Guid WalletID { get; set; }
        public Wallet Wallet { get; set; }
    }
}
