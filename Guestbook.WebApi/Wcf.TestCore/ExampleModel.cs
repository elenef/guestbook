using GuestBook.Data;
using System.Data.Common;
using System.Data.Entity;

namespace GuestBook.TestCore
{
    public class ExampleModel : IModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public ExampleModel()
        {
            Id = IdentityGenerator.NewId();
        }
    }

    public class ExampleDbContext : DbContext
    {
        public DbSet<ExampleModel> ExampleModels { get; set; }

        public ExampleDbContext()
            :base("Default")
        {

        }
        public ExampleDbContext(string connectionString)
            : base(connectionString)
        {

        }

        public ExampleDbContext(DbConnection connection, bool ownedConnection)
            : base(connection, ownedConnection)
        {

        }
    }
}
