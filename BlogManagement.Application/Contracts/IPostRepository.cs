using BlogManagement.Common.Models;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPagedList<Post>> GetPostsForIndexAsync(Expression<Func<Post, bool>> expression, PagingRequest request);
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
