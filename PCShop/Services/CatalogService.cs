using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PCShop.Data.Entities;
using PCShop.Data.Repository;

namespace PCShop.Services
{
    ///<inheritdoc />
    public class CatalogService : ICatalogService
    {
        #region properties

        private readonly IRepository<Product,int> _productRepository;
        private readonly IRepository<ProductComponent, int> _productComponentRepository;
        private readonly IMemoryCache _cache;

        #endregion

        #region ctor

        public CatalogService(IRepository<Product,int> productRepository, IRepository<ProductComponent, int> productComponentRepository, IMemoryCache cache)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productComponentRepository = productComponentRepository ?? throw new ArgumentNullException(nameof(productComponentRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        #endregion


        #region Methods

        ///<inheritdoc />
        public async Task<List<Product>> GetAllProductsAsync(CancellationToken token = default)
        {
            return await _productRepository.TableNoTracking.ToListAsync(token).ConfigureAwait(false);
        }

        ///<inheritdoc />
        public async Task<Product> GetProductById(int id, CancellationToken token)
        {
            var product = await _productRepository.TableNoTracking
                                                    .Include(x=>x.ProductComponent)
                                                    .ThenInclude(x=>x.ProductAttributesMap)
                                                    .ThenInclude(x=>x.ProductAttribute)
                                                    .FirstOrDefaultAsync(x => x.Id == id, token)
                                                    .ConfigureAwait(false);

            if (product?.ProductComponentId == null) return null;

            product.ProductComponent.ChildrenProductComponents = GetChildrenProductComponentsByProductComponentId((int)product.ProductComponentId);

            return product;
        }

        /// <summary>
        /// Get children product components of a product by product identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Collection of ProductComponent</returns>
        private List<ProductComponent> GetChildrenProductComponentsByProductComponentId(int id)
        {
            if (!_cache.TryGetValue("product_components", out var productComponents))
            {
                productComponents = _productComponentRepository.Table.Include(x => x.ChildrenProductComponents)
                                                                        .ThenInclude(x=>x.ProductAttributesMap)
                                                                        .ThenInclude(x=>x.ProductAttribute).ToList();

                _cache.Set("product_components", productComponents, TimeSpan.FromMinutes(10));
            }

            return ((List<ProductComponent>) productComponents).FirstOrDefault(x => x.Id == id)?.ChildrenProductComponents.ToList();
        }

        #endregion
        
    }
}