using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;

namespace Site.Core.DataBase.Repositories
{
    public class OrderRepository : GenericRepositories<Order>, IOrderRepository
    {
        public OrderRepository(Context.LearningSiteDbContext context) : base(context) => OrderDetails = context.Set<OrderDetail>();

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public async Task<Order> FindBySpecifiedIdAsync(Expression<Func<Order, bool>> WhereCondition, CancellationToken cancellationToken)
        {
            return await NoTrackEntities.Include(o => o.OrderDetails).FirstOrDefaultAsync(WhereCondition, cancellationToken);
        }

        public async Task<int> GetOrderedCountAsync(int CourseId, CancellationToken cancellationToken)
        {
            return await NoTrackEntities.Where(predicate: o => !o.IsBought).Include(o => o.OrderDetails).Select(c => c.OrderDetails.Count(od => od.CourseId == CourseId)).FirstOrDefaultAsync();
        }

        public async Task DeleteOrderDetailAsync(string OrderId, string OrderDetailId, CancellationToken cancellationToken)
        {
            OrderDetail detail = new OrderDetail();
            detail.Id = int.Parse(OrderDetailId);
            detail.OrderId = Guid.Parse(OrderId);
            OrderDetails.Remove(detail);
            await SaveChangesAsync(cancellationToken);
        }
    }
}