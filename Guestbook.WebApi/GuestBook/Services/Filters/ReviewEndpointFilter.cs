using GuestBook.Domain;
using System.Linq;
using System.Threading.Tasks;

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
                query = query.Where(p => p.Restaurant.Name.Contains(filter.Search));
            }

            if (!string.IsNullOrWhiteSpace(filter.restaurantName))
            {
                query = query.Where(q => filter.restaurantName.Equals(q.Restaurant.Name));
            }

            return Task.FromResult(query);
        }
    }
}
