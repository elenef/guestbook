using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wcf.Data
{
    /// <summary>
    /// Generic implementation of database based data storage
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ARepository<TItem> : IRepository<TItem>
        where TItem : class, IModel
    {
        private DbSet<TItem> _dbSet;
        private DbContext _dbContext;
        protected IRepositoryFilter _filter;


        public ARepository(DbContext dbContext, IRepositoryFilter filter = null)
        {
            _dbSet = dbContext.Set<TItem>();
            _dbContext = dbContext;
            _filter = filter;
        }

        IQueryable<TItem> IRepository<TItem>.Items
        {
            get
            {
                return _filter != null
                  ? _filter.Apply(_dbSet).Cast<TItem>() : _dbSet;
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
            _dbSet.Attach(item);
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TItem> FindByIdAsync(string id,
            params Expression<Func<TItem, object>>[] includeProperties)
        {
            var query = GetQueryWithInclude(includeProperties);

            return await query
                .Where(i => i.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        private IQueryable<TItem> GetQueryWithInclude(Expression<Func<TItem, object>>[] includeProperties)
        {
            if (includeProperties == null)
            {
                return _dbSet.AsNoTracking();
            }

            var query = includeProperties
                .Aggregate(_dbSet.AsNoTracking(), (c, p) => (DbQuery<TItem>)c.Include(p))
                .AsQueryable();
            return query;
        }
    }
}
