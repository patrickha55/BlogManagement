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
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly BlogManagementContext _context;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(
            BlogManagementContext context,
            ILogger<PostRepository> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Post>> GetByAuthorId(long authorId)
        {
            try
            {
                var posts = await _context.Posts
                    .Where(p => p.AuthorId == authorId)
                    .ToListAsync();

                if (posts is null)
                    throw new ArgumentException(Constants.InvalidArgument);

                return posts;
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "{0} {1}", Constants.InvalidArgument, nameof(GetByAuthorId));
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0} {1}", Constants.ErrorMessageLogging, nameof(GetByAuthorId));
                throw;
            }
        }
    }
}
