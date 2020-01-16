using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PCShop.Data.Entities;

namespace PCShop.Services
{
    /// <summary>
    /// Catalog service
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Products</returns>
        Task<List<Product>> GetAllProductsAsync(CancellationToken token = default);

        /// <summary>
        /// Get product by identifier
        /// </summary>
        /// <returns>Product</returns>
        Task<Product> GetProductById(int id, CancellationToken token);
    }
}