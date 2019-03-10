using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class CustomUser : IdentityUser<Guid>, IEntity
    {
        public CustomUser()
        {

        }
        public CustomUser(DateTime TimeCreated, bool IsActive = false)
        {
            RegisterDate = TimeCreated;
            EmailConfirmed = IsActive;
        }
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal AccountBalance { get; set; }

        //Navigation Peroperties
        public ICollection<Transact> Transactions { get; set; }
    }
}
