using BlogManagement.Contracts.Repositories;
using BlogManagement.Data;
using BlogManagement.Data.Entities;

namespace BlogManagement.Contracts
{
    /// <summary>
    /// This class acts as a central place to communicate with all of the repositories in the application.
    /// Contains:
    ///     + CategoryRepository.
    ///     + PostCommentRepository.
    ///     + PostMetaRepository.
    ///     + PostRepository.
    ///     + TagRepository.
    ///     + UserRepository.
    ///     + CategoryPostRepository.
    ///     + PostTagRepository.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        BlogManagementContext Context { get; }
        #region Repositories
        ICategoryRepository CategoryRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        IPostMetaRepository PostMetaRepository { get; }
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        IUserRepository UserRepository { get; }
        IRepository<CategoryPost> CategoryPostRepository { get; }
        IRepository<PostTag> PostTagRepository { get; }
        IRepository<PostRating> PostRatingRepository { get; }

        #endregion

        /// <summary>
        /// This method call the save method on the DbContext to commit this transaction.
        /// </summary>
        Task SaveAsync();
    }
}
