﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services.Filters
{
    public class BaseEndpoinFilter<TFilterContract, TDataModel>
        : IEndpointFilter<TFilterContract, TDataModel>
        where TFilterContract : IFilterContract
    {
        public virtual async Task<List<TDataModel>> ApplyPagerAsync(IQueryable<TDataModel> queryable, TFilterContract filter)
        {
            var pageSize = filter.PageSize > 0 ? filter.PageSize : BaseFilterContract.DefaultPageSize;
            if (filter.Page > 0)
            {
                queryable = queryable.Skip(filter.Page * pageSize);
            }
            return await queryable
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Метод применяет фильтр. Если параметр <paramref name="filter.OrderBy"/>
        /// не задан, то сортировка выполняется по полю Timestamp или Id. Если таких полей
        /// в <typeparamref name="TDataModel"/> нет, то сортировка выполняется по первому полю.
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual Task<IQueryable<TDataModel>> ApplyAsync(IQueryable<TDataModel> queryable, TFilterContract filter)
        {
            var query = queryable;
            var order = filter.OrderDesc ? "DESC" : "ASC";
            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                query = query.OrderBy($"{filter.OrderBy} {order}");
            }
            else
            {
                var properties = typeof(TDataModel).GetProperties();

                string timestampField = "Created";
                var hasTimestampField = properties
                    .Any(p => p.Name == timestampField);

                string idField = "Id";
                var hasIdField = properties
                    .Any(p => p.Name == idField);

                var firstProperty = properties.First()?.Name;

                var orderProperty = hasTimestampField
                    ? timestampField
                    : (hasIdField ? idField : firstProperty);

                query = query.OrderBy(orderProperty);
            }

            return Task.FromResult(query);
        }
    }
}
