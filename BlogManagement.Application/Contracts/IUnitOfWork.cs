using System;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        ITagRepository TagRepository { get; }
        Task Save();
    }
}
