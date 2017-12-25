using GuestBook.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services.Filters
{
    public class RegisteredUserEndpointFilter : BaseEndpoinFilter<RegisteredUserFilterContract, RegisteredUser>
    {
        public override Task<IQueryable<RegisteredUser>> ApplyAsync(IQueryable<RegisteredUser> queryable,
            RegisteredUserFilterContract filter)
        {
            var query = queryable;

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p => p.Name.Contains(filter.Search));
            }

            return base.ApplyAsync(query, filter);
        }
    }
}
