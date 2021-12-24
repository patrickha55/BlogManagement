using BlogManagement.Application.Contracts;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BlogManagementContext context, ILogger<PostRepository> logger) : base(context, logger)
        {
        }
    }
}
