using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    public class RestaurantEndpointService : EndpointService<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant, RestaurantEndpointFilter>
    {
        private IRepository<Review> _repositoryReview;

        public RestaurantEndpointService(
            IRepository<Restaurant> repository,
            IContractMapper mapper,
            RestaurantEndpointFilter filter,
            IRepository<Review> repositoryReview)
            : base(repository, mapper, filter)
        {
            _repositoryReview = repositoryReview;
        }

        protected override async Task AfterMapAsync(List<Restaurant> model, List<RestaurantContract> contract)
        {
            foreach (var c in contract)
            {
                var review = _repositoryReview.Items.Include(r => r.User).Where(r => r.Restaurant.Id.Equals(c.Id)).ToList();

                var listReview = _mapper.Map<List<Review>, List<ReviewContract>>(review);

                c.ReviewIds = listReview.Select(r => r.Id).ToList();
            }
        }

        protected override IQueryable<Restaurant> GetListQuery()
        {
            return _repository.Items.Include(i => i.Reviews);
        }
    }
}
