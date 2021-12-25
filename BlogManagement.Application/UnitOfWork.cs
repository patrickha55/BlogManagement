using BlogManagement.Application.Contracts;
using BlogManagement.Data;
using System;
using System.Threading.Tasks;
using BlogManagement.Application.Repositories;
using BlogManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogManagementContext _context;
        private IPostRepository _postsRepository;
        private readonly ILogger<PostRepository> _logger;
        public UnitOfWork(
            BlogManagementContext context, 
            ILogger<PostRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IPostRepository PostRepository => _postsRepository ??= new PostRepository(_context, _logger);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
