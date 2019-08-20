﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyManager.DAL.Migrations.DataSeeding;
using MoneyManager.DAL.Models.Contexts;

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

            string connection = Configuration.GetConnectionString("MoneyManagerCodeFirst");
            services
                .AddDbContext<MoneyManagerCodeFirstContext>(options =>
                options.UseSqlServer(connection));

            services
                .AddSingleton<IDataSeeding, DataSeeding>();

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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
