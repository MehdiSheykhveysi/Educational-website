using Site.Core.DataBase.Repositories;
using Site.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Core.ApplicationService.CartServise
{
    public class CartChecker
    {
        public CartChecker(IOrderRepository OrderRepository, ICourseRepository CourseRepository)
        {
            orderRepository = OrderRepository;
            courseRepository = CourseRepository;
        }
        private readonly IOrderRepository orderRepository;
        private readonly ICourseRepository courseRepository;

        public async Task<bool> AddAsync(int CourseID, Guid? ClientId, string AnonymousUserId, CancellationToken cancellationToken, bool IsAuthenticated = false)
        {
            Course course = await courseRepository.GetByIdAsync(CourseID, cancellationToken);

            if (course == null) return false;

            Order order = new Order();
            Order currentOrder = await orderRepository.FindBySpecifiedIdAsync(o => (o.ClientId == ClientId || o.AnonymousUserId == AnonymousUserId) && !o.IsBought, cancellationToken);
            order.ClientId = ClientId ?? order.ClientId;
            order.AnonymousUserId = string.IsNullOrEmpty(AnonymousUserId) ? string.Empty : AnonymousUserId;

            if (currentOrder == null)
            {
                order.IsBought = false;
                order.OrderingTime = DateTime.Now;
                order.OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        Course=course,
                        CourseId=course.Id
                    }
                };
                await orderRepository.AddAsync(order, cancellationToken);
                return true;
            }
            else
            {
                int FoundedItem = currentOrder.OrderDetails.Count(od => od.CourseId == CourseID);

                if (FoundedItem != 0) return false;

                currentOrder.OrderDetails.Add(new OrderDetail
                {
                    Course = course,
                    CourseId = course.Id
                });

                await orderRepository.UpdateAsync(currentOrder, cancellationToken);
                return true;
            }

        }
    }
}