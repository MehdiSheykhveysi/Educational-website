using Microsoft.EntityFrameworkCore;
using Site.Core.Domain.Entities;
using Site.Core.Infrastructures.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Site.Core.Infrastructures.Utilities.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<Course> SmartWhere(this IQueryable<Course> queryable, string Title, bool IsDeleted, IEnumerable<int> SelectedGroup,
            int MinPrice, int MaxPrice, PriceStatusType statusType)

        {
            Expression<Func<Course, bool>> expression = c => (string.IsNullOrEmpty(Title) || EF.Functions.Like(c.CourseTitle, $"%{Title}%")
        && c.IsDeleted == IsDeleted);
            
            switch (statusType)
            {
                case PriceStatusType.All:
                    queryable = queryable.Where(expression);
                    break;
                case PriceStatusType.Free:
                    queryable = queryable.Where(expression).Where(c => c.CoursePrice < 1000);
                    break;
                case PriceStatusType.Cash:
                    queryable = queryable.Where(expression).Where(c => c.CoursePrice <= MaxPrice && c.CoursePrice >= MinPrice);
                    break;
            }
            if (SelectedGroup != null && SelectedGroup.Any())
            {
                queryable = queryable.Where(c => SelectedGroup.Contains(c.CourseGroup.Id));
            }

            return queryable;
        }

        public static IQueryable<Course> SmartOrderByStatus(this IQueryable<Course> queryable, OrderStatusType statusType)
        {
            switch (statusType)
            {
                case OrderStatusType.Default:
                    queryable = queryable.OrderBy(c => c.CourseTitle);
                    break;
                case OrderStatusType.Date:
                    queryable = queryable.OrderBy(c => c.CreateDate);
                    break;
                case OrderStatusType.Price:
                    queryable = queryable.OrderBy(c => c.CoursePrice);
                    break;
            }
            return queryable;
        }
    }
}