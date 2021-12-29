using System.Collections.Generic;
using System.Threading.Tasks;
using BlogManagement.Common.Models.AuthorVMs;

namespace BlogManagement.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVM(int pageNumber = 1, int pageSize = 10);
        Task<AuthorDetailVM> FindAuthorDetailVMAsync(long id);
        Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword);
        Task<bool> EditUserStatusesAsync(long id, AuthorDetailVM request);
    }
}
