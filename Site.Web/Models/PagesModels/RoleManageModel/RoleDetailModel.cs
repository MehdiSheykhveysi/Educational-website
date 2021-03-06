﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels.RoleManageModel
{
    public class RoleDetailModel : BaseRoleModel
    {
        [Display(Name = "نام نقش")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Id { get; set; }
        public List<ClaimDTO> Claims { get; set; } = new List<ClaimDTO>();
    }
    public class ClaimDTO
    {
        public string Value { get; set; }
        public bool Checked { get; set; }
    }
}
