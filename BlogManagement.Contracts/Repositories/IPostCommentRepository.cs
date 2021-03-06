using BlogManagement.Data.Entities;

namespace BlogManagement.Contracts.Repositories
{
    /// <summary>
    /// Post comment repository that implement a generic repository
    /// </summary>
    public interface IPostCommentRepository : IRepository<PostComment>
    {
        /// <summary>
        /// This method get all post comment names and titles.
        /// </summary>
        /// <returns>PostComment entity</returns>
        Task<IEnumerable<PostComment>> GetAllIdAndNameWithoutPagingAsync();
    }
}
