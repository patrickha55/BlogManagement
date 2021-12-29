using BlogManagement.Common.Models.PostVMs;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Data.Entities;

namespace BlogManagement.Application.Contracts.Services
{
    public interface IPostService
    {
        Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(string userName = null, int pageNumber = 1, int pageSize = 10);
        Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(int pageNumber = 1, int pageSize = 10);
        Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName);
        Task<PostEditVM> GetPostEditVMsAsync(long id);
        Task<bool> CreatePostAsync(PostCreateVM request, string userName);
        Task<bool> UpdatePostAsync(long id, PostEditVM request);
        Task<bool> DeletePostAsync(long id);
        Task<bool> IsPostExistAsync(long id);
        Task<(SelectList categories, SelectList tags, SelectList posts)> GetSelectListsForPostCreationAsync(
            long? categoryId = null, [CanBeNull] IEnumerable<long> tagIds = null, long? postId = null);
        /// <summary>
        /// This method increase a post view count by one.
        /// </summary>
        /// <param name="post">A Post to increase view</param>
        Task UpdatePostViewCountAsync(Post post);
    }
}
