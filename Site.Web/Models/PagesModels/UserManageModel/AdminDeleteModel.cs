using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Models.PagesModels
{
    public class AdminDeleteModel : BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public Guid Id { get; set; }

        [Display(Name ="تاریخ عضویت")]
        public DateTime RegisterDate { get; set; }

        [Display(Name ="میزان شارژ حساب")]
        public decimal AccountBalance { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        public string Avatar { get; set; }
    }
}
