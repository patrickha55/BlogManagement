using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Data.Entities;
using System.Linq.Expressions;
using BlogManagement.Common.Models;
using X.PagedList;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface IPostMetaService
    {
        Task<IPagedList<PostMetaVM>> GetPostMetaVMsAsync(PagingRequest pagingRequest, Expression<Func<PostMeta, bool>> expression = null);
        Task<IEnumerable<PostMetaVM>> GetPostMetaVMsWithoutPagingAsync(Expression<Func<PostMeta, bool>> expression = null);
        Task<PostMeta> GetPostMetaAsync(long id);
        Task<PostMetaVM> GetPostMetaVMAsync(long id);
        Task<PostMetaEditVM> GetPostMetaEditVMsAsync(long id);
        Task<PostMetaVM> CreatePostMetaVMAsync(PostMetaCreateVM request);
        Task<bool> UpdatePostMetaAsync(long id, PostMetaEditVM request);
        Task<bool> DeletePostMetaAsync(PostMeta postMeta);
    }
}
