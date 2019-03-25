using Site.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels
{
    public class AdminDetailModel : AdminDeleteModel
    {
        [Display(Name = "نقش های کاربر")]
        public List<RoleModel> SelectedRoles { get; set; } = new List<RoleModel>();

        [Display(Name ="تراکنش های انجام شده")]
        public List<TransactModel> Transactions { get; set; } = new List<TransactModel>();
    }

    public class TransactModel
    {
        public TransactType TransactType { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }
        
        public DateTime TransactDate { get; set; }
    }
}
