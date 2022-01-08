using System.Linq.Expressions;
using BlogManagement.Common.Common;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Repository.Repositories
{
    public class PostMetaRepository : Repository<PostMeta>, IPostMetaRepository
    {
        public PostMetaRepository(BlogManagementContext context, ILogger<PostMetaRepository> logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<PostMeta>> GetPostMetasWithoutPagingAsync(Expression<Func<PostMeta, bool>> expression = null)
        {
            try
            {
                IQueryable<PostMeta> query = Context.PostMetas;

                if (expression is not null)
                {
                    query = query.Where(expression);
                }

                return await query.AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetPostMetasWithoutPagingAsync));
                throw;
            }
        }
    }
}
