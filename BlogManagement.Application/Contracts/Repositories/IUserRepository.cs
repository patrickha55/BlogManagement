using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlogManagement.Data.Entities;

namespace BlogManagement.Application.Contracts.Repositories
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
        /// <returns></returns>
        Task<List<User>> FindUsersAsync(Expression<Func<User,bool>> expression);
    }
}
