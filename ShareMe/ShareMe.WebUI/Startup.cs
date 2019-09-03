using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareMe.BLL.Interfaces.Services;
using ShareMe.BLL.Services;
using ShareMe.DAL.Interfaces.Context;
using ShareMe.DAL.Interfaces.Repositories;
using ShareMe.DAL.Repositories;
using ShareMe.WebUI.Configs.Mapping;

namespace ShareMe.WebUI
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
            string connection = Configuration.GetConnectionString("ShareMeDb");
            services
                .AddDbContext<ShareMeContext>(options =>
                options.UseSqlServer(connection));

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

            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPostTagService, PostTagService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIsEntityExistsService, IsEntityExistsService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ShareMeContext>();
                context.Database.EnsureCreated();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Post}/{action=Posts}");
            });
        }
    }
}
