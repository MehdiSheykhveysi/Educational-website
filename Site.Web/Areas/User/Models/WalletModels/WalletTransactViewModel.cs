using Site.Core.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Web.Areas.User.Models.WalletModels
{
    public class WalletTransactViewModel
    {
        public List<Transact> Wallets { get; set; }

        [Display(Name ="مبلغ شارژ حساب")]
        [Required(ErrorMessage ="لطفا مبلغ را وارد کنید")]
        public decimal Deposits { get; set; }
    }
}
