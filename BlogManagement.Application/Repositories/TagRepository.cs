using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts;
using BlogManagement.Common.Common;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly BlogManagementContext _context;
        private readonly ILogger<TagRepository> _logger;

        public TagRepository(BlogManagementContext context, ILogger<TagRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Tag>> GetAllTagIdsAndTitles()
        {
            try
            {
                return await _context.Tags
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
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetAllTagIdsAndTitles));
                throw;
            }
        }
    }
}
