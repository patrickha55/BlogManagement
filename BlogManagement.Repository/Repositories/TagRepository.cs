using BlogManagement.Common.Common;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Repository.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {

        public TagRepository(BlogManagementContext context, ILogger<TagRepository> logger) : base(context, logger)
        {
            Logger = logger;
        }

        public async Task<IEnumerable<Tag>> GetAllTagIdsAndTitles()
        {
            try
            {
                return await Context.Tags
                    .AsNoTracking()
                    .Select(t => new Tag
                    {
                        Id = t.Id,
                        Title = t.Title
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllTagIdsAndTitles));
                throw;
            }
        }
    }
}
