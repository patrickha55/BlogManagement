﻿using BlogManagement.Application.Contracts;
using BlogManagement.Application.Repositories;
using BlogManagement.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Repositories;

namespace BlogManagement.Application
{
    /// <summary>
    /// This class acts as a central place to communicate with all of the repositories in the application and
    /// as a transaction when get injected. Will get dispose after the method SaveAsync get called.
    /// Contains:
    ///     + CategoryRepository.
    ///     + PostCommentRepository.
    ///     + PostMetaRepository.
    ///     + PostRepository.
    ///     + TagRepository.
    ///     + UserRepository.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogManagementContext _context;

        #region Repositories

        private IPostRepository _postsRepository;
        private ITagRepository _tagRepository;
        private ICategoryRepository _categoryRepository;
        private IPostMetaRepository _postMetaRepository;
        private IPostCommentRepository _postCommentRepository;
        private IUserRepository _userRepository;

        #endregion

        #region Logger

        private readonly ILogger<PostRepository> _postLogger;
        private readonly ILogger<TagRepository> _tagLogger;
        private readonly ILogger<CategoryRepository> _categoryLogger;
        private readonly ILogger<PostMetaRepository> _postMetaLogger;
        private readonly ILogger<PostCommentRepository> _postCommentLogger;
        private readonly ILogger<UserRepository> _userRepositoryLogger;

        #endregion

        public UnitOfWork(
            BlogManagementContext context,
            ILogger<CategoryRepository> categoryLogger, 
            ILogger<PostRepository> postLogger,
            ILogger<PostMetaRepository> postMetaLogger,
            ILogger<TagRepository> tagLogger, 
            ILogger<PostCommentRepository> postCommentLogger, 
            ILogger<UserRepository> userRepositoryLogger)
        {
            _context = context;
            _postLogger = postLogger;
            _tagLogger = tagLogger;
            _postCommentLogger = postCommentLogger;
            _userRepositoryLogger = userRepositoryLogger;
            _categoryLogger = categoryLogger;
            _postMetaLogger = postMetaLogger;
        }

        /*
         * Check if a repository already exist. If yes then use that repository, else create a new instance of it.
         */

        public IPostCommentRepository PostCommentRepository =>
            _postCommentRepository ??= new PostCommentRepository(_context, _postCommentLogger);

        public IPostMetaRepository PostMetaRepository =>
            _postMetaRepository ??= new PostMetaRepository(_context, _postMetaLogger);

        public IPostRepository PostRepository => 
            _postsRepository ??= new PostRepository(_context, _postLogger);

        public ITagRepository TagRepository => 
            _tagRepository ??= new TagRepository(_context, _tagLogger);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context, _userRepositoryLogger);

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
