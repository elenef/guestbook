using GuestBook.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Services.Filters
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

            return Task.FromResult(query);
        }
    }
}
