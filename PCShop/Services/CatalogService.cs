using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCShop.Data;
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
        #endregion

        #region ctor

        public CatalogService(IRepository<Product,int> productRepository, IRepository<ProductComponent, int> productComponentRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productComponentRepository = productComponentRepository;
        }

        #endregion


        #region Methods

        ///<inheritdoc />
        public async Task<IList<Product>> GetAllProductsAsync(CancellationToken token = default)
        {
            return await _productRepository.TableNoTracking.ToListAsync(token).ConfigureAwait(false);
        }

        ///<inheritdoc />
        public async Task<Product> GetProductById(int id, CancellationToken token)
        {
            //var pcc = await _productComponentRepository.Table.Include(x => x.ChildrenProductComponents.Where(p=>p.ParentProductComponentId==null)).ToListAsync();

            var pc = _productComponentRepository.Table.Where(x=>x.Id == 1).Include(x=>x.ChildrenProductComponents);
                var pcc = pc.ToList();
                var cc = pcc.Select(x => x.ChildrenProductComponents).ToList();
                var pccc = pcc.SelectMany(x=>x.ProductAttributesMap.Select(p=>p.ProductAttribute)).ToList();

            var r = await _productRepository.Table
                                                            .Include(x => x.ProductComponent.ProductAttributesMap)
                                                            .ThenInclude(x => x.ProductAttribute)

                                                            .Include(x => x.ProductComponent.ChildrenProductComponents)
                                                            .ThenInclude(x=>x.ProductAttributesMap)
                                                            .ThenInclude(x=>x.ProductAttribute)

                                                            .Include(x=>x.ProductComponent)
                                                            .ThenInclude(x=>x.ChildrenProductComponents)
                                                            .ThenInclude(x=>x.ChildrenProductComponents)
                                                            .ThenInclude(x=>x.ProductAttributesMap)
                                                            .ThenInclude(x=>x.ProductAttribute)

                                                            .FirstOrDefaultAsync(x => x.Id == id, token)
                                                            .ConfigureAwait(false);

            return r;
        }

        #endregion
        
    }
}