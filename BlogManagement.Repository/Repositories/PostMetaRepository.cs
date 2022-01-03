using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Repository.Repositories
{
    public class PostMetaRepository : Repository<PostMeta>, IPostMetaRepository
    {
        public PostMetaRepository(BlogManagementContext context, ILogger<PostMetaRepository> logger) : base(context, logger)
        {
        }
    }
}
