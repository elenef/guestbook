using GuestBook.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services.Filters
{
    public class RestaurantEndpointFilter : BaseEndpoinFilter<RestaurantFilterContract, Restaurant>
    {
        public override Task<IQueryable<Restaurant>> ApplyAsync(IQueryable<Restaurant> queryable,
            RestaurantFilterContract filter)
        {
            var query = queryable;

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var queryList = query.Where(p => p.Name.Contains(filter.Search)).ToList();

                query.Where(p => p.Reviews.Where(r => r.Comment.Contains(filter.Search)).Count() != 0).ToList()
                    .ForEach(q => queryList.Add(q));
            }

            return Task.FromResult(query);
        }
    }
}
