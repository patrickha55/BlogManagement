using System;
using BlogManagement.Data.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogManagement.Common.Models;
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
    }
}
