using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stationary_management_system.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using stationary.Models;

namespace stationary_management_system
{
    public class Startup
    {
        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("CollegeDbConnection")));
            services.AddControllersWithViews();

            services.AddScoped<IProductRepository, SQLProductRepository>();
            services.AddScoped<IBilllocalRepository, SQLBilllocalRepository>();
            services.AddScoped<IBillRepository, SQLBillRepository>();
            services.AddScoped<IBillItemRepository, SQLBillItemRepository>();
            services.AddScoped<IAdminRepository, SQLAdminRepository>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                option =>
                {
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    option.LoginPath = "/Login/Login";
                    option.AccessDeniedPath = "/Login/Login";
                });
            services.AddSession(
                option =>
                {
                    option.IdleTimeout = TimeSpan.FromMinutes(60);
                    option.Cookie.HttpOnly = true;
                    option.Cookie.IsEssential = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "",
                    new { controller = "Login", action = "Index" });
            });
        }
    }
}
