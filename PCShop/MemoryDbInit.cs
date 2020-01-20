using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PCShop.Data;
using PCShop.Data.Entities;

namespace PCShop
{
    public static class MemoryDbInit
    {
        public static void SeedMemoryDb(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<PcShopContext>();

                // Look for any board games already in database.
                var product = dbContext.Set<Product, int>();
                if (product.Any())
                {
                    return;
                }

                product.AddRange(
                    new Product
                    {
                        Id = 1,
                        Name = "Workstation 1",
                        ProductComponentId = 1
                    });

                var productComponent = dbContext.Set<ProductComponent, int>();

                productComponent.AddRange(new ProductComponent
                {
                    Id = 1,
                    Name = "Workstation",
                    ParentProductComponentId = null
                });


                var productAttribute = dbContext.Set<ProductAttribute, int>();

                productAttribute.AddRange(new ProductAttribute
                {
                    Id = 1,
                    Name = "OS",
                    Unit = "OS"
                });

                var productComponentAttributeMap = dbContext.Set<ProductComponentAttributeMap, int>();

                productComponentAttributeMap.AddRange(new ProductComponentAttributeMap
                {
                    Id = 1,
                    ProductAttributeId = 1,
                    ProductComponentId = 1
                });

                dbContext.SaveChanges();
            }
        }
    }
}