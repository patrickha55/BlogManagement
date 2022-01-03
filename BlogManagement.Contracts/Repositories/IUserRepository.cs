using System.Linq.Expressions;
using BlogManagement.Data.Entities;

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
        /// <returns></returns>
        Task<List<User>> FindUsersAsync(Expression<Func<User,bool>> expression);
    }
}
