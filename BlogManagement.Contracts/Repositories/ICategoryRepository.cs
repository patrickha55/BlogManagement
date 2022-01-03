using BlogManagement.Data.Entities;

namespace BlogManagement.Contracts.Repositories
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
