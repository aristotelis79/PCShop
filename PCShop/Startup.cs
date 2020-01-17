using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PCShop.Data;
using PCShop.Data.Entities;
using PCShop.Data.Repository;
using PCShop.Services;
using FluentValidation.AspNetCore;
using PCShop.Validations;

namespace PCShop
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

            #region DI

            services.AddScoped<IDbContext, PcShopContext>()
                .AddScoped<IRepository<Product, int>, EfRepository<Product, int>>()
                .AddScoped<IRepository<ProductComponent, int>, EfRepository<ProductComponent, int>>()
                .AddScoped<IRepository<ProductAttribute, int>, EfRepository<ProductAttribute, int>>()
                .AddScoped<ICatalogService, CatalogService>()
                .AddMemoryCache();

            #endregion

            #region Db

            services.AddDbContext<PcShopContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);


            #endregion

            #region MVC

            services.AddMvc()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<ProductAttributeModelValidation>();
                        fv.ImplicitlyValidateChildProperties = true;
                    })
#if (DEBUG)
            .AddRazorRuntimeCompilation();
#endif
            #endregion

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalog}/{action=Index}/{id?}");
            });

            #region Db
            //just for automate seed values
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<PcShopContext>();
                dbContext.Database.EnsureCreated();

                var result = dbContext.Set<ProductComponent>().Find(new object[]{1});
                if(result != null) return;
                
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "seed_data.sql");
                var sql = File.Exists(filepath) ? File.ReadAllText(filepath) : string.Empty;
                dbContext.Database.ExecuteSqlRaw(sql);
            }

            #endregion
        }
    }
}
