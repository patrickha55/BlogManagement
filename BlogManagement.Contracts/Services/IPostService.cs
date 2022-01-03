using BlogManagement.Common.Models;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Data.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogManagement.Contracts.Services
{
    public interface IPostService
    {
        /// <summary>
        /// This method gets all posts for index view model as well as an option to get all posts of an author.
        /// </summary>
        /// <param name="pagingRequest">PagingRequest object for paging</param>
        /// <param name="authorId">User's ID</param>
        /// <returns>List of PostForIndexVMs</returns>
        Task<List<PostForIndexVM>> GetPostsForIndexVMsAsync(PagingRequest pagingRequest, long? authorId = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<PostForAdminIndexVM>> GetPostForAdminIndexVMsAsync(int pageNumber = 1, int pageSize = 10);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Post> GetPostAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostVM> GetPostVMAsync(long id);
        /// <summary>
        /// This method gets a post along with its related information (author, categories, tags, comment, rating,...)
        /// </summary>
        /// <param name="id">ID of a post</param>
        /// <param name="userName">Username of the current logged in user to check if this person has rated the post</param>
        /// <returns>Return PostDetailVM object</returns>
        Task<PostDetailVM> GetPostDetailVMAsync(long id, string userName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostEditVM> GetPostEditVMsAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<PostVM> CreatePostAsync(PostCreateVM request, string userName);

        /// <summary>
        /// This method updates an existing post.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request">Post to update against</param>
        /// <returns>Return true if success, else return false</returns>
        Task<bool> UpdatePostAsync(long id, PostEditVM request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postVM"></param>
        /// <returns></returns>
        Task<bool> DeletePostAsync(Post post);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsPostExistAsync(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tagIds"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<(SelectList categories, SelectList tags, SelectList posts)> GetSelectListsForPostCreationAsync(
            long? categoryId = null, [CanBeNull] IEnumerable<long> tagIds = null, long? postId = null);
        /// <summary>
        /// This method increase a post view count by one.
        /// </summary>
        /// <param name="post">A Post to increase view</param>
        Task UpdatePostViewCountAsync(Post post);
    }
}
