using GuestBook.Mapper;
using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Repositories;
using GuestBook.Services.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GuestBook.Services
{
    public class RestaurantEndpointService : EndpointService<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant, RestaurantEndpointFilter>
    {
        private IRepository<Review> _repositoryReview;

        private IRepository<User> _repositoryUser;

        public RestaurantEndpointService(
            IRepository<Restaurant> repository,
            IContractMapper mapper,
            RestaurantEndpointFilter filter,
            IRepository<Review> repositoryReview,
            IRepository<User> repositoryUser)
            : base(repository, mapper, filter)
        {
            _repositoryReview = repositoryReview;

            _repositoryUser = repositoryUser;
        }

        protected override async Task AfterMapAsync(List<Restaurant> model, List<RestaurantContract> contract)
        {
            foreach (var c in contract)
            {
                var review = _repositoryReview.Items.Include(r => r.User).Where(r => r.Restaurant.Id.Equals(c.Id)).ToList();

                var listReview = _mapper.Map<List<Review>, List<ReviewContract>>(review);

                c.Reviews = listReview;
            }
        }

        protected override IQueryable<Restaurant> GetListQuery()
        {
            return _repository.Items.Include(i => i.Reviews);
        }
    }
}
