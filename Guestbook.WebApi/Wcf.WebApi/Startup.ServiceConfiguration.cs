using GuestBook.Data;
using GuestBook.Domain;
using GuestBook.WebApi.Contracts;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Mapper;
using GuestBook.WebApi.Services;
using GuestBook.WebApi.Services.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GuestBook.WebApi
{
    public partial class Startup
    {
        private void ConfigureEndpointServices(IServiceCollection services)
        {
            AddIdentity(services);
            AddRepositories(services);
            AddControllerServices(services);
            AddDomainServices(services);
            AddTools(services);
            AddOptionsAccessors(services);
            AddFilters(services);
        }

        private void AddIdentity(IServiceCollection services)
        {
            var connectionString = Configuration["Data:Database:ConnectionString"];

            //services.AddScoped(f => new IdentityContext(new DbContextOptions<IdentityContext>()));
            services.AddScoped<UserManager<User>>();

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<UserContext>();

            services.AddScoped(opt => new DomainContext(connectionString));

            services.AddSingleton<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<UserContext>();
        }

        private void AddControllerServices(IServiceCollection services)
        {
            services.AddScoped<BaseEndpoinFilter<BaseFilterContract, User>>();

            services.AddScoped<IProfilesService, ProfilesService>();
            services.AddScoped<ProfilesService>();

            services.AddScoped<IEndpointService<RegisteredUserContract, EditRegisteredUserContract, RegisteredUserFilterContract, RegisteredUser>, RegisteredUserEndpointService>();
            services.AddScoped<RegisteredUserEndpointService>();

            services.AddScoped(opt => new RestaurantEndpointService(
                opt.GetService<Repository<Restaurant>>(),
                opt.GetService<IContractMapper>(),
                opt.GetService<RestaurantEndpointFilter>(),
                opt.GetService<Repository<Review>>(),
                opt.GetService<IUserContext>(),
                opt.GetService<IRepository<RegisteredUser>>()));

            services.AddScoped(opt => new ReviewEndpointService(
                opt.GetService<Repository<Review>>(),
                opt.GetService<IContractMapper>(),
                opt.GetService<ReviewEndpointFilter>(),
                opt.GetService<Repository<Restaurant>>()));

            services.AddScoped<IEndpointService<RestaurantContract, EditRestaurantContract, RestaurantFilterContract, Restaurant>, RestaurantEndpointService>();
            services.AddScoped<RestaurantEndpointService>();

            services.AddScoped<IEndpointService<ReviewContract, EditReviewContract, ReviewFilterContract, Review>, ReviewEndpointService>();
            services.AddScoped<ReviewEndpointService>();

            services.AddScoped<IUserService, UsersEndpointService>();
            services.AddScoped<UsersEndpointService>();
        }

        private void AddDomainServices(IServiceCollection services)
        {
        }
        private void AddFilters(IServiceCollection services)
        {
            services.AddScoped<IEndpointFilter<RegisteredUserFilterContract, RegisteredUser>, RegisteredUserEndpointFilter>();
            services.AddScoped<RegisteredUserEndpointFilter>();

            services.AddScoped<IEndpointFilter<RestaurantFilterContract, Restaurant>, RestaurantEndpointFilter>();
            services.AddScoped<RestaurantEndpointFilter>();

            services.AddScoped<IEndpointFilter<ReviewFilterContract, Review>, ReviewEndpointFilter>();
            services.AddScoped<ReviewEndpointFilter>();

            services.AddScoped<BaseEndpoinFilter<BaseFilterContract, User>>();

            services.AddScoped<IEndpointFilter<UserFilterContract, User>, UserEndpointFilter>();
            services.AddScoped<UserEndpointFilter>();
        }

        private void AddRepositories(IServiceCollection services)
        {
            AddEF7Repository<User>(services);
            AddEF7Repository<UserRoles>(services);

            AddRepository<RegisteredUser>(services);
            AddRepository<Restaurant>(services);
            AddRepository<Review>(services);
        }

        private void AddOptionsAccessors(IServiceCollection services)
        {
        }

        private void AddTools(IServiceCollection services)
        {
            services.AddScoped<IContractMapper, ContractMapper>();
        }

        private void AddRepository<T>(IServiceCollection services)
            where T : class, IModel
        {
            services.AddScoped<IRepository<T>>(p => new Repository<T>(p.GetService<DomainContext>()));
        }

        private void AddEF7Repository<T>(IServiceCollection services)
            where T : class, IModel
        {
            services.AddScoped<IRepository<T>>(p => new EF7Repository<T>(p.GetService<IdentityContext>()));
        }
    }
}
