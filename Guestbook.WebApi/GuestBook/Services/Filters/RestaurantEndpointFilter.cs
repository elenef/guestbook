using GuestBook.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Services.Filters
{
    public class RestaurantEndpointFilter : BaseEndpoinFilter<RestaurantFilterContract, Restaurant>
    {
        public override Task<IQueryable<Restaurant>> ApplyAsync(IQueryable<Restaurant> queryable,
            RestaurantFilterContract filter)
        {
            var query = queryable;

            return Task.FromResult(query);
        }
    }
}
