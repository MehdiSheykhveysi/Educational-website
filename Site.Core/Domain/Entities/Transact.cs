using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Core.Domain.Entities
{
    public class Transact : BaseEntity<Guid>
    {
        public TransactType TransactType { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public DateTime TransactDate { get; set; }

        //Foreign key
        public string TransactId { get; set; }

        //Navigation Peroperties
        public Guid CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; }
    }


    public enum TransactType
    {
        [Display(Name = "برداشت از حساب")]
        Debtor = 1,
        [Display(Name = "واریز به حساب")]
        Creditor = 2,
        [Display(Name ="خرید")]
        Payment=2
    }
}
