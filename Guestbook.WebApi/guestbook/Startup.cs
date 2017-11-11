using GuestBook.Mapper;
using GuestBook.Models;
using GuestBook.Models.Contracts;
using GuestBook.Repositories;
using GuestBook.Services;
using GuestBook.Services.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GuestBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["Data:Database:ConnectionString"];
            services.AddDbContext<DomainContext>(opt => opt.UseSqlServer(connectionString));

            services.AddMvc();

            services.AddScoped<IContractMapper, ContractMapper>();

            AddRepositores(services);

            AddServices(services);

            AddFilters(services);

            AddControllerServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void AddRepositores(IServiceCollection services)
        {
            AddEF7Repository<User>(services);
            AddEF7Repository<Restaurant>(services);
            AddEF7Repository<Review>(services);
        }

        private void AddServices(IServiceCollection services)
        {

        }

        private void AddFilters(IServiceCollection services)
        {

        }

        private void AddControllerServices(IServiceCollection services)
        {

        }

        private void AddEF7Repository<T>(IServiceCollection services)
            where T : class, IModel
        {
            services.AddScoped<IRepository<T>>(p => new EF7Repository<T>(p.GetService<DomainContext>()));
        }
    }
}
