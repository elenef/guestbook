using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    public class ReviewEndpointService : EndpointService<ReviewContract, EditReviewContract, ReviewFilterContract, Review, ReviewEndpointFilter>
    {
        private IRepository<Restaurant> _repositoryRestaurants;

        public ReviewEndpointService(
            IRepository<Review> repository,
            IContractMapper mapper,
            ReviewEndpointFilter filter,
            IRepository<Restaurant> repositoryRestaurants)
            : base(repository, mapper, filter)
        {
            _repositoryRestaurants = repositoryRestaurants;
        }

        protected override IQueryable<Review> GetListQuery()
        {
            return _repository.Items
                .Include(i => i.Restaurant);
        }

        public override async Task<ItemList<ReviewContract>> ListAsync(ReviewFilterContract filterModel)
        {
            var query = await _filter.ApplyAsync(GetListQuery(), filterModel);
            var total = query.Count();
            var list = await _filter.ApplyPagerAsync(query, filterModel);

            var models = _mapper.Map<List<Review>, List<ReviewContract>>(list);
            await AfterMapAsync(list, models);

            var result = new ItemList<ReviewContract>
            {
                PageSize = filterModel.PageSize,
                Page = filterModel.Page,
                Data = models,
                Total = total
            };

            return result;
        }

        protected override async Task BeforeUpdate(EditReviewContract model, Review item)
        {
            item.Restaurant = await _repositoryRestaurants.Items.Where(r => r.Id.Equals(model.RestaurantId))
                .FirstOrDefaultAsync();

            var restaurant = await _repositoryRestaurants.Items.Where(l => l.Id.Equals(model.RestaurantId))
                .FirstOrDefaultAsync();

            restaurant.Rating = (restaurant.Rating + model.ReviewRating) / 2;

            await _repositoryRestaurants.UpdateAsync(restaurant);

            await base.BeforeUpdate(model, item);
        }

        public override async Task<ReviewContract> UpdateAsync(string id, EditReviewContract model)
        {
            await ValidateModel(model);
            var item = await GetListQuery()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new Exception(typeof(Review) + id);
            }

            _mapper.Map(model, item);

            await _repository.UpdateAsync(item);

            var result = _mapper.Map<Review, ReviewContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }
    }
}
