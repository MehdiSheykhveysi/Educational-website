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
        public CustomUser(DateTime TimeCreated)
        {
            RegisterDate = TimeCreated;
            IsDeleted = false;
        }
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal AccountBalance { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Peroperties
        public ICollection<Transact> Transactions { get; set; }
    }
}
