using System.Linq.Expressions;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Data.Entities;

namespace BlogManagement.Contracts.Repositories
{
    /// <summary>
    /// Post meta repository that implement a generic repository
    /// </summary>
    public interface IPostMetaRepository : IRepository<PostMeta>
    {
        Task<IEnumerable<PostMeta>>
            GetPostMetasWithoutPagingAsync(Expression<Func<PostMeta, bool>> expression = null);
    }
}
