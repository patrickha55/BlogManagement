using BlogManagement.Common.Models.PostCommentVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts.Services
{
    public interface IPostCommentService
    {
        Task<bool> CreatePostCommentAsync(PostCommentCreateVM request, string userName);
        Task<SelectList> GetPostCommentsForSelectListAsync(long? parentId = null);
    }
}
