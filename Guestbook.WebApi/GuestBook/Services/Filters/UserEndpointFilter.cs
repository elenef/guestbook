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

            return Task.FromResult(query);
        }
    }
}
