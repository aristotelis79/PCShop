using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCShop.Data.Entities;

namespace PCShop.Data
{
    /// <summary>
    /// Represents Interface of application DB context
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="T">Struct of Id of Base entity</typeparam>
        /// <returns>A set for the given entity type</returns>
        DbSet<TEntity> Set<TEntity,T>() where TEntity : BaseEntity<T> where T :struct;
    }
}