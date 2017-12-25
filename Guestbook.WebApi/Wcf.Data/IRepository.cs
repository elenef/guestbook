using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GuestBook.Data
{
    /// <summary>
    /// Represents abstraction of data storage
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IRepository<TItem>
    {
        /// <summary>
        /// Return list of exist items from storage
        /// </summary>
        IQueryable<TItem> Items { get; }

        /// <summary>
        /// Add new item into storage
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddAsync(TItem item);

        /// <summary>
        /// Update exist item in storage
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateAsync(TItem item);

        /// <summary>
        /// Remove exist item from storage
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task RemoveAsync(TItem item);

        /// <summary>
        /// Find item by id in storage
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<TItem> FindByIdAsync(string id,
            params Expression<Func<TItem, object>>[] includeProperties);
    }
}
