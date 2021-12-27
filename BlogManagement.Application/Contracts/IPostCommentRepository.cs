using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Data.Entities;

namespace BlogManagement.Application.Contracts
{
    public interface IPostCommentRepository : IRepository<PostComment>
    {
        Task<IEnumerable<PostComment>> GetAllIdAndNameWithoutPagingAsync();
    }
}
