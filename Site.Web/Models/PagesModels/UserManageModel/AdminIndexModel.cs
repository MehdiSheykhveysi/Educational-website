﻿using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.DTO;

namespace Site.Web.Models.PagesModels
{
    public class AdminIndexModel
    {
        public PagedResult<CustomUser> List { get; set; } = new PagedResult<CustomUser>();
        public string Searckkeyvalue { get; set; }
        public bool IsDeleted { get; set; }
    }
}