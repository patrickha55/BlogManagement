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
        private ICategoryRepository _categoryRepository;
        private IPostMetaRepository _postMetaRepository;
        private readonly ILogger<PostRepository> _postLogger;
        private readonly ILogger<TagRepository> _tagLogger;
        private readonly ILogger<CategoryRepository> _categoryLogger;
        private readonly ILogger<PostMetaRepository> _postMetaLogger;
        public UnitOfWork(
            BlogManagementContext context,
            ILogger<CategoryRepository> categoryLogger, 
            ILogger<PostRepository> postLogger,
            ILogger<PostMetaRepository> postMetaLogger,
            ILogger<TagRepository> tagLogger)
        {
            _context = context;
            _postLogger = postLogger;
            _tagLogger = tagLogger;
            _categoryLogger = categoryLogger;
            _postMetaLogger = postMetaLogger;
        }

        public IPostMetaRepository PostMetaRepository =>
            _postMetaRepository ??= new PostMetaRepository(_context, _postMetaLogger);

        public IPostRepository PostRepository => 
            _postsRepository ??= new PostRepository(_context, _postLogger);

        public ITagRepository TagRepository => 
            _tagRepository ??= new TagRepository(_context, _tagLogger);

        public ICategoryRepository CategoryRepository =>
            _categoryRepository ??= new CategoryRepository(_context, _categoryLogger);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
