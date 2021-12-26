using BlogManagement.Application.Contracts;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Repositories
{
    public class PostMetaRepository : Repository<PostMeta>, IPostMetaRepository
    {
        public PostMetaRepository(BlogManagementContext context, ILogger<PostMetaRepository> logger) : base(context, logger)
        {
        }
    }
}
