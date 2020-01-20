using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCShop.Data;
using PCShop.Data.Entities;

namespace PCShop
{
    /// <summary>
    /// Helper Class for Seed Db 
    /// </summary>
    public static class MemoryDbInit
    {
        /// <summary>
        /// Seed db object with sample data from json file
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
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
                var productComponent = dbContext.Set<ProductComponent, int>();
                var productAttribute = dbContext.Set<ProductAttribute, int>();
                var productComponentAttributeMap = dbContext.Set<ProductComponentAttributeMap, int>();

                var jObjs = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(@"seed_data.json"));

                product.AddRange(jObjs[nameof(Product)].ToObject<Product[]>());

                productComponent.AddRange(jObjs[nameof(ProductComponent)].ToObject<ProductComponent[]>());

                productAttribute.AddRange(jObjs[nameof(ProductAttribute)].ToObject<ProductAttribute[]>());

                productComponentAttributeMap.AddRange(jObjs[nameof(ProductComponentAttributeMap)].ToObject<ProductComponentAttributeMap[]>());
                
                dbContext.SaveChanges();
            }
        }
    }
}