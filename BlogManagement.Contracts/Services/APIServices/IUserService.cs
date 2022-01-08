using BlogManagement.Common.DTOs.UserDTOs;
using BlogManagement.Common.Models;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Data.Entities;
using Microsoft.AspNetCore.Identity;
using X.PagedList;

namespace BlogManagement.Contracts.Services.APIServices
{
    public interface IUserService
    {
        Task<List<AuthorAdminIndexVM>> GetAuthorAdminIndexVM(int pageNumber = 1, int pageSize = 10);
        Task<AuthorDetailVM> FindAuthorDetailVMAsync(long id);
        Task<List<AuthorVM>> FindAuthorVMsAsync(string keyword, PagingRequest pagingRequest);
        Task<bool> EditUserStatusesAsync(long id, AuthorDetailVM request);
        Task<(IdentityResult, User)> RegisterAsync(UserRegisterDTO userRegisterDTO);
    }
}
