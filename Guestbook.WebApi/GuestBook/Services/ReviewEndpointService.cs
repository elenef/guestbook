﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestBook.Mapper;
using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Repositories;
using GuestBook.Services.Filters;
using Microsoft.EntityFrameworkCore;

namespace GuestBook.Services
{
    public class ReviewEndpointService : EndpointService<ReviewContract, EditReviewContract, ReviewFilterContract, Review, ReviewEndpointFilter>
    {
        private IRepository<User> _repositoryUsers;
        private IRepository<Restaurant> _repositoryRestaurants;

        public ReviewEndpointService(
            IRepository<Review> repository,
            IContractMapper mapper,
            ReviewEndpointFilter filter,
            IRepository<User> repositoryUsers,
            IRepository<Restaurant> repositoryRestaurants)
            : base(repository, mapper, filter)
        {
            _repositoryRestaurants = repositoryRestaurants;
            _repositoryUsers = repositoryUsers;
        }

        protected override IQueryable<Review> GetListQuery()
        {
            return _repository.Items.Include(i => i.Restaurant).Include(i => i.User);
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
            item.Restaurant = await _repositoryRestaurants.Items.Where(r => r.Id.Equals(model.Restaurant.Id))
                .FirstOrDefaultAsync();

            item.User = await _repositoryUsers.Items.Where(r => r.Id.Equals(model.User.Id))
                .FirstOrDefaultAsync();

            await base.BeforeUpdate(model, item);
        }
    }
}
