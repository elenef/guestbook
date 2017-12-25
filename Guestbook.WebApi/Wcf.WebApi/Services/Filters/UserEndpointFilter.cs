using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GuestBook.WebApi.Identity;

namespace GuestBook.WebApi.Services.Filters
{
    public class UserEndpointFilter : BaseEndpoinFilter<UserFilterContract, User>
    {
        public async override Task<List<User>> ApplyPagerAsync(IQueryable<User> queryable, UserFilterContract filter)
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

        public override Task<IQueryable<User>> ApplyAsync(IQueryable<User> queryable, UserFilterContract filter)
        {
            // выборка по строковым полям(по имени или логину)
            if (!string.IsNullOrEmpty(filter.Search))
            {
                queryable = queryable.Where(w => w.Name.Contains(filter.Search) || w.UserName.Contains(filter.Search));
            }

            //queryable = ExceptUsersWithProviderAndCreditorRoles(queryable);

            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                queryable = FilterByOrderBy(queryable, filter.OrderBy, filter.OrderDesc);
            }

            return base.ApplyAsync(queryable, filter);
        }

        /*private IQueryable<User> ExceptUsersWithProviderAndCreditorRoles(IQueryable<User> queryable)
        {
            var query = queryable.Where(u => u.Roles.FirstOrDefault().RoleId != UserRoles.Creditor &&
            u.Roles.FirstOrDefault().RoleId != UserRoles.Provider);

            return query;
        }*/

        private IQueryable<User> FilterByOrderBy(IQueryable<User> queryable, string filterOrderBy, bool orderDesc)
        {
            if (filterOrderBy == "role" && !string.IsNullOrEmpty(filterOrderBy))
            {
                if (orderDesc)
                {
                    queryable = queryable.OrderByDescending(u => u.Roles.FirstOrDefault().RoleId);
                }
                else
                {
                    queryable = queryable.OrderBy(u => u.Roles.FirstOrDefault().RoleId);
                }
                return queryable;
            }
            return queryable;
        }
    }
}
