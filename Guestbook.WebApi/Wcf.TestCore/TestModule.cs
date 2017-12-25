using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services.Filters;
using Moq;
using Ninject.Activation;
using Ninject.Modules;
using System.Data.Entity;

namespace GuestBook.TestCore
{
    public class TestModule : NinjectModule
    {
        private DbContext _context;

        public Mock<IUserContext> UserContext;
        public Mock<IRepository<User>> UserRepositoryMock;
        public Mock<IRepository<UserRoles>> RolesRepositoryMock;

        public TestModule()
        {
            UserRepositoryMock = new Mock<IRepository<User>>();
            RolesRepositoryMock = new Mock<IRepository<UserRoles>>();
            UserContext = new Mock<IUserContext>();
        }

        public override void Load()
        {
            //Модуль создается для каждого теста. Лучше всего использовать InSingletonScope()
            Bind<DbContext>().ToMethod(CreateContext).InSingletonScope();

            AddRepository<ExampleModel>();

            Bind<IContractMapper>().To<ContractMapper>().InSingletonScope();
        }

        private DbContext CreateContext(IContext context)
        {
            if (_context == null)
            {
                var dbContext = new DomainContext();
                dbContext.Database.Delete();
                dbContext.Database.Create();
                _context = dbContext;
            }

            return _context;
        }

        private void AddRepository<T>()
            where T : class, IModel
        {
            Bind<IRepository<T>>().To<Repository<T>>().InSingletonScope();
        }

        public override void Dispose(bool disposing)
        {
            if (_context != null)
            {
                _context.Database.Delete(); // Remove database after test
                _context.Dispose();
                _context = null;
            }

            base.Dispose(disposing);
        }
    }
}
