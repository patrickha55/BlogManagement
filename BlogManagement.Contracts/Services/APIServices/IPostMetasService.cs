using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Data.Entities;
using System.Linq.Expressions;
using BlogManagement.Common.Models;
using X.PagedList;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface IPostMetasService
    {
        Task<IPagedList<PostMetaVM>> GetPostMetaVMsAsync(PagingRequest pagingRequest, Expression<Func<PostMeta, bool>> expression = null);
        Task<IEnumerable<PostMetaVM>> GetPostMetaVMsWithoutPagingAsync(Expression<Func<Post, bool>> expression = null);
        Task<PostMetaVM> GetPostMetaVMAsync(long id);
        Task<PostMetaEditVM> GetPostMetaEditVMsAsync(long id);
        Task<bool> CreatePostMetaVMAsync(PostMetaCreateVM request);
        Task<bool> UpdatePostMetaAsync(long id, PostMetaEditVM request);
        Task<bool> DeletePostMetaAsync(long id);
    }
}
