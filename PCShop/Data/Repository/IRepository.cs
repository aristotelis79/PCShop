using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PCShop.Data.Entities;

namespace PCShop.Data.Repository
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="T">Primitive type for Id of Entity</typeparam>
    public interface IRepository<TEntity, in T> where TEntity : BaseEntity<T> where T : struct
    {
        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="token">Cancellation token of action</param>
        /// <returns>Number of effective rows</returns>
        Task<int> InsertAsync(TEntity entity, CancellationToken token = default);
    }
}