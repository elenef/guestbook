using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    public class RestaurantEndpointService : EndpointService<RestaurantContract, EditRestaurantContract,
        RestaurantFilterContract, Restaurant, RestaurantEndpointFilter>
    {
        private IRepository<Review> _repositoryReview;
        private IUserContext _userContext;
        IRepository<RegisteredUser> _registeredUserRepository;

        public RestaurantEndpointService(
            IRepository<Restaurant> repository,
            IContractMapper mapper,
            RestaurantEndpointFilter filter,
            IRepository<Review> repositoryReview,
            IUserContext userContext,
            IRepository<RegisteredUser> registeredUserRepository)
            : base(repository, mapper, filter)
        {
            _repositoryReview = repositoryReview;
            _userContext = userContext;
            _registeredUserRepository = registeredUserRepository;
        }

        protected override async Task AfterMapAsync(List<Restaurant> model, List<RestaurantContract> contract)
        {
            foreach (var c in contract)
            {
                var review = _repositoryReview.Items.Where(r => r.Restaurant.Id.Equals(c.Id)).ToList();

                var listReview = _mapper.Map<List<Review>, List<ReviewContract>>(review);

                c.ReviewIds = listReview.Select(r => r.Id).ToList();
            }
        }

        protected override IQueryable<Restaurant> GetListQuery()
        {
            return _repository.Items.Include(i => i.Reviews);
        }

        public override async Task<RestaurantContract> AddAsync(EditRestaurantContract model)
        {
            await ValidateModel(model);
            var item = _mapper.Map<EditRestaurantContract, Restaurant>(model);
            await BeforeUpdate(model, item);

            var userId = _userContext.UserId;
            var user = await _registeredUserRepository.Items.FirstOrDefaultAsync(u => u.Id.Equals(userId));

            item.Users = user;

            await _repository.AddAsync(item);

            var result = _mapper.Map<Restaurant, RestaurantContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }
    }
}
