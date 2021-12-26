using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Application.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly BlogManagementContext _context;

        public CategoryRepository(BlogManagementContext context, ILogger<CategoryRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<Category>> GetAllIdAndNameWithoutPagingAsync()
        {
            try
            {
                return await _context.Categories
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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllIdAndNameWithoutPagingAsync));
                throw;
            }
        }
    }
}
