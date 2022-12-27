using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OceanStore.DataAccesLayer.DataContext;
using OceanStore.BusinessLayer;
using Microsoft.EntityFrameworkCore;
using OceanStore.BusinessLayer.Managers;
using Microsoft.AspNetCore.Identity;
using OceanStore.DataAccesLayer.Models;
//using OceanStore.BusinessLayer.Interfaces;
//using OceanStore.BusinessLayer.Repositorys;

namespace OceanStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbCotext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Context"));
            });
            services.AddIdentity<User, IdentityRole>(IdentityOptions =>
            {
                IdentityOptions.Password.RequiredLength = 8;
                IdentityOptions.Password.RequireNonAlphanumeric = false;
                IdentityOptions.Password.RequireUppercase = true;
                IdentityOptions.Password.RequireLowercase = true;
                IdentityOptions.Lockout.MaxFailedAccessAttempts = 4;
                IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
                IdentityOptions.Lockout.AllowedForNewUsers = true;
                IdentityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                IdentityOptions.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbCotext>();
            services.AddScoped<ProductManager>();
            services.AddScoped<AccountManager>();
            services.AddScoped<UserAppManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
