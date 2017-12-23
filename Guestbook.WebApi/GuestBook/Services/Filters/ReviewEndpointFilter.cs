﻿using System;
using GuestBook.Domain;
using System.Linq;
using System.Threading.Tasks;
using GuestBook.Domain.Helpers;

namespace GuestBook.WebApi.Services.Filters
{
    public class ReviewEndpointFilter : BaseEndpoinFilter<ReviewFilterContract, Review>
    {
        public override Task<IQueryable<Review>> ApplyAsync(IQueryable<Review> queryable,
            ReviewFilterContract filter)
        {
            var query = queryable;

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p => p.Comment.Contains(filter.Search) || p.UserName.Contains(filter.Search));
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p => p.Comment.Contains(filter.Search) || p.UserName.Contains(filter.Search));
            }

            if (!string.IsNullOrWhiteSpace(filter.RestaurantName))
            {
                query = query.Where(q => filter.RestaurantName.Equals(q.Restaurant.Name));
            }

            query = FilterByReviewCreationDate(query, filter.DateTo, filter.DateFrom);

            return Task.FromResult(query);
        }

        private IQueryable<Review> FilterByReviewCreationDate(IQueryable<Review> query, long filterDateTo, long filterDateFrom)
        {
            var dateFrom = TimeConverter.FromUnixTime(filterDateFrom);

            if (filterDateTo <= 0)
            {
                filterDateTo = (long)TimeConverter.ToUnixTime(DateTime.UtcNow);
            }

            var dateTo = TimeConverter.FromUnixTime(filterDateTo);

            if (dateFrom > dateTo)
            {
                throw new Exception("Вверхняя граница фильтра больше чем нижняя граница.");
            }

            if (filterDateFrom != 0)
            {
                query = query.Where(s => s.Created >= dateFrom);
            }

            if (filterDateTo != 0)
            {
                query = query.Where(s => s.Created <= dateTo);
            }
            return query;
        }
    }
}
