using BlogManagement.Application.Contracts;
using BlogManagement.Application.Repositories;
using BlogManagement.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogManagementContext _context;
        private IPostRepository _postsRepository;
        private ITagRepository _tagRepository;
        private readonly ILogger<PostRepository> _postLogger;
        private readonly ILogger<TagRepository> _tagLogger;
        public UnitOfWork(
            BlogManagementContext context,
            ILogger<PostRepository> postLogger,
            ILogger<TagRepository> tagLogger)
        {
            _context = context;
            _postLogger = postLogger;
            _tagLogger = tagLogger;
        }

        public IPostRepository PostRepository => _postsRepository ??= new PostRepository(_context, _postLogger);
        public ITagRepository TagRepository => _tagRepository ??= new TagRepository(_context, _tagLogger);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
