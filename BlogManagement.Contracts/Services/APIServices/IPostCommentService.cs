using BlogManagement.Common.Models.PostCommentVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface IPostCommentService
    {
        Task<PostCommentVM?> GetPostCommentVMAsync(long id);
        Task<SelectList> GetPostCommentsForSelectListAsync(long? parentId = null);
        Task<PostCommentVM?> CreatePostCommentAsync(PostCommentCreateVM request);
    }
}
