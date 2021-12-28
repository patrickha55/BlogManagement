using BlogManagement.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts
{
    /// <summary>
    /// Category repository that implement a generic repository
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {

        /// <summary>
        /// This method get all category names and titles.
        /// </summary>
        /// <returns>A category object</returns>
        Task<IEnumerable<Category>> GetAllIdAndNameWithoutPagingAsync();
    }
}
