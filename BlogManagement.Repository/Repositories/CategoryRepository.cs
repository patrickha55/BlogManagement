using BlogManagement.Common.Common;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Repository.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogManagementContext context, ILogger<CategoryRepository> logger) : base(context, logger)
        {
            Logger = logger;
        }


        public async Task<IEnumerable<Category>> GetAllIdAndNameWithoutPagingAsync()
        {
            try
            {
                return await Context.Categories
                    .AsNoTracking()
                    .Select(c => new Category
                    {
                        Id = c.Id,
                        Title = c.Title
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
