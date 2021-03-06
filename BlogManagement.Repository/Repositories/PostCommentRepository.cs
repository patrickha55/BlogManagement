using BlogManagement.Common.Common;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Repository.Repositories
{
    public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(BlogManagementContext context, ILogger<PostCommentRepository> logger) : base(context, logger)
        {
            Logger = logger;
        }

        public async Task<IEnumerable<PostComment>> GetAllIdAndNameWithoutPagingAsync()
        {
            try
            {
                return await Context.PostComments
                    .AsNoTracking()
                    .Select(pc => new PostComment()
                    {
                        Id = pc.Id,
                        Title = pc.Title
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllIdAndNameWithoutPagingAsync));
                throw;
            }
        }
    }
}
