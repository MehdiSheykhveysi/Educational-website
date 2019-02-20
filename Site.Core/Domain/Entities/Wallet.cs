using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Core.Domain.Entities
{
    public class Wallet : BaseEntity<Guid>
    {
        public WalletType WalletType { get; set; }
        public decimal Balance { get; set; }
        public bool IsConfitmPayTransaction { get; set; }
        public string Description { get; set; }
        public DateTime TransactDate { get; set; }

        //Navigation Peroperties
        public Guid CustomerID { get; set; }
        public CustomUser CustomUser { get; set; }
    }


    public enum WalletType
    {
        [Display(Name = "بدهکار")]
        Debtor = 1,
        [Display(Name = "طلبکار")]
        Creditor = 2
    }
}
