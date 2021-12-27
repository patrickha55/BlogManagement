using System;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        IPostMetaRepository PostMetaRepository { get; }
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
