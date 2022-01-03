using System.Linq.Expressions;
using BlogManagement.Common.Models;
using BlogManagement.Data.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace BlogManagement.Contracts.Repositories
{
    /// <summary>
    /// Post repository that implement a generic repository
    /// </summary>
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// This method gets all posts of an author.
        /// </summary>
        /// <param name="authorId">Author Id to search</param>
        /// <returns>A list of Posts</returns>
        Task<IEnumerable<Post>> GetByAuthorId(long authorId);
        /// <summary>
        /// This method gets all posts with related data for displaying on Index pages.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPagedList<Post>> GetPostsForIndexAsync(PagingRequest request, Expression<Func<Post, bool>> expression = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<Post?> GetPostDetailsAsync(long postId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetAllPostIdsAndTitles();
        /// <summary>
        /// This method gets all post without using paging.
        /// </summary>
        /// <param name="expression">Delegate method for filtering out posts</param>
        /// <param name="includes">Related relationship of a post</param>
        /// <returns>List of posts</returns>
        Task<IEnumerable<Post>> GetAllPostWithoutPaging([CanBeNull] Expression<Func<Post, bool>> expression = null,
            [CanBeNull] List<string> includes = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<Post> CreatePostAsync(Post post, IFormFile formFile);

        /// <summary>
        /// This method update an existing post in DB
        /// </summary>
        /// <param name="post"></param>
        /// <param name="formFile">Image of a post to to update</param>
        /// <returns></returns>
        Task<bool> UpdatePostAsync(Post post, [CanBeNull] IFormFile formFile);
    }
}
