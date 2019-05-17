using Site.Core.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.DataBase.Repositories
{
    public interface IOrderRepository : IGenericRepositories<Order>
    {
        Task<Order> FindBySpecifiedIdAsync(Expression<Func<Order, bool>> WhereCondition, CancellationToken cancellationToken);
        Task<int> GetOrderedCountAsync(int CourseId, CancellationToken cancellationToken);
        Task DeleteOrderDetailAsync(string OrderId, string OrderDetailId, CancellationToken cancellationToken);
    }
}