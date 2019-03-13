﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Site.Core.Domain.Entities
{
    public class Transact : BaseEntity<Guid>
    {
        public TransactType TransactType { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public DateTime TransactDate { get; set; }
        public string TransactId { get; set; }

        //Navigation Peroperties
        public Guid CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; }
    }


    public enum TransactType
    {
        [Display(Name = "بدهکار")]
        Debtor = 1,
        [Display(Name = "طلبکار")]
        Creditor = 2
    }
}