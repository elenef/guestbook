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

            return Task.FromResult(query);
        }
    }
}
