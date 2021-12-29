using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogManagement.Common.Models;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace BlogManagement.Application.Contracts.Repositories
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
        Task<Post> GetPostDetailsAsync(long postId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetAllPostIdsAndTitles();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<bool> CreatePostAsync(Post post, IFormFile formFile);
    }
}
