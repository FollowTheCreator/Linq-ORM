using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.BLL.Interfaces.Services;
using MoneyManager.BLL.Services;
using MoneyManager.BLL.DataSeeding;
using MoneyManager.DAL.Interfaces.DataSeeding;
using MoneyManager.DAL.Interfaces.Repositories;
using MoneyManager.DAL.Models.Contexts;
using MoneyManager.DAL.Repositories;
using MoneyManager.WebUI.Configs.Mapping;
using Utils;

namespace MoneyManager.WebUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connection = Configuration.GetConnectionString("MoneyManagerDb");
            services
                .AddDbContext<MoneyManagerContext>(options =>
                options.UseSqlServer(connection));

            services.AddSingleton<IDataSeeding, DataSeeding>();

            services.AddSingleton(typeof(Coder));
            services.AddSingleton(typeof(Generate));

            var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new WebUIMappingProfile());
                    mc.AddProfile(new BLLMappingProfile());
                }
            );
#if DEBUG
            mappingConfig.AssertConfigurationIsValid();
#endif
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IQueriesService, QueriesService>();
            services.AddScoped<IConfigService, ConfigService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var serviceProvider = services.BuildServiceProvider();

            var dataSeeding = serviceProvider.GetService<IDataSeeding>();
            dataSeeding.SeedData().GetAwaiter().GetResult();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=GetRecordsAsync}");
            });
        }
    }
}
