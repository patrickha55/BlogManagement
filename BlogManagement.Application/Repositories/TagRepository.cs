using BlogManagement.Application.Contracts;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(BlogManagementContext context, ILogger<TagRepository> logger) : base(context, logger)
        {
        }
    }
}
