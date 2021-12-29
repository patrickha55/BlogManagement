using System;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Repositories;

namespace BlogManagement.Application.Contracts
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
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories

        ICategoryRepository CategoryRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        IPostMetaRepository PostMetaRepository { get; }
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        IUserRepository UserRepository { get; }

            #endregion

        /// <summary>
        /// This method call the save method on the DbContext to commit this transaction.
        /// </summary>
        Task SaveAsync();
    }
}
