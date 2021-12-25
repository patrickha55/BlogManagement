using BlogManagement.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetByAuthorId(long authorId);
    }
}
