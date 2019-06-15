using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DivineDrinks.Data;
using DivineDrinks.Data.Interfaces;
using DivineDrinks.Data.Mock;
using DivineDrinks.Data.Models;
using DivineDrinks.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DivineDrinks
{
    public class Startup
    {
        private IConfiguration _config;
        
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _config = configuration;
          
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(_config.GetConnectionString("DbConnection"));
            });


            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication();

            services.AddTransient<IDrinkRepository, DrinkRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            
            


            app.UseMvc(routes => {
                routes.MapRoute(
                 name: "drinkdetails",
                 template: "Drinks/Details/{drinkId?}",
                 defaults: new { Controller = "Drinks", action = "Details" });

                routes.MapRoute(
                    name: "categoryfilter",
                    template: "Drinks/{action}/{category?}",
                    defaults: new { Controller = "Drinks", action = "List" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{Id?}");
                });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Something is wrong with your code!!!!");
            });

           
        }
    }
}
