using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogManagement.Common.Models;
using X.PagedList;

namespace BlogManagement.Application.Contracts.Repositories
{
    /// <summary>
    /// This class is a generic repository allows interacting with any properties in the DbContext.
    /// </summary>
    /// <typeparam name="TEntity">Domain class in the application</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// This get method allows you to get information base on what type of expression you choose and if you want to include any additional data with it.
        /// </summary>
        /// <param name="expression">Optional expression for filtering</param>
        /// <param name="request">Paging request model</param>
        /// <param name="includes">List of strings </param>
        /// <returns>A list of objects</returns>
        Task<IPagedList<TEntity>> GetAllAsync(PagingRequest request, Expression<Func<TEntity, bool>> expression = null, List<string> includes = null);

        /// <summary>
        /// This get method allows you to get information base on what type of expression you choose and if you want to include any additional data with it.
        /// </summary>
        /// <param name="expression">Lambda expression to filter out the information</param>
        /// <param name="includes">Additional information to include with this data (Optional)</param>
        /// <returns>A generic type object</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> expression,
            List<string> includes = null);

        /// <summary>
        /// This method is for creating a new entity. 
        /// </summary>
        /// <param name="entity">Generic class</param>
        /// <returns>Return true if success, else false</returns>
        Task<bool> CreateAsync(TEntity entity);
        /// <summary>
        /// This method is for updating an existing entity.
        /// </summary>
        /// <param name="entity">Generic class</param>
        /// <returns>Return true if success, else false</returns>
        Task<bool> UpdateAsync(TEntity entity);
        /// <summary>
        /// This method is for deleting an existing entity.
        /// </summary>
        /// <param name="entity">Generic class</param>
        /// <returns>Return true if success, else false</returns>
        Task<bool> DeleteAsync(TEntity entity);
        /// <summary>
        /// This method deletes a list of entities.
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>Return true if success, else false</returns>
        Task DeleteRangeAsync(List<TEntity> entities);
        /// <summary>
        /// This method checks if an entity existed in the database.
        /// </summary>
        /// <param name="id">Id of an entity</param>
        /// <returns>Return true if success, else false</returns>
        Task<bool> IsExistsAsync(long id);
    }
}
