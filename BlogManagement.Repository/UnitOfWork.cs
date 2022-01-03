using BlogManagement.Contracts;
using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;
using BlogManagement.Repository.Repositories;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Repository
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
        #region Repositories
        private IPostRepository _postsRepository;
        private ITagRepository _tagRepository;
        private ICategoryRepository _categoryRepository;
        private IPostMetaRepository _postMetaRepository;
        private IPostCommentRepository _postCommentRepository;
        private IUserRepository _userRepository;
        private IRepository<CategoryPost> _categoryPostRepository;
        private IRepository<PostTag> _postTagRepository;
        private IRepository<PostRating> _postRatingRepository;

        #endregion

        #region Logger

        private readonly ILogger<PostRepository> _postLogger;
        private readonly ILogger<TagRepository> _tagLogger;
        private readonly ILogger<CategoryRepository> _categoryLogger;
        private readonly ILogger<PostMetaRepository> _postMetaLogger;
        private readonly ILogger<PostCommentRepository> _postCommentLogger;
        private readonly ILogger<UserRepository> _userRepositoryLogger;
        private readonly ILogger<Repository<CategoryPost>> _categoryPostLogger;
        private readonly ILogger<Repository<PostTag>> _postTagLogger;
        private readonly ILogger<Repository<PostRating>> _postRatingLogger;

        #endregion

        public UnitOfWork(
            BlogManagementContext context,
            ILogger<CategoryRepository> categoryLogger,
            ILogger<PostRepository> postLogger,
            ILogger<PostMetaRepository> postMetaLogger,
            ILogger<TagRepository> tagLogger,
            ILogger<PostCommentRepository> postCommentLogger,
            ILogger<UserRepository> userRepositoryLogger,
            ILogger<Repository<CategoryPost>> categoryPostLogger,
            ILogger<Repository<PostTag>> postTagLogger, 
            ILogger<Repository<PostRating>> postRatingLogger)
        {
            Context = context;
            _postLogger = postLogger;
            _tagLogger = tagLogger;
            _postCommentLogger = postCommentLogger;
            _userRepositoryLogger = userRepositoryLogger;
            _categoryPostLogger = categoryPostLogger;
            _postTagLogger = postTagLogger;
            _postRatingLogger = postRatingLogger;
            _categoryLogger = categoryLogger;
            _postMetaLogger = postMetaLogger;
        }

        /*
         * Check if a repository already exist. If yes then use that repository, else create a new instance of it.
         */

        public IPostCommentRepository PostCommentRepository =>
            _postCommentRepository ??= new PostCommentRepository(Context, _postCommentLogger);

        public IPostMetaRepository PostMetaRepository =>
            _postMetaRepository ??= new PostMetaRepository(Context, _postMetaLogger);

        public IPostRepository PostRepository =>
            _postsRepository ??= new PostRepository(Context, _postLogger);

        public ITagRepository TagRepository =>
            _tagRepository ??= new TagRepository(Context, _tagLogger);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(Context, _userRepositoryLogger);

        public IRepository<CategoryPost> CategoryPostRepository =>
            _categoryPostRepository ??= new Repository<CategoryPost>(Context, _categoryPostLogger);

        public IRepository<PostTag> PostTagRepository
            => _postTagRepository ??= new Repository<PostTag>(Context, _postTagLogger);

        public IRepository<PostRating> PostRatingRepository =>
            _postRatingRepository ??= new Repository<PostRating>(Context, _postRatingLogger);

        public ICategoryRepository CategoryRepository =>
            _categoryRepository ??= new CategoryRepository(Context, _categoryLogger);

        public BlogManagementContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync() => await Context.SaveChangesAsync();
    }
}
