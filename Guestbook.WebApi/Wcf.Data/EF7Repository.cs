using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GuestBook.Data
{
    /// <summary>
    /// Generic implementation of database based data storage
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class EF7Repository<TItem> : IRepository<TItem>
        where TItem : class, IModel
    {
        private DbSet<TItem> _dbSet;
        private DbContext _dbContext;
        protected IRepositoryFilter _filter;

        public EF7Repository(DbContext dbContext, IRepositoryFilter filter = null)
        {
            _dbSet = dbContext.Set<TItem>();
            _dbContext = dbContext;
            _filter = filter;
        }

        public IQueryable<TItem> Items
        {
            get
            {
                return _filter != null
                    ? _filter.Apply(_dbSet).Cast<TItem>()
                    : _dbSet;
            }
        }

        public async Task AddAsync(TItem item)
        {
            _dbSet.Add(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TItem item)
        {
            _dbSet.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(TItem item)
        {
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TItem> FindByIdAsync(string id, params Expression<Func<TItem, object>>[] includeProperties)
        {
            var query = GetQueryWithInclude(includeProperties);

            return await query
                .Where(i => i.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public IQueryable<TItem> GetRole(params Expression<Func<TItem, object>>[] includeProperties)
        {
            var query = GetQueryWithInclude(includeProperties);

            return query
                .Where(i => i.Id != null)
                .AsNoTracking();
        }

        public async Task<List<TItem>> FindByPageAsync(Expression<Func<TItem, bool>> predicate, int pageIndex,
            int pageSize, params Expression<Func<TItem, object>>[] includeProperties)
        {
            var query = GetQueryWithInclude(includeProperties);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        private IQueryable<TItem> GetQueryWithInclude(Expression<Func<TItem, object>>[] includeProperties)
        {
            if (includeProperties == null)
            {
                return _dbSet.AsNoTracking();
            }

            var query = includeProperties
                .Aggregate(_dbSet.AsNoTracking(), (c, p) => c.Include(p))
                .AsQueryable();
            return query;
        }
    }
}
