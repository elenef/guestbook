using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using GuestBook.WebApi.Identity;
using GuestBook.WebApi.Identity.Policies;

namespace GuestBook.WebApi
{
    public partial class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        private IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .AddViewLocalization()
            .AddDataAnnotationsLocalization();
            services.Configure<MvcOptions>(opt =>
            {
                opt.OutputFormatters.RemoveType<JsonOutputFormatter>();
                opt.OutputFormatters.Add(new JsonOutputFormatter(Config.JsonSerializerSettings, ArrayPool<char>.Shared));
            });

            var connectionString = Configuration["Data:Database:ConnectionString"];
            services.AddDbContext<Identity.IdentityContext>(opt => opt.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Identity.IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Policies.DefaultAuthorizationPolicy,
                    policy => policy.AddRequirements(new DefaultEndpointRequirement()));

                opt.DefaultPolicy = opt.GetPolicy(Policies.DefaultAuthorizationPolicy);
            });

            services.AddSingleton<IAuthorizationHandler, DefaultEndpointHandler>();

            ConfigureIdentityServer(services, _environment);
            ConfigureEndpointServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            /*loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Debug);

            app.UseCors("AllowAll");

            ConfigureIdentity(app);

            CreateDefaultUsers(app);

            app.UseMvc();*/

            loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                //builder.WithOrigins(Configuration["Data:AllowOriginFor"])
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            ConfigureIdentity(app);

            app.UseMvc();

            CreateDefaultUsers(app);
        }

        private void ConfigureIdentity(IApplicationBuilder app)
        {
            var authorityServer = Configuration["Data:Identity:AuthorityServer"];

            app.Use(next => async context =>
            {
                await next(context);
                if (context.Items.ContainsKey(DefaultEndpointHandler.EndpointRequirementFailedField))
                {
                    context.Response.StatusCode = 401;
                }
            });

            app.UseIdentityServerAuthentication(
                new IdentityServerAuthenticationOptions
                {
                    Authority = authorityServer,
                    RequireHttpsMetadata = !(_environment.IsDevelopment() || _environment.IsEnvironment("Debug")),
                    ApiName = "Main",
                });

            app.UseIdentity();
            app.UseIdentityServer();
        }

        private void ConfigureIdentityServer(IServiceCollection services, IHostingEnvironment env)
        {
            var certFile = Path.Combine(env.ContentRootPath, Configuration["Data:Identity:CertificateFile"]);
            var certPass = Configuration["Data:Identity:CertificatePassword"];




            try
            {
                var serverBuilder = services.AddIdentityServer();

                if (_environment.IsProduction() || _environment.EnvironmentName == "Release")
                {
                    var jwtSigningCert = new X509Certificate2(certFile, certPass);
                    serverBuilder.AddSigningCredential(jwtSigningCert);
                }
                else
                {
                    serverBuilder.AddTemporarySigningCredential();
                }

                serverBuilder
                    .AddInMemoryIdentityResources(IdentityResources.Get())
                    .AddInMemoryApiResources(ApiResources.Get())
                    .AddInMemoryClients(Clients.Get())
                    .AddAspNetIdentity<User>();
            }
            catch (Exception ex)
            {
                throw new Exception($"File path: {certFile}", ex);
            }
        }
    }
}
