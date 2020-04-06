using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartSportStore.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace StartSportStore
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
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductReprository, EFProductReprository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IWebServiceRepository, WebServiceRepository>();
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Configuration["Data:SportStoreProducts:ConnectionString"];
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });
            services.AddSession(options => {
                options.Cookie.Name = "StartSportStore.Session";
                options.IdleTimeout = TimeSpan.FromHours(48);
                options.Cookie.HttpOnly = false;
            });
            services.Configure<CookiePolicyOptions>(options =>
            {

                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().AddJsonOptions(options=>options.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Serialize).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: null, template: "{Controller=Product}/{Action=UpdateProduct}/{id:int}");
                routes.MapRoute(name: null, template: "{category}/page{productPage:int}", defaults: new { Controller = "Product", Action = "List" });
                routes.MapRoute(name: "Pagination", template: "products/page{productPage}", defaults: new { Controller = "Product", Action = "Index" });
                routes.MapRoute(name: "product", template: "{Controller=Product}/{Action=List}");
            });
        }
    }
}
