using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Site.Core.Domain.Entities
{
    public class CustomUser : IdentityUser<Guid>, IEntity
    {
        public CustomUser()
        {
            IsDeleted = false;
        }
        public CustomUser(DateTime TimeCreated) : this()
        {
            RegisterDate = TimeCreated;
        }
        public string ShowUserName { get; set; }
        public string Avatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public decimal AccountBalance { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Peroperties
        public ICollection<Course> Courses { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Transact> Transactions { get; set; }
    }
}
