using System.Linq.Expressions;
using BlogManagement.Common.Models;
using BlogManagement.Data.Entities;
using X.PagedList;

namespace BlogManagement.Contracts.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<User> FindUserDetailAsync(Expression<Func<User,bool>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        Task<IPagedList<User>> FindUsersAsync(Expression<Func<User,bool>> expression, PagingRequest pagingRequest);
    }
}
