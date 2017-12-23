using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Services.Filters;
using System.Threading.Tasks;

namespace GuestBook.WebApi.Services
{
    /// <summary>
    /// Интерфейс сервиса типового ресурса
    /// </summary>
    /// <typeparam name="TContract">Тип модели документа</typeparam>
    /// <typeparam name="TEditContract">Тип модели документа, который содержит только те поля, которые можно изменять.</typeparam>
    /// <typeparam name="TFilterContract">Тип модели фильтра, которая содержит параметры для фильтрации</typeparam>
    /// <typeparam name="TDataModel">Тип модели данных, к который применяется фильтр. Обычно это модель,
    /// в которой данные храняться в базе данных
    /// </typeparam>
    public interface IEndpointService<TContract, TEditContract, TFilterContract, TDataModel>
        where TContract : class
        where TEditContract : class
        where TFilterContract : IFilterContract
    {
        /// <summary>
        /// Получить список документов ресурса.
        /// </summary>
        /// <param name="filterModel">Фильтр, который нужно применить к ресурсу.</param>
        /// <returns></returns>
        Task<ItemList<TContract>> ListAsync(TFilterContract filterModel);

        /// <summary>
        /// Получить документ по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор документа</param>
        /// <returns></returns>
        Task<TContract> GetAsync(string id);

        /// <summary>
        /// Добавить новый документ в ресурс.
        /// </summary>
        /// <param name="model">Модель документа</param>
        /// <returns></returns>
        Task<TContract> AddAsync(TEditContract model);

        /// <summary>
        /// Обновить существующий документ по его идентификатору.
        /// </summary>
        /// <exception cref="WebApiNotFoundException">Если документ c идентификатором 
        /// <paramref name="id"/> не существует</exception>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="model">Модель документа</param>
        /// <returns></returns>
        Task<TContract> UpdateAsync(string id, TEditContract model);

        /// <summary>
        /// Удалить существующий элемент.
        /// </summary>
        /// <exception cref="WebApiNotFoundException">Если документ c идентификатором 
        /// <paramref name="id"/> не существует</exception>
        /// <param name="id">Идентификатор документа</param>
        /// <returns></returns>
        Task DeleteAsync(string id);
    }
}
