using System.Data.Entity;

namespace GuestBook.Domain
{
    /// <summary>
    /// контекст базы данных
    /// </summary>
    public class DomainContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<RegisteredUser> RegisteredUser { get; set; }


        public DomainContext()
           : base("ReviewsDb")
        {
        }

        public DomainContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        /// <summary>
        /// в этом методе описывается создание моделей и настройка полей
        /// </summary>
        /// <param name="mb">модель БД</param>
        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Entity<Restaurant>()
                .HasMany(l => l.Reviews);

            mb.Entity<RegisteredUser>()
                .HasMany(l => l.Restaurants);
        }
    }
}
