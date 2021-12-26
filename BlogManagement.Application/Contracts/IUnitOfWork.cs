using System;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IPostMetaRepository PostMetaRepository { get; }
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        Task SaveAsync();
    }
}
