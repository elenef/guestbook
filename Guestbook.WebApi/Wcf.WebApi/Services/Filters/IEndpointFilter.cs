using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services.Filters
{
    /// <summary>
    /// Фильтр для ресурса апи.
    /// </summary>
    /// <typeparam name="TFilterContract">Тип модели фильтра, которая задает параметры фильтра</typeparam>
    /// <typeparam name="TDataModel">Тип данных, фильтрация которых осуществляется</typeparam>
    public interface IEndpointFilter<TFilterContract, TDataModel>
        where TFilterContract: IFilterContract
    {
        /// <summary>
        /// Применить фильтр.
        /// </summary>
        /// <param name="queryable">IQueryable к которому нужно применить фильтр</param>
        /// <param name="filter">Параметры фильтра</param>
        /// <returns></returns>
        Task<IQueryable<TDataModel>> ApplyAsync(IQueryable<TDataModel> queryable, TFilterContract filter);

        /// <summary>
        /// Применить страничник к запросу
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        Task<List<TDataModel>> ApplyPagerAsync(IQueryable<TDataModel> queryable, TFilterContract filter);

    }
}
