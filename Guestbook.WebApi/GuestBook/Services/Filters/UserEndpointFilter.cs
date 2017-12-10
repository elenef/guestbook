using GuestBook.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Services.Filters
{
    public class UserEndpointFilter : BaseEndpoinFilter<UserFilterContract, User>
    {
        public override Task<IQueryable<User>> ApplyAsync(IQueryable<User> queryable,
            UserFilterContract filter)
        {
            var query = queryable;

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p => p.Name.Contains(filter.Search));
            }

            return Task.FromResult(query);
        }
    }
}
