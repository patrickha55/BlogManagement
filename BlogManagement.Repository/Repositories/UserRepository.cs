using BlogManagement.Common.Common;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BlogManagement.Repository.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BlogManagementContext context, ILogger<Repository<User>> logger) : base(context, logger)
        {
            Logger = logger;
        }


        public async Task<User> FindUserDetailAsync(Expression<Func<User, bool>> expression)
        {
            try
            {
                return (await Context.Users
                    .AsNoTracking()
                    .Include(u => u.PostComments)
                    .Include(u => u.PostUserRatings)
                    .Include(u => u.Posts)
                    .SingleOrDefaultAsync(expression))!;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindUserDetailAsync));
                throw;
            }
        }

        public async Task<List<User>> FindUsersAsync(Expression<Func<User, bool>> expression)
        {
            try
            {
                return await Context.Users
                    .Where(expression)
                    .AsNoTracking()
                    .Select(u => new User
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        ImageUrl = u.ImageUrl,
                        Intro = u.Intro
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(FindUserDetailAsync));
                throw;
            }
        }
    }
}
