using System;
using System.Collections.Generic;

namespace Site.Core.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            IsBought = false;
        }

        public Order(bool IsBoughted)
        {
            IsBought = IsBoughted;
        }
        public bool IsBought { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderingTime { get; set; }
        public string AnonymousUserId { get; set; }

        //Forgion Key
        public Guid? ClientId { get; set; }

        //Navigations
        public CustomUser Client { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
