using System;
using GuestBook.Services.Filters;
using GuestBook.Mapper;
using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Services
{
    public class EndpointService<TContract, TEditContract, TFilterContract, TDataModel, TFilter>
        : IEndpointService<TContract, TEditContract, TFilterContract, TDataModel>
        where TContract : class
        where TEditContract : class
        where TDataModel : class, IModel
        where TFilter : IEndpointFilter<TFilterContract, TDataModel>
        where TFilterContract : IFilterContract
    {
        protected IRepository<TDataModel> _repository;

        protected IContractMapper _mapper;

        protected TFilter _filter;

        public EndpointService(
            IRepository<TDataModel> repository,
            IContractMapper mapper,
            TFilter filter)
        {
            _repository = repository;
            _mapper = mapper;
            _filter = filter;
        }

        public virtual async Task<ItemList<TContract>> ListAsync(TFilterContract filterModel)
        {
            var query = await _filter.ApplyAsync(_repository.Items, filterModel);
            var total = query.Count();
            var list = await _filter.ApplyPagerAsync(query, filterModel);

            var models = _mapper.Map<List<TDataModel>, List<TContract>>(list);
            await AfterMapAsync(list, models);

            var result = new ItemList<TContract>
            {
                PageSize = filterModel.PageSize,
                Page = filterModel.Page,
                Data = models,
                Total = total
            };
            return result;
        }

        public virtual async Task<TContract> GetAsync(string id)
        {
            var item = await GetListQuery()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new Exception(typeof(TDataModel) + id);
            }

            var result = _mapper.Map<TDataModel, TContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }

        public virtual async Task<TContract> AddAsync(TEditContract model)
        {
            await ValidateModel(model);
            var item = _mapper.Map<TEditContract, TDataModel>(model);
            await BeforeUpdate(model, item);

            await _repository.AddAsync(item);

            var result = _mapper.Map<TDataModel, TContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }

        public virtual async Task<TContract> UpdateAsync(string id, TEditContract model)
        {
            await ValidateModel(model);
            var item = await GetListQuery()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                throw new Exception(typeof(TDataModel) + id);
            }

            _mapper.Map(model, item);
            await BeforeUpdate(model, item);

            await _repository.UpdateAsync(item);

            var result = _mapper.Map<TDataModel, TContract>(item);
            await AfterMapAsync(item, result);
            return result;
        }

        public virtual async Task DeleteAsync(string id)
        {
            var item = await GetListQuery()
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item != null)
            {
                await BeforeDeleteItem(item);

                await _repository.RemoveAsync(item);
            }
            else
            {
                throw new Exception(typeof(TDataModel) + id);
            }

            return;
        }

        /// <summary>
        /// Валидация модели. Метод может быть переопределен в дочерних классах.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual Task<bool> ValidateModel(TEditContract model) => Task.FromResult(true);

        /// <summary>
        /// Этот метод вызывается перед выполнение удаления документа из репозитория.
        /// Может быть переопределен в дочерних классах
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual Task BeforeDeleteItem(TDataModel item) => Task.CompletedTask;

        /// <summary>
        /// Это метод вызывается перед сохранением изменений в репозиторий.
        /// Этот метод вызывается как при создании, так и при обновлении документа.
        /// </summary>
        /// <param name="model">Измененная модель документа.</param>
        /// <param name="item">Модель базы данных, которая будет сохранена</param>
        /// <returns></returns>
        protected virtual Task BeforeUpdate(TEditContract model, TDataModel item) => Task.CompletedTask;

        /// <summary>
        /// Этот метод вызывается после преобразования моделей данных в модели контрактов.
        /// Может быть переопределен в дочерних классах для заполнения отдельных полей контрактов.
        /// </summary>
        /// <param name="models"></param>
        /// <param name="contracts"></param>
        /// <returns></returns>
        protected virtual Task AfterMapAsync(List<TDataModel> models, List<TContract> contracts) => Task.CompletedTask;

        /// <summary>
        /// В этом методе формируется запрос к репозиторию на получение данных. Метод может быть переопределен в дочерних классах,
        /// чтобы изменить запрос
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TDataModel> GetListQuery()
        {
            return _repository.Items;
        }

        /// <summary>
        /// В этом методе формируется запрос к репозиторию, который будет использован для добавления или удаления данных.
        /// Метод может быть переопределен в дочерних классах, чтобы изменить запрос
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TDataModel> GetUpdateQuery()
        {
            return GetListQuery();
        }

        protected async Task AfterMapAsync(TDataModel model, TContract contract)
        {
            await AfterMapAsync(new List<TDataModel> { model }, new List<TContract> { contract });
        }
    }
}
